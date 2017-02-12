using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.BlackBox
{
    [TestClass]
    public class ExtraLibraryTest : BlackBoxTest
    {
        public override void PreSetup()
        {
            base.PreSetup();
            foreach (string dll in CompileSolution(Path.Combine(TestFolder, "extraLibrary", "extraLibrary.sln")))
            {
                File.Copy(dll, Path.Combine(TestFolder, "bin", Path.GetFileName(dll)));
            }
        }

        [TestMethod]
        [DeploymentItem("tests/extraLibrary", "extraLibrary")]
        public void TestExtraLibrary()
        {
            TestFolder = "extraLibrary";
            RunTest();
        }
    }
}
