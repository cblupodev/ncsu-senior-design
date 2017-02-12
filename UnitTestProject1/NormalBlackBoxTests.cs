using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.BlackBox
{
    [TestClass]
    public class NormalBlackBoxTests : BlackBoxTest
    {
        [TestMethod]
        [DeploymentItem("tests/alias", "alias")]
        public void TestAlias()
        {
            TestFolder = "alias";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/assemblyChange", "assemblyChange")]
        public void TestAssemblyChange()
        {
            TestFolder = "assemblyChange";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/assemblyMerge", "assemblyMerge")]
        public void TestAssemblyMerge()
        {
            TestFolder = "assemblyMerge";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/assemblySplit", "assemblySplit")]
        public void TestAssemblySplit()
        {
            TestFolder = "assemblySplit";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/basic", "basic")]
        public void TestBasic()
        {
            TestFolder = "basic";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/basicNamespace", "basicNamespace")]
        public void TestBasicNamespace()
        {
            TestFolder = "basicNamespace";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/casting", "casting")]
        public void TestCasting()
        {
            TestFolder = "casting";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/classInClass", "classInClass")]
        public void TestClassInClass()
        {
            TestFolder = "classInClass";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/extendsSDKClass", "extendsSDKClass")]
        public void TestExtendsSDKClass()
        {
            TestFolder = "extendsSDKClass";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/fullyQualified", "fullyQualified")]
        public void TestFullyQualified()
        {
            TestFolder = "fullyQualified";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/fullyQualifiedModelIdentifier", "fullyQualifiedModelIdentifier")]
        public void TestFullyQualifiedModelIdentifier()
        {
            TestFolder = "fullyQualifiedModelIdentifier";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/instantiatesSDKClass", "instantiatesSDKClass")]
        public void TestInstantiatesSDKClass()
        {
            TestFolder = "instantiatesSDKClass";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/multiAssembly", "multiAssembly")]
        public void TestMultiAssembly()
        {
            TestFolder = "multiAssembly";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/multiBasic", "multiBasic")]
        public void TestMultiBasic()
        {
            TestFolder = "multiBasic";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/namespaceInNamespace", "namespaceInNamespace")]
        public void TestNamespaceInNamespace()
        {
            TestFolder = "namespaceInNamespace";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/newConflicts", "newConflicts")]
        public void TestNewConflicts()
        {
            TestFolder = "newConflicts";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/nonRootUsing", "nonRootUsing")]
        public void TestNonRootUsing()
        {
            TestFolder = "nonRootUsing";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/nothing", "nothing")]
        public void TestNothing()
        {
            TestFolder = "nothing";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/oldConflicts", "oldConflicts")]
        public void TestOldConflicts()
        {
            TestFolder = "oldConflicts";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/typeof", "typeof")]
        public void TestTypeof()
        {
            TestFolder = "typeof";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/unused", "unused")]
        public void TestUnused()
        {
            TestFolder = "unused";
            RunTest();
        }
    }
}
