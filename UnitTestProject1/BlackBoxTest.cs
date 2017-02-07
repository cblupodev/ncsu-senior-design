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
        static string pathToCreateMappings = @""; //TODO fill in
        static string pathToTransformClient = @""; //TODO fill in

        public string CompileProject(Project proj)
        {
            var comp = proj.GetCompilationAsync().Result;
            if (File.Exists(proj.OutputFilePath))
                File.Delete(proj.OutputFilePath);
            comp.Emit(proj.OutputFilePath);
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

        public void verifyProject(string projectPath, string expectedPath)
        {
            var proj = MSBuildWorkspace.Create().OpenProjectAsync(projectPath).Result;
            var result = CompileProject(proj);
            var proc = new Process();
            proc.StartInfo.FileName = result;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            var expectedOut = new StreamReader(expectedPath);
            while ( ! proc.StandardOutput.EndOfStream && ! expectedOut.EndOfStream)
            {
                Assert.AreEqual(expectedOut.ReadLine(), proc.StandardOutput.ReadLine());
            }
            Assert.AreEqual(expectedOut.EndOfStream, proc.StandardOutput.EndOfStream);
        }

        IEnumerable<string> dllsToRemove;
        public virtual void PreSetup()
        {
            foreach ( string path in Directory.GetFiles(Path.Combine(TestFolder, "clientC#", "libs")) )
            {
                File.Delete(path);
            }
            foreach (string path in Directory.GetFiles(Path.Combine(TestFolder, "clientVB", "libs")))
            {
                File.Delete(path);
            }
            var toRemove = new LinkedList<string>();
            foreach (string dll in CompileSolution(Path.Combine(TestFolder, "oldSDK", "SDK.sln")))
            {
                File.Copy(dll, Path.Combine(TestFolder, "clientC#", "libs"));
                File.Copy(dll, Path.Combine(TestFolder, "clientVB", "libs"));
                toRemove.AddLast(Path.Combine(TestFolder, "clientC#", "libs", Path.GetFileName(dll)));
                toRemove.AddLast(Path.Combine(TestFolder, "clientVB", "libs", Path.GetFileName(dll)));
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
            verifyProject(Path.Combine(TestFolder, "clientC#", "Client", "Client.csproj"), expectedPath);
            verifyProject(Path.Combine(TestFolder, "clientVB", "Client", "Client.vbproj"), expectedPath);
        }

        public virtual void Setup()
        {
            foreach (string dll in dllsToRemove)
            {
                File.Delete(dll);
            }

            foreach (string dll in CompileSolution(Path.Combine(TestFolder, "newSDK", "SDK.sln")))
            {
                File.Copy(dll, Path.Combine(TestFolder, "clientC#", "libs"));
                File.Copy(dll, Path.Combine(TestFolder, "clientVB", "libs"));
            }
        }

        public virtual void RunProgram()
        {
            var createMapping = new Process();
            createMapping.StartInfo.FileName = pathToCreateMappings;
            createMapping.StartInfo.Arguments = ""; //TODO fill in
            createMapping.Start();
            createMapping.WaitForExit();
            var translateClient = new Process();
            translateClient.StartInfo.FileName = pathToTransformClient;
            translateClient.StartInfo.Arguments = ""; //TODO fill in
            translateClient.Start();
            translateClient.WaitForExit();
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
            verifyProject(Path.Combine(TestFolder, "clientC#", "Client", "Client.csproj"), expectedPath);
            verifyProject(Path.Combine(TestFolder, "clientVB", "Client", "Client.vbproj"), expectedPath);

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
