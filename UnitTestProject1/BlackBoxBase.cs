using DBConnector;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.BlackBox
{
    [DeploymentItem("Microsoft.CodeAnalysis.CSharp.dll")]
    [DeploymentItem("Microsoft.CodeAnalysis.CSharp.Workspaces.dll")]
    [DeploymentItem("Microsoft.CodeAnalysis.VisualBasic.dll")]
    [DeploymentItem("Microsoft.CodeAnalysis.VisualBasic.Workspaces.dll")]
    public class BlackBoxBase
    {
        // this must be deployed using the attribute
        // [DeploymentItem(test_folder)]
        // on the test method
        public string TestFolder
        {
            get
            {
                return testFolder;
            }

            set
            {
                testFolder = new FileInfo(value).FullName;
                sdkNameId = Path.GetFileName(testFolder);
            }
        }
        private string testFolder;
        private string sdkNameId;
        public string SdkNameId
        {
            get
            {
                return sdkNameId;
            }
        }
        
        public virtual void RunMappingTest()
        {
            LoadExpectedMappings();
            ResetDatabase();
            RunMapping();
            VerifyMapping();
        }
        
        public virtual void RunPreTransformTest()
        {
            SetupPreTransformTest();
            VerifyPreTransformTest();
        }

        public virtual void RunPostTransformTest()
        {
            SetupPostTransformTest();
            ProcessPostTransformTest();
            VerifyPostTransformTest();
        }

        static string pathToCreateMappings = null;
        static string pathToTransformClient = null;

        static BlackBoxBase()
        {
            var dir = new DirectoryInfo(".");
            while (dir != null && dir.GetFiles("*.sln").Length == 0)
            {
                dir = dir.Parent;
            }
            if (dir == null)
            {
                Assert.Fail("could not find parent solution");
                return;
            }
            var soln = MSBuildWorkspace.Create().OpenSolutionAsync(dir.GetFiles("*.sln")[0].FullName).Result;
            foreach (var proj in soln.Projects)
            {
                if (proj.Name.Equals("CreateMappings"))
                {
                    pathToCreateMappings = proj.OutputFilePath;
                }
                else if (proj.Name.Equals("TransformClient2"))
                {
                    pathToTransformClient = proj.OutputFilePath;
                }
            }
            if (pathToCreateMappings == null)
            {
                Assert.Fail("Couldn't find create mapping program");
                return;
            }
            if (pathToTransformClient == null)
            {
                Assert.Fail("Couldn't find transofrm client program");
                return;
            }
        }
        
        public string CompileProject(string projPath)
        {
            var proj = MSBuildWorkspace.Create().OpenProjectAsync(projPath).Result;
            var comp = proj.GetCompilationAsync().Result;
            if (File.Exists(proj.OutputFilePath))
                File.Delete(proj.OutputFilePath);
            comp.Emit(proj.OutputFilePath);
            return proj.OutputFilePath;
        }

        public void CompileSolution(string solnPath)
        {
            var soln = MSBuildWorkspace.Create().OpenSolutionAsync(solnPath).Result;
            foreach ( var proj in soln.Projects )
            {
                var comp = proj.GetCompilationAsync().Result;
                if (File.Exists(proj.OutputFilePath))
                    File.Delete(proj.OutputFilePath);
                comp.Emit(proj.OutputFilePath);
            }
        }

        public void VerifyProject(string projectPath, string expectedPath)
        {
            var result = CompileProject(projectPath);
            var proc = new Process();
            proc.StartInfo.FileName = result;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            var expectedOut = new StreamReader(expectedPath);
            while (!proc.StandardOutput.EndOfStream && !expectedOut.EndOfStream)
            {
                Assert.AreEqual(expectedOut.ReadLine(), proc.StandardOutput.ReadLine());
            }
            Assert.AreEqual(expectedOut.EndOfStream, proc.StandardOutput.EndOfStream);
        }

        // MappingTest
        List<Mapping> expectedMappings;

        public virtual void LoadExpectedMappings()
        {
            int model_identifier_id = -1;
            int old_namespace_id = -1;
            int old_classname_id = -1;
            int new_namespace_id = -1;
            int new_classname_id = -1;
            int old_assembly_path_id = -1;
            int new_assembly_path_id = -1;
            if (!File.Exists(Path.Combine(TestFolder, "expectedSql.txt")))
            {
                Assert.Fail("expectedSql.txt does not exist");
            }
            var lines = File.ReadAllLines(Path.Combine(TestFolder, "expectedSql.txt"));
            if (lines.Length <= 0)
            {
                Assert.Fail("expectedSql.txt is empty");
            }
            var lineOne = lines[0].Split('\t');
            Assert.AreEqual(7, lineOne.Length, "Impropper number of headers");
            for (var i = 0; i < lineOne.Length; i++)
            {
                switch (lineOne[i])
                {
                    case "model_identifier":
                        model_identifier_id = i;
                        break;
                    case "old_namespace":
                        old_namespace_id = i;
                        break;
                    case "old_classname":
                        old_classname_id = i;
                        break;
                    case "new_namespace":
                        new_namespace_id = i;
                        break;
                    case "new_classname":
                        new_classname_id = i;
                        break;
                    case "old_assembly_path":
                        old_assembly_path_id = i;
                        break;
                    case "new_assembly_path":
                        new_assembly_path_id = i;
                        break;
                    default:
                        Assert.Fail("invalid header " + lineOne[i]);
                        break;
                }
            }
            expectedMappings = new List<Mapping>();
            for (var i = 1; i < lines.Length; i++)
            {
                var curLine = lines[i].Split('\t');
                Assert.AreEqual(lineOne.Length, curLine.Length, "wrong number of entries on line " + i);
                expectedMappings.Add(new Mapping(curLine[old_namespace_id], curLine[new_namespace_id],
                    curLine[model_identifier_id], curLine[old_classname_id], curLine[new_classname_id],
                    curLine[old_assembly_path_id], curLine[new_namespace_id]));
            }
        }

        public virtual void ResetDatabase()
        {
            SDKSQLConnector.GetInstance().DeleteSDKByName(sdkNameId);
        }

        public virtual void RunMapping()
        {
            var createMapping = new Process();
            createMapping.StartInfo.FileName = pathToCreateMappings;
            createMapping.StartInfo.Arguments = "\"" + Path.Combine(TestFolder, "bin1") + "\" \"" +
                Path.Combine(TestFolder, "bin2") + "\" \"" + sdkNameId + "\"";
            createMapping.Start();
            createMapping.WaitForExit();
            Assert.AreEqual(0, createMapping.ExitCode, "Error running the creating mappings");
        }

        public virtual void VerifyMapping()
        {
            var sdkId = SDKSQLConnector.GetInstance().getByName(sdkNameId).id;
            var actualMappings = SDKMappingSQLConnector.GetInstance().GetAllByWhereClause(m => m.id == sdkId);
            Assert.AreEqual(expectedMappings.Count, actualMappings.Count, "Wrong number of generated mappings");
            foreach (var expect in actualMappings)
            {
                var found = false;
                foreach (var actual in expectedMappings)
                {
                    if (expect.ModelIdentifierGUID.Equals(actual.ModelIdentifierGUID))
                    {
                        found = true;
                        Assert.AreEqual(expect.NewClassName, actual.NewClassName, "NewClassName mismatch");
                        Assert.AreEqual(expect.NewNamespace, actual.NewNamespace, "NewNamespace mismatch");
                        Assert.AreEqual(expect.OldClassName, actual.OldClassName, "OldClassName mismatch");
                        Assert.AreEqual(expect.OldNamespace, actual.OldNamespace, "OldNamespace mismatch");
                        Assert.AreEqual(
                            Path.GetFullPath(Path.Combine(TestFolder, expect.NewDllPath)),
                            Path.GetFullPath(actual.NewDllPath),
                            "NewDllPath mismatch");
                        Assert.AreEqual(
                            Path.GetFullPath(Path.Combine(TestFolder, expect.OldDllPath)),
                            Path.GetFullPath(actual.OldDllPath),
                            "NewDllPath mismatch");
                        break;
                    }
                }
                if (!found)
                {
                    Assert.Fail("Could not find actual mapping for " + expect.ModelIdentifierGUID);
                }
            }
        }


        // PreTransformTest
        public virtual void SetupPreTransformTest()
        {
            Directory.Delete(Path.Combine(TestFolder, "bin1"), true);
            Directory.CreateDirectory(Path.Combine(TestFolder, "bin1"));
            CompileSolution(Path.Combine(TestFolder, "oldSDK", "SDK.sln"));
        }

        public virtual void VerifyPreTransformTest()
        {
            string expectedPath = Path.Combine(TestFolder, "expectedOutOld.txt");
            if (!File.Exists(expectedPath))
            {
                expectedPath = (Path.Combine(TestFolder, "expectedOut.txt"));
                if (!File.Exists(expectedPath))
                {
                    Assert.Fail("No file containing expected output.");
                }
            }
            VerifyProject(Path.Combine(TestFolder, "clientC#", "Client", "Client.csproj"), expectedPath);
            VerifyProject(Path.Combine(TestFolder, "clientVB", "Client", "Client.vbproj"), expectedPath);
        }

        // PostTransformTest
        public virtual void SetupPostTransformTest()
        {
            Directory.Delete(Path.Combine(TestFolder, "bin1"), true);
            Directory.CreateDirectory(Path.Combine(TestFolder, "bin1"));
            CompileSolution(Path.Combine(TestFolder, "oldSDK", "SDK.sln"));
            Directory.Delete(Path.Combine(TestFolder, "bin2"), true);
            Directory.CreateDirectory(Path.Combine(TestFolder, "bin2"));
            CompileSolution(Path.Combine(TestFolder, "newSDK", "SDK.sln"));
        }

        public virtual void ProcessPostTransformTest()
        {

        }

        public virtual void VerifyPostTransformTest()
        {

        }
    }
}
