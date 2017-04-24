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
    [TestClass]
    public class FujitsuSuppliedTests : BlackBoxBase
    {
        private void VerifyProjectNoExecute(string projectPath)
        {
            var expectedReferences = System.Text.RegularExpressions.Regex.Matches(File.ReadAllText(projectPath),
                "<Reference").Count;
            expectedReferences += 1; // for the auto-included refernce to system libraries "mscorlib.dll"
            if (projectPath.EndsWith(".vbproj"))
            {
                expectedReferences += 1; // for the auto-included visual basic libraries ""Microsoft.VisualBasic.dll"
            }
            var proj = MSBuildWorkspace.Create().OpenProjectAsync(projectPath).Result;
            Assert.AreEqual(expectedReferences, proj.MetadataReferences.Count, "Incorrect number of references, some are probably repeated or missing");
            var result = CompileProject(proj);
        }

        public override void RunMapping()
        {
#if DEBUGABLE_EXECUTION
            CreateMappings.ReadProject.Main(new[] { Path.Combine(TestFolder, "Main", "bin"),
                Path.Combine(TestFolder, "Main2", "bin"), SdkNameId });
#else
            var createMapping = new Process();
            createMapping.StartInfo.FileName = pathToCreateMappings;
            createMapping.StartInfo.Arguments = "\"" + Path.Combine(TestFolder, "Main", "bin") + "\" \"" +
                Path.Combine(TestFolder, "Main2", "bin") + "\" \"" + SdkNameId + "\"";
            createMapping.StartInfo.UseShellExecute = false;
            createMapping.StartInfo.RedirectStandardOutput = true;
            createMapping.StartInfo.RedirectStandardError = true;
            createMapping.Start();
            Trace.WriteLine("------------ Started create mappings with arguments: " + createMapping.StartInfo.Arguments);
            Trace.WriteLine("------------ Start create mappings standard output");
            while (!createMapping.StandardOutput.EndOfStream)
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
        [TestMethod]
        [DeploymentItem("specialTests/fujitsuProvided", "PreTransformFujitsuSupplied")]
        public void TestPreTransformFujitsuSupplied()
        {
            TestFolder = "PreTransformFujitsuSupplied";
            projectUnderTest = Path.Combine(TestFolder, "two_up", "one_up", "ConversionTest", "ConversionTest.csproj");
            VerifyProjectNoExecute(projectUnderTest);
        }
        [TestMethod]
        [DeploymentItem("specialTests/fujitsuProvided", "EndToEndFujitsuSupplied")]
        public void TestEndToEndFujitsuSupplied()
        {
            TestFolder = "EndToEndFujitsuSupplied";
            projectUnderTest = Path.Combine(TestFolder, "two_up", "one_up", "ConversionTest", "ConversionTest.csproj");
            ResetDatabase();
            RunMapping();
            ProcessPostTransformTest();
            Directory.Delete(Path.Combine(TestFolder, "Main"), true);
            VerifyProjectNoExecute(Path.Combine(Directory.GetParent(projectUnderTest).FullName + "_transformed",
                Path.GetFileName(projectUnderTest)));
        }
    }
}
