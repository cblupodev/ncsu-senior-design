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
        static string pathToCreateMappings = @""; //TODO fill in
        static string pathToTransformClient = @""; //TODO fill in

        public string CompileProject(Project proj)
        {
            var comp = proj.GetCompilationAsync().Result;
            if (File.Exists(proj.OutputFilePath))
                File.Delete(proj.OutputFilePath);
            comp.Emit(proj.OutputFilePath);
            var solnDir = new FileInfo(proj.FilePath).Directory.Parent;
            var outDir = new FileInfo(proj.OutputFilePath).Directory;
            foreach (MetadataReference reference in comp.ExternalReferences)
            {
                if (reference is PortableExecutableReference)
                {
                    var refPath = new FileInfo(((PortableExecutableReference)reference).FilePath);
                    if ( refPath.Directory.FullName.StartsWith(solnDir.FullName))
                    {
                        // this reference is inside of the solution directory
                        // so we probably need to include it in the output directory
                        File.Copy(refPath.FullName, Path.Combine(outDir.FullName, refPath.Name), true);
                    }
                }
            }
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

        public void CleanProject(string projectPath)
        {
            var proj = MSBuildWorkspace.Create().OpenProjectAsync(projectPath).Result;
            Directory.Delete(new FileInfo(proj.OutputFilePath).Directory.FullName, true);
        }

        public void VerifyProject(string projectPath, string expectedPath)
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
                var csFile = Path.Combine(TestFolder, "clientC#", "libs", Path.GetFileName(dll));
                var vbFile = Path.Combine(TestFolder, "clientVB", "libs", Path.GetFileName(dll));
                File.Copy(dll, csFile);
                File.Copy(dll, vbFile);
                toRemove.AddLast(csFile);
                toRemove.AddLast(vbFile);
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
            VerifyProject(Path.Combine(TestFolder, "clientC#", "Client", "Client.csproj"), expectedPath);
            VerifyProject(Path.Combine(TestFolder, "clientVB", "Client", "Client.vbproj"), expectedPath);
        }

        public virtual void Setup()
        {
            foreach (string dll in dllsToRemove)
            {
                File.Delete(dll);
            }
            CleanProject(Path.Combine(TestFolder, "clientC#", "Client", "Client.csproj"));
            CleanProject(Path.Combine(TestFolder, "clientVB", "Client", "Client.vbproj"));

            foreach (string dll in CompileSolution(Path.Combine(TestFolder, "newSDK", "SDK.sln")))
            {
                File.Copy(dll, Path.Combine(TestFolder, "clientC#", "libs", Path.GetFileName(dll)));
                File.Copy(dll, Path.Combine(TestFolder, "clientVB", "libs", Path.GetFileName(dll)));
            }
        }

        public virtual void RunProgram()
        {
            var createMapping = new Process();
            createMapping.StartInfo.FileName = pathToCreateMappings;
            createMapping.StartInfo.Arguments = ""; //TODO fill in
            createMapping.Start();
            createMapping.WaitForExit();
            Assert.AreEqual(0, createMapping.ExitCode);
            var translateClient = new Process();
            translateClient.StartInfo.FileName = pathToTransformClient;
            translateClient.StartInfo.Arguments = ""; //TODO fill in
            translateClient.Start();
            translateClient.WaitForExit();
            Assert.AreEqual(0, translateClient.ExitCode);
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
            VerifyProject(Path.Combine(TestFolder, "clientC#", "Client", "Client.csproj"), expectedPath);
            VerifyProject(Path.Combine(TestFolder, "clientVB", "Client", "Client.vbproj"), expectedPath);

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
