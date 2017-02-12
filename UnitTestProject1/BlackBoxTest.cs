using System;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis;
using DBConnector;

namespace UnitTest.BlackBox
{
    [TestClass]
    [DeploymentItem("Microsoft.CodeAnalysis.CSharp.dll")]
    [DeploymentItem("Microsoft.CodeAnalysis.CSharp.Workspaces.dll")]
    [DeploymentItem("Microsoft.CodeAnalysis.VisualBasic.dll")]
    [DeploymentItem("Microsoft.CodeAnalysis.VisualBasic.Workspaces.dll")]
    public class BlackBoxTest
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
        }


        static string testFolder;
        static string pathToCreateMappings = null;
        static string pathToTransformClient = null;
        static string sdkNameId = null;

        static BlackBoxTest()
        {
            var dir = new DirectoryInfo(".");
            while ( dir != null && dir.GetFiles("*.sln").Length == 0 )
            {
                dir = dir.Parent;
            }
            if ( dir == null )
            {
                Assert.Fail("could not find parent solution");
                return;
            }
            var soln = MSBuildWorkspace.Create().OpenSolutionAsync(dir.GetFiles("*.sln")[0].FullName).Result;
            foreach ( var proj in soln.Projects )
            {
                if ( proj.Name.Equals("CreateMappings") )
                {
                    pathToCreateMappings = proj.OutputFilePath;
                }
                else if ( proj.Name.Equals("NamespaceRefactorer") )
                {
                    pathToTransformClient = proj.OutputFilePath;
                }
            }
            if ( pathToCreateMappings == null )
            {
                Assert.Fail("Couldn't find create mapping program");
                return;
            }
            if ( pathToTransformClient == null )
            {
                Assert.Fail("Couldn't find transofrm client program");
                return;
            }
        }

        public string CompileProject(Project proj)
        {
            var comp = proj.GetCompilationAsync().Result;
            if (File.Exists(proj.OutputFilePath))
                File.Delete(proj.OutputFilePath);
            comp.Emit(proj.OutputFilePath);
            var solnDir = new FileInfo(proj.FilePath).Directory.Parent;
            var outDir = new FileInfo(proj.OutputFilePath).Directory;
            return proj.OutputFilePath;
        }

        public IEnumerable<string> CompileSolution(string solnPath)
        {
            var result = new LinkedList<string>();
            var soln = MSBuildWorkspace.Create().OpenSolutionAsync(solnPath).Result;
            foreach ( Project proj in soln.Projects )
            {
                result.AddLast(CompileProject(proj));
            }
            return result;
        }

        public void VerifyProject(string projectPath, string expectedPath, string assertMessage)
        {
            var proj = MSBuildWorkspace.Create().OpenProjectAsync(projectPath).Result;
            var result = CompileProject(proj);
            var proc = new Process();
            proc.StartInfo.FileName = result;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            var expectedOut = new StreamReader(expectedPath);
            while ( ! proc.StandardOutput.EndOfStream && ! expectedOut.EndOfStream)
            {
                Assert.AreEqual(expectedOut.ReadLine(), proc.StandardOutput.ReadLine(), assertMessage);
            }
            Assert.AreEqual(expectedOut.EndOfStream, proc.StandardOutput.EndOfStream, assertMessage);
        }

        public virtual void ResetDatabase()
        {
            SDKSQLConnector.GetInstance().DeleteSDKByName(sdkNameId);
        }

        IEnumerable<string> dllsToRemove;
        public virtual void PreSetup()
        {
            foreach ( string path in Directory.GetFiles(Path.Combine(TestFolder, "bin")) )
            {
                File.Delete(path);
            }
            var toRemove = new LinkedList<string>();
            foreach (string dll in CompileSolution(Path.Combine(TestFolder, "oldSDK", "SDK.sln")))
            {
                File.Copy(dll, Path.Combine(TestFolder, "bin", Path.GetFileName(dll)));
                toRemove.AddLast(Path.Combine(TestFolder, "bin", Path.GetFileName(dll)));
            }
            dllsToRemove = toRemove;
        }

        public virtual void PreVerify()
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
            VerifyProject(Path.Combine(TestFolder, "clientC#", "Client", "Client.csproj"), expectedPath,
                "Pre-translation failed");
            VerifyProject(Path.Combine(TestFolder, "clientVB", "Client", "Client.vbproj"), expectedPath,
                "Pre-translation failed");
        }

        IEnumerable<string> dllsToMap;
        public virtual void Setup()
        {
            
        }

        public virtual void RunPreMapping()
        {
            foreach (string dll in dllsToRemove)
            {
                var createMapping = new Process();
                createMapping.StartInfo.FileName = pathToCreateMappings;
                createMapping.StartInfo.Arguments = "\"" + dll + "\" \"" + sdkNameId + "\""; //TODO fill in
                createMapping.Start();
                createMapping.WaitForExit();
                Assert.AreEqual(0, createMapping.ExitCode, "Error running the creating mapping program on old dll: " + dll);
            }
        }

        public virtual void MidMappingSetup()
        {
            foreach (string dll in dllsToRemove)
            {
                File.Delete(dll);
            }

            var toMap = new LinkedList<string>();
            foreach (string dll in CompileSolution(Path.Combine(TestFolder, "newSDK", "SDK.sln")))
            {
                File.Copy(dll, Path.Combine(TestFolder, "bin", Path.GetFileName(dll)));
                toMap.AddLast(dll);
            }
            dllsToMap = toMap;
        }

        public virtual void RunPostMapping()
        {
            foreach (string dll in dllsToMap)
            {
                var createMapping = new Process();
                createMapping.StartInfo.FileName = pathToCreateMappings;
                createMapping.StartInfo.Arguments = "\"" + dll + "\" \"" + sdkNameId + "\""; //TODO fill in
                createMapping.Start();
                createMapping.WaitForExit();
                Assert.AreEqual(0, createMapping.ExitCode, "Error running the creating mapping program on new dll: " + dll);
            }
        }

        public virtual void RunTransformation()
        {
            var projects = new String[] { Path.Combine(TestFolder, "clientC#", "Client", "Client.csproj"),
                Path.Combine(TestFolder, "clientVB", "Client", "Client.vbproj") };
            foreach (var project in projects)
            {
                var translateClient = new Process();
                translateClient.StartInfo.FileName = pathToTransformClient;
                translateClient.StartInfo.Arguments = "\"" + project + "\"";
                translateClient.Start();
                translateClient.WaitForExit();
                Assert.AreEqual(0, translateClient.ExitCode, "error running the translate client program on: " + project);
            }
        }

        public virtual void VerifyResult()
        {
            string expectedPath = Path.Combine(TestFolder, "expectedOutNew.txt");
            if (!File.Exists(expectedPath))
            {
                expectedPath = (Path.Combine(TestFolder, "expectedOut.txt"));
                if (!File.Exists(expectedPath))
                {
                    Assert.Fail("No file containing expected output.");
                }
            }
            VerifyProject(Path.Combine(TestFolder, "clientC#", "Client", "Client.csproj"), expectedPath,
                "Post-translation failed");
            VerifyProject(Path.Combine(TestFolder, "clientVB", "Client", "Client.vbproj"), expectedPath,
                "Post-translation failed");

        }
        
        // call this method in the test method
        public virtual void RunTest()
        {
            sdkNameId = testFolder;
            ResetDatabase();
            PreSetup();
            PreVerify();
            Setup();
            RunPreMapping();
            MidMappingSetup();
            RunPostMapping();
            RunTransformation();
            VerifyResult();
        }


        [TestMethod]
        [DeploymentItem("tests/alias", "alias")]
        public void TestAlias()
        {
            testFolder = "alias";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/assemblyChange", "assemblyChange")]
        public void TestAssemblyChange()
        {
            testFolder = "assemblyChange";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/assemblyMerge", "assemblyMerge")]
        public void TestAssemblyMerge()
        {
            testFolder = "assemblyMerge";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/assemblySplit", "assemblySplit")]
        public void TestAssemblySplit()
        {
            testFolder = "assemblySplit";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/basic", "basic")]
        public void TestBasic()
        {
            testFolder = "basic";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/basicNamespace", "basicNamespace")]
        public void TestBasicNamespace()
        {
            testFolder = "basicNamespace";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/casting", "casting")]
        public void TestCasting()
        {
            testFolder = "casting";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/classInClass", "classInClass")]
        public void TestClassInClass()
        {
            testFolder = "classInClass";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/extendsSDKClass", "extendsSDKClass")]
        public void TestExtendsSDKClass()
        {
            testFolder = "extendsSDKClass";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/extraLibrary", "extraLibrary")]
        public void TestExtraLibrary()
        {
            testFolder = "extraLibrary";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/fullyQualified", "fullyQualified")]
        public void TestFullyQualified()
        {
            testFolder = "fullyQualified";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/fullyQualifiedModelIdentifier", "fullyQualifiedModelIdentifier")]
        public void TestFullyQualifiedModelIdentifier()
        {
            testFolder = "fullyQualifiedModelIdentifier";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/instantiatesSDKClass", "instantiatesSDKClass")]
        public void TestInstantiatesSDKClass()
        {
            testFolder = "instantiatesSDKClass";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/multiAssembly", "multiAssembly")]
        public void TestMultiAssembly()
        {
            testFolder = "multiAssembly";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/multiBasic", "multiBasic")]
        public void TestMultiBasic()
        {
            testFolder = "multiBasic";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/namespaceInNamespace", "namespaceInNamespace")]
        public void TestNamespaceInNamespace()
        {
            testFolder = "namespaceInNamespace";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/newConflicts", "newConflicts")]
        public void TestNewConflicts()
        {
            testFolder = "newConflicts";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/nonRootUsing", "nonRootUsing")]
        public void TestNonRootUsing()
        {
            testFolder = "nonRootUsing";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/nothing", "nothing")]
        public void TestNothing()
        {
            testFolder = "nothing";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/oldConflicts", "oldConflicts")]
        public void TestOldConflicts()
        {
            testFolder = "oldConflicts";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/typeof", "typeof")]
        public void TestTypeof()
        {
            testFolder = "typeof";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/unused", "unused")]
        public void TestUnused()
        {
            testFolder = "unused";
            RunTest();
        }
        
    }
}

// To generate test methods, run this code in bash:
//
//for f in ./*/
//do
//  d=$(basename "${f}")
//  echo
//  echo "        [TestMethod]"
//  echo "        [DeploymentItem(\"tests/$d\", \"$d\")]"
//  echo "        public void Test${d^}()"
//  echo "        {"
//  echo "            testFolder = \"$d\";"
//  echo "            RunTest();"
//  echo "        }"
//done
