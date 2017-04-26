using EFSQLConnector;
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
    [DeploymentItem("EntityFramework.SqlServer.dll")]
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

        protected string projectUnderTest;

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

        public virtual void RunEndToEndCSTest()
        {
            projectUnderTest = Path.Combine(TestFolder, "clientC#", "Client", "Client.csproj");
            SetupEndToEnd();
            RunMapping();
            ProcessPostTransformTest();
            VerifyPostTransformTest();
        }

        public virtual void RunEndToEndVBTest()
        {
            projectUnderTest = Path.Combine(TestFolder, "clientVB", "Client", "Client.vbproj");
            SetupEndToEnd();
            RunMapping();
            ProcessPostTransformTest();
            VerifyPostTransformTest();
        }

        static protected string pathToCreateMappings = null;
        static protected string pathToTransformClient = null;

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

        public string CompileProject(Project proj)
        {
            Trace.WriteLine("------------ Compiling \"" + proj.FilePath + "\"");
            var comp = proj.GetCompilationAsync().Result;
            var diag = comp.GetDiagnostics();
            foreach (var item in diag.Where(item => item.Severity != DiagnosticSeverity.Hidden))
            {
                Trace.WriteLine(item.ToString());
            }
            var issues = comp.GetDiagnostics().Where(item => item.Severity == DiagnosticSeverity.Error);
            if ( issues.Count() > 0 )
            {
                Assert.Fail("Errors encountered when compiling \"" + proj.FilePath + "\"");
            }
            if (File.Exists(proj.OutputFilePath))
                File.Delete(proj.OutputFilePath);
            Trace.WriteLine("--- Emitting");
            var emitResult = comp.Emit(proj.OutputFilePath);
            var emitIssues = emitResult.Diagnostics.Where(item => item.Severity == DiagnosticSeverity.Error);
            foreach (var item in emitResult.Diagnostics.Where(item => item.Severity != DiagnosticSeverity.Hidden))
            {
                Trace.WriteLine(item.ToString());
            }
            if ( emitIssues.Count() > 0 )
            {
                Assert.Fail("Errors encountered when emitting \"" + proj.FilePath + "\"");
            }
            Trace.WriteLine("------------ Finished compiling \"" + proj.FilePath + "\"");
            return proj.OutputFilePath;
        }

        public void CompileSolution(string solnPath)
        {
            var soln = MSBuildWorkspace.Create().OpenSolutionAsync(solnPath).Result;
            foreach ( var proj in soln.Projects )
            {
                CompileProject(proj);
            }
        }

        public void VerifyProject(string projectPath, string expectedPath)
        {
            var expectedReferences = System.Text.RegularExpressions.Regex.Matches(File.ReadAllText(projectPath),
                "<Reference").Count;
            expectedReferences += 1; // for the auto-included refernce to system libraries "mscorlib.dll"
            if ( projectPath.EndsWith(".vbproj") )
            {
                expectedReferences += 1; // for the auto-included visual basic libraries ""Microsoft.VisualBasic.dll"
            }
            var proj = MSBuildWorkspace.Create().OpenProjectAsync(projectPath).Result;
            Assert.AreEqual(expectedReferences, proj.MetadataReferences.Count, "Incorrect number of references, some are probably repeated or missing");
            var result = CompileProject(proj);
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
            proc.WaitForExit();
            Assert.AreEqual(0, proc.ExitCode, "project exited unexpectedly");
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
            
        List<sdk_map2> expectedMappings;

        public string GetValueFromExpected(string expected)
        {
            if ( expected == "'null'")
            {
                return null;
            }
            else if ( expected == "'?'")
            {
                return "'?'";
            }
            else
            {
                return expected;
            }
        }

        public virtual void LoadExpectedMappings()
        {
            int model_identifier_id = -1;
            int old_namespace_id = -1;
            int old_classname_id = -1;
            int new_namespace_id = -1;
            int new_classname_id = -1;
            int old_assembly_path_id = -1;
            int new_assembly_path_id = -1;
            int assembly_name_id = -1;
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
            Assert.AreEqual(8, lineOne.Length, "Impropper number of headers");
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
                    case "assembly_name":
                        assembly_name_id = i;
                        break;
                    default:
                        Assert.Fail("invalid header " + lineOne[i]);
                        break;
                }
            }
            expectedMappings = new List<sdk_map2>();
            for (var i = 1; i < lines.Length; i++)
            {
                var curLine = lines[i].Split('\t');
                Assert.AreEqual(lineOne.Length, curLine.Length, "wrong number of entries on line " + i);
                var namespaceMap = new namespace_map();
                namespaceMap.new_namespace = GetValueFromExpected(curLine[new_namespace_id]);
                namespaceMap.old_namespace = GetValueFromExpected(curLine[old_namespace_id]);
                var assemblyMap = new assembly_map();
                assemblyMap.name = GetValueFromExpected(curLine[assembly_name_id]);
                assemblyMap.new_path = GetValueFromExpected(curLine[new_assembly_path_id]);
                if (assemblyMap.new_path != null && assemblyMap.new_path != "'?'")
                {
                    assemblyMap.new_path = Path.GetFullPath(Path.Combine(TestFolder, curLine[new_assembly_path_id]));
                }
                assemblyMap.old_path = GetValueFromExpected(curLine[old_assembly_path_id]);
                if (assemblyMap.old_path != null && assemblyMap.old_path != "'?'")
                {
                    assemblyMap.old_path = Path.GetFullPath(Path.Combine(TestFolder, curLine[old_assembly_path_id]));
                }
                var sdkMap = new sdk_map2();
                sdkMap.namespace_map = namespaceMap;
                sdkMap.assembly_map = assemblyMap;
                sdkMap.new_classname = GetValueFromExpected(curLine[new_classname_id]);
                sdkMap.old_classname = GetValueFromExpected(curLine[old_classname_id]);
                sdkMap.model_identifier = GetValueFromExpected(curLine[model_identifier_id]);
                expectedMappings.Add(sdkMap);
            }
        }

        public virtual void LoadExpectedMappingsToDatabase()
        {
            LoadExpectedMappings();
            SDKSQLConnector.GetInstance().SaveSDK(sdkNameId, Path.GetFullPath(Path.Combine(TestFolder, "bin2")));
            var sdkId = SDKSQLConnector.GetInstance().getByName(sdkNameId).id;
            SDKMappingSQLConnector.GetInstance().SaveFullSDKMap(sdkId, expectedMappings);
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
#if DEBUGABLE_EXECUTION
            var originalOut = Console.Out;
            var originalErr = Console.Error;
            try
            {
                Console.SetOut(new TraceTextWriter(originalOut, "Out: "));
                Console.SetError(new TraceTextWriter(originalErr, "Err: "));
                CreateMappings.ReadProject.Main(new[] { Path.Combine(TestFolder, "bin1"),
                    Path.Combine(TestFolder, "bin2"), sdkNameId });
            }
            finally
            {
                Console.SetOut(originalOut);
                Console.SetError(originalErr);
            }
#else
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
#endif
        }

        private bool CheckMappingValue(string expect, string actual)
        {
            if ( expect == "'?'")
            {
                return true;
            }
            else
            {
                return actual == expect;
            }
            
        }

        public virtual void VerifyMapping()
        {
            var sdkId = SDKSQLConnector.GetInstance().getByName(sdkNameId).id;
            Assert.AreEqual(Path.GetFullPath(Path.Combine(TestFolder,"bin2")), SDKSQLConnector.GetInstance().getOutputPathById(sdkId),
                "Wrong output path");
            var actualMappings = SDKMappingSQLConnector.GetInstance().GetAllBySdkId(sdkId);
            Assert.AreEqual(expectedMappings.Count, actualMappings.Count, "Wrong number of generated mappings");
            foreach (var expect in expectedMappings)
            {
                var hasMapping = false;
                foreach (var actual in actualMappings)
                {
                    if ( expect.model_identifier == actual.model_identifier)
                    {
                        if (CheckMappingValue(expect.new_classname, actual.new_classname) &&
                            CheckMappingValue(expect.assembly_map.new_path, actual.assembly_map.new_path) &&
                            CheckMappingValue(expect.namespace_map.new_namespace, actual.namespace_map.new_namespace) &&
                            CheckMappingValue(expect.old_classname, actual.old_classname) &&
                            CheckMappingValue(expect.assembly_map.old_path, actual.assembly_map.old_path) &&
                            CheckMappingValue(expect.namespace_map.old_namespace, actual.namespace_map.old_namespace) &&
                            CheckMappingValue(expect.assembly_map.name, actual.assembly_map.name) )
                        {
                            hasMapping = true;
                        }
                        break;
                    }
                }
                if ( ! hasMapping/*actualMappings.Contains(expect)*/ )
                {
                    Assert.Fail("Missing actual mapping for " + expect.model_identifier);
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
#if DEBUGABLE_EXECUTION
            var originalOut = Console.Out;
            var originalErr = Console.Error;
            try
            {
                Console.SetOut(new TraceTextWriter(originalOut, "Out: "));
                Console.SetError(new TraceTextWriter(originalErr, "Err: "));
                TransformClient.TransformProject.Main(new[] { projectUnderTest, sdkNameId });
            }
            finally
            {
                Console.SetOut(originalOut);
                Console.SetError(originalErr);
            }
#else
            var translateClient = new Process();
            translateClient.StartInfo.FileName = pathToTransformClient;
            translateClient.StartInfo.Arguments = "\"" + projectUnderTest + "\" \"" + sdkNameId + "\"";
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
#endif
        }

        public virtual void VerifyPostTransformTest()
        {
            Directory.Delete(Path.Combine(TestFolder, "bin1"), true);
            string expectedPath = Path.Combine(TestFolder, "expectedOutNew.txt");
            if (!File.Exists(expectedPath))
            {
                expectedPath = (Path.Combine(TestFolder, "expectedOut.txt"));
                if (!File.Exists(expectedPath))
                {
                    Assert.Fail("No file containing expected output.");
                }
            }
            VerifyProject(Path.Combine(Directory.GetParent(projectUnderTest).FullName + "_transformed",
                Path.GetFileName(projectUnderTest)), expectedPath);
        }
        
        //EndToEndTest
        public virtual void SetupEndToEnd()
        {
            CompileLibraries();
            ResetDatabase();
        }

    }
    
    class TraceTextWriter : TextWriter
    {
        private TextWriter original;
        private string linePrefix;
        public TraceTextWriter(TextWriter original, string linePrefix)
        {
            this.original = original;
            this.linePrefix = linePrefix;
        }
        public override Encoding Encoding
        {
            get
            {
                return original.Encoding;
            }
        }

        public override void Write(char value)
        {
            original.Write(value);
            Trace.Write(value);
            if ( value == "\n".Last() )
            {
                original.Write(linePrefix);
                Trace.Write(linePrefix);
            }
        }
    }
}

// current special cases:
// extraLibrary (add this code) 
//public override void CompileLibraries()
//{
//    base.CompileLibraries();
//    CompileSolution(Path.Combine(TestFolder, "extraLibrary", "extraLibrary.sln"));
//    File.Copy(Path.Combine(TestFolder, "bin1", "extraLibrary.dll"),
//        Path.Combine(TestFolder, "bin2", "extraLibrary.dll"));
//}
//
// To generate test methods, run this code in bash in the tests directory:
//
//for f in ./*/
//do
//  d=$(basename "${f}")
//  echo ""
//  echo "    [TestClass]"
//  echo "    public class BlackBox${d^}Tests : BlackBoxBase"
//  echo "    {"
//  for test in Mapping PreTransformCS PostTransformCS PreTransformVB PostTransformVB EndToEndCS EndToEndVB
//  do
//    echo "        [TestMethod]"
//    echo "        [DeploymentItem(\"tests/$d\", \"${test}${d^}\")]"
//    echo "        public void TestBlackBox${test}${d^}()"
//    echo "        {"
//    echo "            TestFolder = \"${test}${d^}\";"
//    echo "            Run${test}Test();"
//    echo "        }"
//  done
//  echo "    }"
//done
