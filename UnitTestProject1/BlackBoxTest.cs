using System;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis;

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
        static string pathToCreateMappings = null; //TODO fill in
        static string pathToTransformClient = null; //TODO fill in

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

        public virtual void Setup()
        {
            foreach (string dll in dllsToRemove)
            {
                File.Delete(dll);
            }

            foreach (string dll in CompileSolution(Path.Combine(TestFolder, "newSDK", "SDK.sln")))
            {
                File.Copy(dll, Path.Combine(TestFolder, "bin", Path.GetFileName(dll)));
            }
        }

        public virtual void RunProgram()
        {
            var createMapping = new Process();
            createMapping.StartInfo.FileName = pathToCreateMappings;
            createMapping.StartInfo.Arguments = ""; //TODO fill in
            createMapping.Start();
            createMapping.WaitForExit();
            Assert.AreEqual(0, createMapping.ExitCode, "Error running the creating mapping program");
            var translateClient = new Process();
            translateClient.StartInfo.FileName = pathToTransformClient;
            translateClient.StartInfo.Arguments = ""; //TODO fill in
            translateClient.Start();
            translateClient.WaitForExit();
            Assert.AreEqual(0, translateClient.ExitCode, "error running the translate client program");
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
            PreSetup();
            PreVerify();
            Setup();
            RunProgram();
            VerifyResult();
        }

        [TestMethod]
        [DeploymentItem("tests/nothing","nothing")]
        public void TestNothing()
        {
            testFolder = "nothing";
            RunTest();
        }
        
    }
}
