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
        string testFolder;
        string sdkNameId;
        public string SdkNameId
        {
            get
            {
                return sdkNameId;
            }
        }
        
        public virtual void RunMappingTest()
        {
            MappingSetup();
            RunMapping();
            VerifyMapping();
        }

        string projectUnderTest;

        public virtual void RunPreTransformCSTest()
        {
            projectUnderTest = Path.Combine(TestFolder, "clientC#", "Client", "Client.csproj");
            SetupPreTransformTest();
            VerifyPreTransformTest();
        }

        public virtual void RunPostTransformCSTest()
        {
            projectUnderTest = Path.Combine(TestFolder, "clientC#", "Client", "Client.csproj");
            SetupPostTransformTest();
            ProcessPostTransformTest();
            VerifyPostTransformTest();
        }

        public virtual void RunPreTransformVBTest()
        {
            projectUnderTest = Path.Combine(TestFolder, "clientVB", "Client", "Client.vbproj");
            SetupPreTransformTest();
            VerifyPreTransformTest();
        }

        public virtual void RunPostTransformVBTest()
        {
            projectUnderTest = Path.Combine(TestFolder, "clientVB", "Client", "Client.vbproj");
            SetupPostTransformTest();
            ProcessPostTransformTest();
            VerifyPostTransformTest();
        }

        public virtual void RunCSEndToEndTest()
        {
            projectUnderTest = Path.Combine(TestFolder, "clientC#", "Client", "Client.csproj");
            SetupEndToEnd();
            RunMapping();
            ProcessPostTransformTest();
            VerifyPostTransformTest();
        }

        public virtual void RunVBEndToEndTest()
        {
            projectUnderTest = Path.Combine(TestFolder, "clientVB", "Client", "Client.vbproj");
            SetupEndToEnd();
            RunMapping();
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
                else if (proj.Name.Equals("TransformClient"))
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

        public virtual void ResetDatabase()
        {
            SDKSQLConnector.GetInstance().DeleteSDKByName(sdkNameId);
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
            Assert.AreEqual(0, proc.ExitCode, "project exited unexpectedly");
            var expectedOut = new StreamReader(expectedPath);
            while (!proc.StandardOutput.EndOfStream && !expectedOut.EndOfStream)
            {
                Assert.AreEqual(expectedOut.ReadLine(), proc.StandardOutput.ReadLine());
            }
            Assert.AreEqual(expectedOut.EndOfStream, proc.StandardOutput.EndOfStream);
        }

        public virtual void CompileLibraries()
        {
            Directory.Delete(Path.Combine(TestFolder, "bin1"), true);
            Directory.CreateDirectory(Path.Combine(TestFolder, "bin1"));
            CompileSolution(Path.Combine(TestFolder, "oldSDK", "SDK.sln"));
            Directory.Delete(Path.Combine(TestFolder, "bin2"), true);
            Directory.CreateDirectory(Path.Combine(TestFolder, "bin2"));
            CompileSolution(Path.Combine(TestFolder, "newSDK", "SDK.sln"));
        }
            
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
                    Path.GetFullPath(Path.Combine(TestFolder, curLine[old_assembly_path_id])),
                    Path.GetFullPath(Path.Combine(TestFolder, curLine[new_assembly_path_id])) ));
            }
        }

        public virtual void LoadExpectedMappingsToDatabase()
        {
            LoadExpectedMappings();
            SDKSQLConnector.GetInstance().SaveSDK(sdkNameId, Path.GetFullPath(Path.Combine(TestFolder, "bin2")));
            var sdkId = SDKSQLConnector.GetInstance().getByName(sdkNameId).id;
            SDKMappingSQLConnector.GetInstance().SaveSDKMappings(expectedMappings, sdkId);
        }
        
        // MappingTest
        public virtual void MappingSetup()
        {
            CompileLibraries();
            LoadExpectedMappings();
            ResetDatabase();
        }

        public virtual void RunMapping()
        {
            var createMapping = new Process();
            createMapping.StartInfo.FileName = pathToCreateMappings;
            createMapping.StartInfo.Arguments = "\"" + Path.Combine(TestFolder, "bin1") + "\" \"" +
                Path.Combine(TestFolder, "bin2") + "\" \"" + sdkNameId + "\"";
            createMapping.StartInfo.UseShellExecute = false;
            createMapping.StartInfo.RedirectStandardOutput = true;
            createMapping.StartInfo.RedirectStandardError = true;
            createMapping.Start();
            Trace.WriteLine("------------ Started create mappings with arguments: " + createMapping.StartInfo.Arguments);
            Trace.WriteLine("------------ Start create mappings standard output");
            while ( !createMapping.StandardOutput.EndOfStream )
            {
                Trace.WriteLine(createMapping.StandardOutput.ReadLine());
            }
            Trace.WriteLine("------------ End create mappings standard output ");
            Trace.WriteLine("------------ Start create mappings error output ");
            while (!createMapping.StandardError.EndOfStream)
            {
                Trace.WriteLine(createMapping.StandardError.ReadLine());
            }
            Trace.WriteLine("------------ End create mappings error output ");
            createMapping.WaitForExit();
            Assert.AreEqual(0, createMapping.ExitCode, "Error running the create mappings program");
        }

        public virtual void VerifyMapping()
        {
            var sdkId = SDKSQLConnector.GetInstance().getByName(sdkNameId).id;
            Assert.AreEqual(Path.GetFullPath(Path.Combine(TestFolder,"bin2")), SDKSQLConnector.GetInstance().getOutputPathById(sdkId),
                "Wrong output path");
            var actualMappings = SDKMappingSQLConnector.GetInstance().GetAllSDKMapsBySDKId(sdkId);
            Assert.AreEqual(expectedMappings.Count, actualMappings.Count, "Wrong number of generated mappings");
            foreach (var expect in actualMappings)
            {
                if ( ! actualMappings.Contains(expect) )
                {
                    Assert.Fail("Missing actual mapping for " + expect.ModelIdentifierGUID);
                }
            }
        }


        // PreTransformTest
        public virtual void SetupPreTransformTest()
        {
            CompileLibraries();
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
            VerifyProject(projectUnderTest, expectedPath);
        }

        // PostTransformTest
        public virtual void SetupPostTransformTest()
        {
            CompileLibraries();
            ResetDatabase();
            LoadExpectedMappingsToDatabase();
        }

        public virtual void ProcessPostTransformTest()
        {
            var translateClient = new Process();
            translateClient.StartInfo.FileName = pathToTransformClient;
            translateClient.StartInfo.Arguments = "\"" + projectUnderTest + "\" \"" + sdkNameId + "\"";
            translateClient.StartInfo.UseShellExecute = false;
            translateClient.StartInfo.RedirectStandardOutput = true;
            translateClient.Start();
            translateClient.StartInfo.UseShellExecute = false;
            translateClient.StartInfo.RedirectStandardOutput = true;
            translateClient.StartInfo.RedirectStandardError = true;
            translateClient.Start();
            Trace.WriteLine("------------ Started translate client with arguments: " + translateClient.StartInfo.Arguments);
            Trace.WriteLine("------------ Start translate client standard output ");
            while (!translateClient.StandardOutput.EndOfStream)
            {
                Trace.WriteLine(translateClient.StandardOutput.ReadLine());
            }
            Trace.WriteLine("------------ End translate client standard output ");
            Trace.WriteLine("------------ Start translate client error output ");
            while (!translateClient.StandardError.EndOfStream)
            {
                Trace.WriteLine(translateClient.StandardError.ReadLine());
            }
            Trace.WriteLine("------------ End translate client error output ");
            translateClient.WaitForExit();
            Assert.AreEqual(0, translateClient.ExitCode, "error running the translate client program");
        }

        public virtual void VerifyPostTransformTest()
        {
            Directory.Delete(Path.Combine(TestFolder, "bin1"));
            string expectedPath = Path.Combine(TestFolder, "expectedOutNew.txt");
            if (!File.Exists(expectedPath))
            {
                expectedPath = (Path.Combine(TestFolder, "expectedOut.txt"));
                if (!File.Exists(expectedPath))
                {
                    Assert.Fail("No file containing expected output.");
                }
            }
            VerifyProject(projectUnderTest, expectedPath);
        }
        
        //EndToEndTest
        public virtual void SetupEndToEnd()
        {
            CompileLibraries();
            ResetDatabase();
        }

    }
}

// current special cases:
// extraLibrary (add this code) 
//public override void CompileLibraries()
//{
//    base.CompileLibraries();
//    CompileSolution(Path.Combine(TestFolder, "extraLibrary", "extraLibrary.sln"));
//}
//
// To generate test methods, run this code in bash in the tests directory:
//
//for f in ./*/
//do
//  d=$(basename "${f}")
//  echo ""
//  echo "    [TestClass]"
//  echo "    [DeploymentItem(\"tests/$d\", \"$d\")]"
//  echo "    public class ${d^}Tests : BlackBoxBase"
//  echo "    {"
//  echo "        [TestInitialize]"
//  echo "        public void Init${d^}()"
//  echo "        {"
//  echo "            TestFolder = \"$d\";"
//  echo "        }"
//  echo "        [TestMethod]"
//  echo "        public void TestMapping${d^}()"
//  echo "        {"
//  echo "            RunMappingTest();"
//  echo "        }"
//  echo "        [TestMethod]"
//  echo "        public void TestPreTransformCS${d^}()"
//  echo "        {"
//  echo "            RunPreTransformCSTest();"
//  echo "        }"
//  echo "        [TestMethod]"
//  echo "        public void TestPostTransformCS${d^}()"
//  echo "        {"
//  echo "            RunPostTransformCSTest();"
//  echo "        }"
//  echo "        [TestMethod]"
//  echo "        public void TestPreTransformVB${d^}()"
//  echo "        {"
//  echo "            RunPreTransformVBTest();"
//  echo "        }"
//  echo "        [TestMethod]"
//  echo "        public void TestPostTransformVB${d^}()"
//  echo "        {"
//  echo "            RunPostTransformVBTest();"
//  echo "        }"
//  echo "        [TestMethod]"
//  echo "        public void TestEndToEndCS${d^}()"
//  echo "        {"
//  echo "            RunPostTransformVBTest();"
//  echo "        }"
//  echo "        [TestMethod]"
//  echo "        public void TestEndToEndVB${d^}()"
//  echo "        {"
//  echo "            RunPostTransformVBTest();"
//  echo "        }"
//  echo "    }"
//done
