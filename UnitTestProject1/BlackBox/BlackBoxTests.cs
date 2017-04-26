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
    public class BlackBoxAliasTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/alias", "MappingAlias")]
        public void TestBlackBoxMappingAlias()
        {
            TestFolder = "MappingAlias";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/alias", "PreTransformCSAlias")]
        public void TestBlackBoxPreTransformCSAlias()
        {
            TestFolder = "PreTransformCSAlias";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/alias", "PostTransformCSAlias")]
        public void TestBlackBoxPostTransformCSAlias()
        {
            TestFolder = "PostTransformCSAlias";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/alias", "PreTransformVBAlias")]
        public void TestBlackBoxPreTransformVBAlias()
        {
            TestFolder = "PreTransformVBAlias";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/alias", "PostTransformVBAlias")]
        public void TestBlackBoxPostTransformVBAlias()
        {
            TestFolder = "PostTransformVBAlias";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/alias", "EndToEndCSAlias")]
        public void TestBlackBoxEndToEndCSAlias()
        {
            TestFolder = "EndToEndCSAlias";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/alias", "EndToEndVBAlias")]
        public void TestBlackBoxEndToEndVBAlias()
        {
            TestFolder = "EndToEndVBAlias";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxAssemblyChangeTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/assemblyChange", "MappingAssemblyChange")]
        public void TestBlackBoxMappingAssemblyChange()
        {
            TestFolder = "MappingAssemblyChange";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyChange", "PreTransformCSAssemblyChange")]
        public void TestBlackBoxPreTransformCSAssemblyChange()
        {
            TestFolder = "PreTransformCSAssemblyChange";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyChange", "PostTransformCSAssemblyChange")]
        public void TestBlackBoxPostTransformCSAssemblyChange()
        {
            TestFolder = "PostTransformCSAssemblyChange";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyChange", "PreTransformVBAssemblyChange")]
        public void TestBlackBoxPreTransformVBAssemblyChange()
        {
            TestFolder = "PreTransformVBAssemblyChange";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyChange", "PostTransformVBAssemblyChange")]
        public void TestBlackBoxPostTransformVBAssemblyChange()
        {
            TestFolder = "PostTransformVBAssemblyChange";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyChange", "EndToEndCSAssemblyChange")]
        public void TestBlackBoxEndToEndCSAssemblyChange()
        {
            TestFolder = "EndToEndCSAssemblyChange";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyChange", "EndToEndVBAssemblyChange")]
        public void TestBlackBoxEndToEndVBAssemblyChange()
        {
            TestFolder = "EndToEndVBAssemblyChange";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxAssemblyMergeTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/assemblyMerge", "MappingAssemblyMerge")]
        public void TestBlackBoxMappingAssemblyMerge()
        {
            TestFolder = "MappingAssemblyMerge";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyMerge", "PreTransformCSAssemblyMerge")]
        public void TestBlackBoxPreTransformCSAssemblyMerge()
        {
            TestFolder = "PreTransformCSAssemblyMerge";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyMerge", "PostTransformCSAssemblyMerge")]
        public void TestBlackBoxPostTransformCSAssemblyMerge()
        {
            TestFolder = "PostTransformCSAssemblyMerge";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyMerge", "PreTransformVBAssemblyMerge")]
        public void TestBlackBoxPreTransformVBAssemblyMerge()
        {
            TestFolder = "PreTransformVBAssemblyMerge";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyMerge", "PostTransformVBAssemblyMerge")]
        public void TestBlackBoxPostTransformVBAssemblyMerge()
        {
            TestFolder = "PostTransformVBAssemblyMerge";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyMerge", "EndToEndCSAssemblyMerge")]
        public void TestBlackBoxEndToEndCSAssemblyMerge()
        {
            TestFolder = "EndToEndCSAssemblyMerge";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyMerge", "EndToEndVBAssemblyMerge")]
        public void TestBlackBoxEndToEndVBAssemblyMerge()
        {
            TestFolder = "EndToEndVBAssemblyMerge";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxAssemblySplitTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/assemblySplit", "MappingAssemblySplit")]
        public void TestBlackBoxMappingAssemblySplit()
        {
            TestFolder = "MappingAssemblySplit";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblySplit", "PreTransformCSAssemblySplit")]
        public void TestBlackBoxPreTransformCSAssemblySplit()
        {
            TestFolder = "PreTransformCSAssemblySplit";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblySplit", "PostTransformCSAssemblySplit")]
        public void TestBlackBoxPostTransformCSAssemblySplit()
        {
            TestFolder = "PostTransformCSAssemblySplit";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblySplit", "PreTransformVBAssemblySplit")]
        public void TestBlackBoxPreTransformVBAssemblySplit()
        {
            TestFolder = "PreTransformVBAssemblySplit";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblySplit", "PostTransformVBAssemblySplit")]
        public void TestBlackBoxPostTransformVBAssemblySplit()
        {
            TestFolder = "PostTransformVBAssemblySplit";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblySplit", "EndToEndCSAssemblySplit")]
        public void TestBlackBoxEndToEndCSAssemblySplit()
        {
            TestFolder = "EndToEndCSAssemblySplit";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblySplit", "EndToEndVBAssemblySplit")]
        public void TestBlackBoxEndToEndVBAssemblySplit()
        {
            TestFolder = "EndToEndVBAssemblySplit";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxBasicTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/basic", "MappingBasic")]
        public void TestBlackBoxMappingBasic()
        {
            TestFolder = "MappingBasic";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basic", "PreTransformCSBasic")]
        public void TestBlackBoxPreTransformCSBasic()
        {
            TestFolder = "PreTransformCSBasic";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basic", "PostTransformCSBasic")]
        public void TestBlackBoxPostTransformCSBasic()
        {
            TestFolder = "PostTransformCSBasic";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basic", "PreTransformVBBasic")]
        public void TestBlackBoxPreTransformVBBasic()
        {
            TestFolder = "PreTransformVBBasic";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basic", "PostTransformVBBasic")]
        public void TestBlackBoxPostTransformVBBasic()
        {
            TestFolder = "PostTransformVBBasic";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basic", "EndToEndCSBasic")]
        public void TestBlackBoxEndToEndCSBasic()
        {
            TestFolder = "EndToEndCSBasic";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basic", "EndToEndVBBasic")]
        public void TestBlackBoxEndToEndVBBasic()
        {
            TestFolder = "EndToEndVBBasic";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxBasicNamespaceTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/basicNamespace", "MappingBasicNamespace")]
        public void TestBlackBoxMappingBasicNamespace()
        {
            TestFolder = "MappingBasicNamespace";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basicNamespace", "PreTransformCSBasicNamespace")]
        public void TestBlackBoxPreTransformCSBasicNamespace()
        {
            TestFolder = "PreTransformCSBasicNamespace";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basicNamespace", "PostTransformCSBasicNamespace")]
        public void TestBlackBoxPostTransformCSBasicNamespace()
        {
            TestFolder = "PostTransformCSBasicNamespace";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basicNamespace", "PreTransformVBBasicNamespace")]
        public void TestBlackBoxPreTransformVBBasicNamespace()
        {
            TestFolder = "PreTransformVBBasicNamespace";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basicNamespace", "PostTransformVBBasicNamespace")]
        public void TestBlackBoxPostTransformVBBasicNamespace()
        {
            TestFolder = "PostTransformVBBasicNamespace";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basicNamespace", "EndToEndCSBasicNamespace")]
        public void TestBlackBoxEndToEndCSBasicNamespace()
        {
            TestFolder = "EndToEndCSBasicNamespace";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basicNamespace", "EndToEndVBBasicNamespace")]
        public void TestBlackBoxEndToEndVBBasicNamespace()
        {
            TestFolder = "EndToEndVBBasicNamespace";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxCastingTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/casting", "MappingCasting")]
        public void TestBlackBoxMappingCasting()
        {
            TestFolder = "MappingCasting";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/casting", "PreTransformCSCasting")]
        public void TestBlackBoxPreTransformCSCasting()
        {
            TestFolder = "PreTransformCSCasting";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/casting", "PostTransformCSCasting")]
        public void TestBlackBoxPostTransformCSCasting()
        {
            TestFolder = "PostTransformCSCasting";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/casting", "PreTransformVBCasting")]
        public void TestBlackBoxPreTransformVBCasting()
        {
            TestFolder = "PreTransformVBCasting";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/casting", "PostTransformVBCasting")]
        public void TestBlackBoxPostTransformVBCasting()
        {
            TestFolder = "PostTransformVBCasting";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/casting", "EndToEndCSCasting")]
        public void TestBlackBoxEndToEndCSCasting()
        {
            TestFolder = "EndToEndCSCasting";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/casting", "EndToEndVBCasting")]
        public void TestBlackBoxEndToEndVBCasting()
        {
            TestFolder = "EndToEndVBCasting";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxClassDeleteTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/classDelete", "MappingClassDelete")]
        public void TestBlackBoxMappingClassDelete()
        {
            TestFolder = "MappingClassDelete";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classDelete", "PreTransformCSClassDelete")]
        public void TestBlackBoxPreTransformCSClassDelete()
        {
            TestFolder = "PreTransformCSClassDelete";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classDelete", "PostTransformCSClassDelete")]
        public void TestBlackBoxPostTransformCSClassDelete()
        {
            TestFolder = "PostTransformCSClassDelete";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classDelete", "PreTransformVBClassDelete")]
        public void TestBlackBoxPreTransformVBClassDelete()
        {
            TestFolder = "PreTransformVBClassDelete";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classDelete", "PostTransformVBClassDelete")]
        public void TestBlackBoxPostTransformVBClassDelete()
        {
            TestFolder = "PostTransformVBClassDelete";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classDelete", "EndToEndCSClassDelete")]
        public void TestBlackBoxEndToEndCSClassDelete()
        {
            TestFolder = "EndToEndCSClassDelete";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classDelete", "EndToEndVBClassDelete")]
        public void TestBlackBoxEndToEndVBClassDelete()
        {
            TestFolder = "EndToEndVBClassDelete";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxClassInClassTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/classInClass", "MappingClassInClass")]
        public void TestBlackBoxMappingClassInClass()
        {
            TestFolder = "MappingClassInClass";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classInClass", "PreTransformCSClassInClass")]
        public void TestBlackBoxPreTransformCSClassInClass()
        {
            TestFolder = "PreTransformCSClassInClass";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classInClass", "PostTransformCSClassInClass")]
        public void TestBlackBoxPostTransformCSClassInClass()
        {
            TestFolder = "PostTransformCSClassInClass";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classInClass", "PreTransformVBClassInClass")]
        public void TestBlackBoxPreTransformVBClassInClass()
        {
            TestFolder = "PreTransformVBClassInClass";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classInClass", "PostTransformVBClassInClass")]
        public void TestBlackBoxPostTransformVBClassInClass()
        {
            TestFolder = "PostTransformVBClassInClass";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classInClass", "EndToEndCSClassInClass")]
        public void TestBlackBoxEndToEndCSClassInClass()
        {
            TestFolder = "EndToEndCSClassInClass";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classInClass", "EndToEndVBClassInClass")]
        public void TestBlackBoxEndToEndVBClassInClass()
        {
            TestFolder = "EndToEndVBClassInClass";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxEmptyDLLTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/emptyDLL", "MappingEmptyDLL")]
        public void TestBlackBoxMappingEmptyDLL()
        {
            TestFolder = "MappingEmptyDLL";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/emptyDLL", "PreTransformCSEmptyDLL")]
        public void TestBlackBoxPreTransformCSEmptyDLL()
        {
            TestFolder = "PreTransformCSEmptyDLL";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/emptyDLL", "PostTransformCSEmptyDLL")]
        public void TestBlackBoxPostTransformCSEmptyDLL()
        {
            TestFolder = "PostTransformCSEmptyDLL";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/emptyDLL", "PreTransformVBEmptyDLL")]
        public void TestBlackBoxPreTransformVBEmptyDLL()
        {
            TestFolder = "PreTransformVBEmptyDLL";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/emptyDLL", "PostTransformVBEmptyDLL")]
        public void TestBlackBoxPostTransformVBEmptyDLL()
        {
            TestFolder = "PostTransformVBEmptyDLL";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/emptyDLL", "EndToEndCSEmptyDLL")]
        public void TestBlackBoxEndToEndCSEmptyDLL()
        {
            TestFolder = "EndToEndCSEmptyDLL";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/emptyDLL", "EndToEndVBEmptyDLL")]
        public void TestBlackBoxEndToEndVBEmptyDLL()
        {
            TestFolder = "EndToEndVBEmptyDLL";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxExtendsSDKClassTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/extendsSDKClass", "MappingExtendsSDKClass")]
        public void TestBlackBoxMappingExtendsSDKClass()
        {
            TestFolder = "MappingExtendsSDKClass";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extendsSDKClass", "PreTransformCSExtendsSDKClass")]
        public void TestBlackBoxPreTransformCSExtendsSDKClass()
        {
            TestFolder = "PreTransformCSExtendsSDKClass";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extendsSDKClass", "PostTransformCSExtendsSDKClass")]
        public void TestBlackBoxPostTransformCSExtendsSDKClass()
        {
            TestFolder = "PostTransformCSExtendsSDKClass";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extendsSDKClass", "PreTransformVBExtendsSDKClass")]
        public void TestBlackBoxPreTransformVBExtendsSDKClass()
        {
            TestFolder = "PreTransformVBExtendsSDKClass";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extendsSDKClass", "PostTransformVBExtendsSDKClass")]
        public void TestBlackBoxPostTransformVBExtendsSDKClass()
        {
            TestFolder = "PostTransformVBExtendsSDKClass";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extendsSDKClass", "EndToEndCSExtendsSDKClass")]
        public void TestBlackBoxEndToEndCSExtendsSDKClass()
        {
            TestFolder = "EndToEndCSExtendsSDKClass";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extendsSDKClass", "EndToEndVBExtendsSDKClass")]
        public void TestBlackBoxEndToEndVBExtendsSDKClass()
        {
            TestFolder = "EndToEndVBExtendsSDKClass";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxExtraLibraryTests : BlackBoxBase
    {
        public override void CompileLibraries()
        {
            base.CompileLibraries();
            CompileSolution(Path.Combine(TestFolder, "extraLibrary", "extraLibrary.sln"));
            File.Copy(Path.Combine(TestFolder, "bin1", "extraLibrary.dll"),
                Path.Combine(TestFolder, "bin2", "extraLibrary.dll"));
        }
        [TestMethod]
        [DeploymentItem("tests/extraLibrary", "MappingExtraLibrary")]
        public void TestBlackBoxMappingExtraLibrary()
        {
            TestFolder = "MappingExtraLibrary";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extraLibrary", "PreTransformCSExtraLibrary")]
        public void TestBlackBoxPreTransformCSExtraLibrary()
        {
            TestFolder = "PreTransformCSExtraLibrary";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extraLibrary", "PostTransformCSExtraLibrary")]
        public void TestBlackBoxPostTransformCSExtraLibrary()
        {
            TestFolder = "PostTransformCSExtraLibrary";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extraLibrary", "PreTransformVBExtraLibrary")]
        public void TestBlackBoxPreTransformVBExtraLibrary()
        {
            TestFolder = "PreTransformVBExtraLibrary";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extraLibrary", "PostTransformVBExtraLibrary")]
        public void TestBlackBoxPostTransformVBExtraLibrary()
        {
            TestFolder = "PostTransformVBExtraLibrary";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extraLibrary", "EndToEndCSExtraLibrary")]
        public void TestBlackBoxEndToEndCSExtraLibrary()
        {
            TestFolder = "EndToEndCSExtraLibrary";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extraLibrary", "EndToEndVBExtraLibrary")]
        public void TestBlackBoxEndToEndVBExtraLibrary()
        {
            TestFolder = "EndToEndVBExtraLibrary";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxFullyQualifiedTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/fullyQualified", "MappingFullyQualified")]
        public void TestBlackBoxMappingFullyQualified()
        {
            TestFolder = "MappingFullyQualified";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualified", "PreTransformCSFullyQualified")]
        public void TestBlackBoxPreTransformCSFullyQualified()
        {
            TestFolder = "PreTransformCSFullyQualified";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualified", "PostTransformCSFullyQualified")]
        public void TestBlackBoxPostTransformCSFullyQualified()
        {
            TestFolder = "PostTransformCSFullyQualified";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualified", "PreTransformVBFullyQualified")]
        public void TestBlackBoxPreTransformVBFullyQualified()
        {
            TestFolder = "PreTransformVBFullyQualified";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualified", "PostTransformVBFullyQualified")]
        public void TestBlackBoxPostTransformVBFullyQualified()
        {
            TestFolder = "PostTransformVBFullyQualified";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualified", "EndToEndCSFullyQualified")]
        public void TestBlackBoxEndToEndCSFullyQualified()
        {
            TestFolder = "EndToEndCSFullyQualified";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualified", "EndToEndVBFullyQualified")]
        public void TestBlackBoxEndToEndVBFullyQualified()
        {
            TestFolder = "EndToEndVBFullyQualified";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxFullyQualifiedModelIdentifierTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/fullyQualifiedModelIdentifier", "MappingFullyQualifiedModelIdentifier")]
        public void TestBlackBoxMappingFullyQualifiedModelIdentifier()
        {
            TestFolder = "MappingFullyQualifiedModelIdentifier";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualifiedModelIdentifier", "PreTransformCSFullyQualifiedModelIdentifier")]
        public void TestBlackBoxPreTransformCSFullyQualifiedModelIdentifier()
        {
            TestFolder = "PreTransformCSFullyQualifiedModelIdentifier";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualifiedModelIdentifier", "PostTransformCSFullyQualifiedModelIdentifier")]
        public void TestBlackBoxPostTransformCSFullyQualifiedModelIdentifier()
        {
            TestFolder = "PostTransformCSFullyQualifiedModelIdentifier";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualifiedModelIdentifier", "PreTransformVBFullyQualifiedModelIdentifier")]
        public void TestBlackBoxPreTransformVBFullyQualifiedModelIdentifier()
        {
            TestFolder = "PreTransformVBFullyQualifiedModelIdentifier";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualifiedModelIdentifier", "PostTransformVBFullyQualifiedModelIdentifier")]
        public void TestBlackBoxPostTransformVBFullyQualifiedModelIdentifier()
        {
            TestFolder = "PostTransformVBFullyQualifiedModelIdentifier";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualifiedModelIdentifier", "EndToEndCSFullyQualifiedModelIdentifier")]
        public void TestBlackBoxEndToEndCSFullyQualifiedModelIdentifier()
        {
            TestFolder = "EndToEndCSFullyQualifiedModelIdentifier";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualifiedModelIdentifier", "EndToEndVBFullyQualifiedModelIdentifier")]
        public void TestBlackBoxEndToEndVBFullyQualifiedModelIdentifier()
        {
            TestFolder = "EndToEndVBFullyQualifiedModelIdentifier";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxGenericsTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/generics", "MappingGenerics")]
        public void TestBlackBoxMappingGenerics()
        {
            TestFolder = "MappingGenerics";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/generics", "PreTransformCSGenerics")]
        public void TestBlackBoxPreTransformCSGenerics()
        {
            TestFolder = "PreTransformCSGenerics";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/generics", "PostTransformCSGenerics")]
        public void TestBlackBoxPostTransformCSGenerics()
        {
            TestFolder = "PostTransformCSGenerics";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/generics", "PreTransformVBGenerics")]
        public void TestBlackBoxPreTransformVBGenerics()
        {
            TestFolder = "PreTransformVBGenerics";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/generics", "PostTransformVBGenerics")]
        public void TestBlackBoxPostTransformVBGenerics()
        {
            TestFolder = "PostTransformVBGenerics";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/generics", "EndToEndCSGenerics")]
        public void TestBlackBoxEndToEndCSGenerics()
        {
            TestFolder = "EndToEndCSGenerics";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/generics", "EndToEndVBGenerics")]
        public void TestBlackBoxEndToEndVBGenerics()
        {
            TestFolder = "EndToEndVBGenerics";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxInstantiatesSDKClassTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/instantiatesSDKClass", "MappingInstantiatesSDKClass")]
        public void TestBlackBoxMappingInstantiatesSDKClass()
        {
            TestFolder = "MappingInstantiatesSDKClass";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/instantiatesSDKClass", "PreTransformCSInstantiatesSDKClass")]
        public void TestBlackBoxPreTransformCSInstantiatesSDKClass()
        {
            TestFolder = "PreTransformCSInstantiatesSDKClass";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/instantiatesSDKClass", "PostTransformCSInstantiatesSDKClass")]
        public void TestBlackBoxPostTransformCSInstantiatesSDKClass()
        {
            TestFolder = "PostTransformCSInstantiatesSDKClass";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/instantiatesSDKClass", "PreTransformVBInstantiatesSDKClass")]
        public void TestBlackBoxPreTransformVBInstantiatesSDKClass()
        {
            TestFolder = "PreTransformVBInstantiatesSDKClass";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/instantiatesSDKClass", "PostTransformVBInstantiatesSDKClass")]
        public void TestBlackBoxPostTransformVBInstantiatesSDKClass()
        {
            TestFolder = "PostTransformVBInstantiatesSDKClass";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/instantiatesSDKClass", "EndToEndCSInstantiatesSDKClass")]
        public void TestBlackBoxEndToEndCSInstantiatesSDKClass()
        {
            TestFolder = "EndToEndCSInstantiatesSDKClass";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/instantiatesSDKClass", "EndToEndVBInstantiatesSDKClass")]
        public void TestBlackBoxEndToEndVBInstantiatesSDKClass()
        {
            TestFolder = "EndToEndVBInstantiatesSDKClass";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxMethodCallOnParameterTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/methodCallOnParameter", "MappingMethodCallOnParameter")]
        public void TestBlackBoxMappingMethodCallOnParameter()
        {
            TestFolder = "MappingMethodCallOnParameter";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/methodCallOnParameter", "PreTransformCSMethodCallOnParameter")]
        public void TestBlackBoxPreTransformCSMethodCallOnParameter()
        {
            TestFolder = "PreTransformCSMethodCallOnParameter";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/methodCallOnParameter", "PostTransformCSMethodCallOnParameter")]
        public void TestBlackBoxPostTransformCSMethodCallOnParameter()
        {
            TestFolder = "PostTransformCSMethodCallOnParameter";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/methodCallOnParameter", "PreTransformVBMethodCallOnParameter")]
        public void TestBlackBoxPreTransformVBMethodCallOnParameter()
        {
            TestFolder = "PreTransformVBMethodCallOnParameter";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/methodCallOnParameter", "PostTransformVBMethodCallOnParameter")]
        public void TestBlackBoxPostTransformVBMethodCallOnParameter()
        {
            TestFolder = "PostTransformVBMethodCallOnParameter";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/methodCallOnParameter", "EndToEndCSMethodCallOnParameter")]
        public void TestBlackBoxEndToEndCSMethodCallOnParameter()
        {
            TestFolder = "EndToEndCSMethodCallOnParameter";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/methodCallOnParameter", "EndToEndVBMethodCallOnParameter")]
        public void TestBlackBoxEndToEndVBMethodCallOnParameter()
        {
            TestFolder = "EndToEndVBMethodCallOnParameter";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxMultiAssemblyTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/multiAssembly", "MappingMultiAssembly")]
        public void TestBlackBoxMappingMultiAssembly()
        {
            TestFolder = "MappingMultiAssembly";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiAssembly", "PreTransformCSMultiAssembly")]
        public void TestBlackBoxPreTransformCSMultiAssembly()
        {
            TestFolder = "PreTransformCSMultiAssembly";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiAssembly", "PostTransformCSMultiAssembly")]
        public void TestBlackBoxPostTransformCSMultiAssembly()
        {
            TestFolder = "PostTransformCSMultiAssembly";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiAssembly", "PreTransformVBMultiAssembly")]
        public void TestBlackBoxPreTransformVBMultiAssembly()
        {
            TestFolder = "PreTransformVBMultiAssembly";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiAssembly", "PostTransformVBMultiAssembly")]
        public void TestBlackBoxPostTransformVBMultiAssembly()
        {
            TestFolder = "PostTransformVBMultiAssembly";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiAssembly", "EndToEndCSMultiAssembly")]
        public void TestBlackBoxEndToEndCSMultiAssembly()
        {
            TestFolder = "EndToEndCSMultiAssembly";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiAssembly", "EndToEndVBMultiAssembly")]
        public void TestBlackBoxEndToEndVBMultiAssembly()
        {
            TestFolder = "EndToEndVBMultiAssembly";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxMultiBasicTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/multiBasic", "MappingMultiBasic")]
        public void TestBlackBoxMappingMultiBasic()
        {
            TestFolder = "MappingMultiBasic";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiBasic", "PreTransformCSMultiBasic")]
        public void TestBlackBoxPreTransformCSMultiBasic()
        {
            TestFolder = "PreTransformCSMultiBasic";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiBasic", "PostTransformCSMultiBasic")]
        public void TestBlackBoxPostTransformCSMultiBasic()
        {
            TestFolder = "PostTransformCSMultiBasic";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiBasic", "PreTransformVBMultiBasic")]
        public void TestBlackBoxPreTransformVBMultiBasic()
        {
            TestFolder = "PreTransformVBMultiBasic";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiBasic", "PostTransformVBMultiBasic")]
        public void TestBlackBoxPostTransformVBMultiBasic()
        {
            TestFolder = "PostTransformVBMultiBasic";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiBasic", "EndToEndCSMultiBasic")]
        public void TestBlackBoxEndToEndCSMultiBasic()
        {
            TestFolder = "EndToEndCSMultiBasic";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiBasic", "EndToEndVBMultiBasic")]
        public void TestBlackBoxEndToEndVBMultiBasic()
        {
            TestFolder = "EndToEndVBMultiBasic";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxMultiTypeGenericTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/multiTypeGeneric", "MappingMultiTypeGeneric")]
        public void TestBlackBoxMappingMultiTypeGeneric()
        {
            TestFolder = "MappingMultiTypeGeneric";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiTypeGeneric", "PreTransformCSMultiTypeGeneric")]
        public void TestBlackBoxPreTransformCSMultiTypeGeneric()
        {
            TestFolder = "PreTransformCSMultiTypeGeneric";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiTypeGeneric", "PostTransformCSMultiTypeGeneric")]
        public void TestBlackBoxPostTransformCSMultiTypeGeneric()
        {
            TestFolder = "PostTransformCSMultiTypeGeneric";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiTypeGeneric", "PreTransformVBMultiTypeGeneric")]
        public void TestBlackBoxPreTransformVBMultiTypeGeneric()
        {
            TestFolder = "PreTransformVBMultiTypeGeneric";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiTypeGeneric", "PostTransformVBMultiTypeGeneric")]
        public void TestBlackBoxPostTransformVBMultiTypeGeneric()
        {
            TestFolder = "PostTransformVBMultiTypeGeneric";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiTypeGeneric", "EndToEndCSMultiTypeGeneric")]
        public void TestBlackBoxEndToEndCSMultiTypeGeneric()
        {
            TestFolder = "EndToEndCSMultiTypeGeneric";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiTypeGeneric", "EndToEndVBMultiTypeGeneric")]
        public void TestBlackBoxEndToEndVBMultiTypeGeneric()
        {
            TestFolder = "EndToEndVBMultiTypeGeneric";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxNamespaceInNamespaceTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/namespaceInNamespace", "MappingNamespaceInNamespace")]
        public void TestBlackBoxMappingNamespaceInNamespace()
        {
            TestFolder = "MappingNamespaceInNamespace";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceInNamespace", "PreTransformCSNamespaceInNamespace")]
        public void TestBlackBoxPreTransformCSNamespaceInNamespace()
        {
            TestFolder = "PreTransformCSNamespaceInNamespace";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceInNamespace", "PostTransformCSNamespaceInNamespace")]
        public void TestBlackBoxPostTransformCSNamespaceInNamespace()
        {
            TestFolder = "PostTransformCSNamespaceInNamespace";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceInNamespace", "PreTransformVBNamespaceInNamespace")]
        public void TestBlackBoxPreTransformVBNamespaceInNamespace()
        {
            TestFolder = "PreTransformVBNamespaceInNamespace";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceInNamespace", "PostTransformVBNamespaceInNamespace")]
        public void TestBlackBoxPostTransformVBNamespaceInNamespace()
        {
            TestFolder = "PostTransformVBNamespaceInNamespace";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceInNamespace", "EndToEndCSNamespaceInNamespace")]
        public void TestBlackBoxEndToEndCSNamespaceInNamespace()
        {
            TestFolder = "EndToEndCSNamespaceInNamespace";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceInNamespace", "EndToEndVBNamespaceInNamespace")]
        public void TestBlackBoxEndToEndVBNamespaceInNamespace()
        {
            TestFolder = "EndToEndVBNamespaceInNamespace";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxNamespaceMergeTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/namespaceMerge", "MappingNamespaceMerge")]
        public void TestBlackBoxMappingNamespaceMerge()
        {
            TestFolder = "MappingNamespaceMerge";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceMerge", "PreTransformCSNamespaceMerge")]
        public void TestBlackBoxPreTransformCSNamespaceMerge()
        {
            TestFolder = "PreTransformCSNamespaceMerge";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceMerge", "PostTransformCSNamespaceMerge")]
        public void TestBlackBoxPostTransformCSNamespaceMerge()
        {
            TestFolder = "PostTransformCSNamespaceMerge";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceMerge", "PreTransformVBNamespaceMerge")]
        public void TestBlackBoxPreTransformVBNamespaceMerge()
        {
            TestFolder = "PreTransformVBNamespaceMerge";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceMerge", "PostTransformVBNamespaceMerge")]
        public void TestBlackBoxPostTransformVBNamespaceMerge()
        {
            TestFolder = "PostTransformVBNamespaceMerge";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceMerge", "EndToEndCSNamespaceMerge")]
        public void TestBlackBoxEndToEndCSNamespaceMerge()
        {
            TestFolder = "EndToEndCSNamespaceMerge";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceMerge", "EndToEndVBNamespaceMerge")]
        public void TestBlackBoxEndToEndVBNamespaceMerge()
        {
            TestFolder = "EndToEndVBNamespaceMerge";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxNamespaceSplitTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/namespaceSplit", "MappingNamespaceSplit")]
        public void TestBlackBoxMappingNamespaceSplit()
        {
            TestFolder = "MappingNamespaceSplit";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplit", "PreTransformCSNamespaceSplit")]
        public void TestBlackBoxPreTransformCSNamespaceSplit()
        {
            TestFolder = "PreTransformCSNamespaceSplit";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplit", "PostTransformCSNamespaceSplit")]
        public void TestBlackBoxPostTransformCSNamespaceSplit()
        {
            TestFolder = "PostTransformCSNamespaceSplit";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplit", "PreTransformVBNamespaceSplit")]
        public void TestBlackBoxPreTransformVBNamespaceSplit()
        {
            TestFolder = "PreTransformVBNamespaceSplit";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplit", "PostTransformVBNamespaceSplit")]
        public void TestBlackBoxPostTransformVBNamespaceSplit()
        {
            TestFolder = "PostTransformVBNamespaceSplit";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplit", "EndToEndCSNamespaceSplit")]
        public void TestBlackBoxEndToEndCSNamespaceSplit()
        {
            TestFolder = "EndToEndCSNamespaceSplit";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplit", "EndToEndVBNamespaceSplit")]
        public void TestBlackBoxEndToEndVBNamespaceSplit()
        {
            TestFolder = "EndToEndVBNamespaceSplit";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxNamespaceSplitMergeTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/namespaceSplitMerge", "MappingNamespaceSplitMerge")]
        public void TestBlackBoxMappingNamespaceSplitMerge()
        {
            TestFolder = "MappingNamespaceSplitMerge";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplitMerge", "PreTransformCSNamespaceSplitMerge")]
        public void TestBlackBoxPreTransformCSNamespaceSplitMerge()
        {
            TestFolder = "PreTransformCSNamespaceSplitMerge";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplitMerge", "PostTransformCSNamespaceSplitMerge")]
        public void TestBlackBoxPostTransformCSNamespaceSplitMerge()
        {
            TestFolder = "PostTransformCSNamespaceSplitMerge";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplitMerge", "PreTransformVBNamespaceSplitMerge")]
        public void TestBlackBoxPreTransformVBNamespaceSplitMerge()
        {
            TestFolder = "PreTransformVBNamespaceSplitMerge";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplitMerge", "PostTransformVBNamespaceSplitMerge")]
        public void TestBlackBoxPostTransformVBNamespaceSplitMerge()
        {
            TestFolder = "PostTransformVBNamespaceSplitMerge";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplitMerge", "EndToEndCSNamespaceSplitMerge")]
        public void TestBlackBoxEndToEndCSNamespaceSplitMerge()
        {
            TestFolder = "EndToEndCSNamespaceSplitMerge";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplitMerge", "EndToEndVBNamespaceSplitMerge")]
        public void TestBlackBoxEndToEndVBNamespaceSplitMerge()
        {
            TestFolder = "EndToEndVBNamespaceSplitMerge";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxNewConflictsTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/newConflicts", "MappingNewConflicts")]
        public void TestBlackBoxMappingNewConflicts()
        {
            TestFolder = "MappingNewConflicts";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/newConflicts", "PreTransformCSNewConflicts")]
        public void TestBlackBoxPreTransformCSNewConflicts()
        {
            TestFolder = "PreTransformCSNewConflicts";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/newConflicts", "PostTransformCSNewConflicts")]
        public void TestBlackBoxPostTransformCSNewConflicts()
        {
            TestFolder = "PostTransformCSNewConflicts";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/newConflicts", "PreTransformVBNewConflicts")]
        public void TestBlackBoxPreTransformVBNewConflicts()
        {
            TestFolder = "PreTransformVBNewConflicts";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/newConflicts", "PostTransformVBNewConflicts")]
        public void TestBlackBoxPostTransformVBNewConflicts()
        {
            TestFolder = "PostTransformVBNewConflicts";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/newConflicts", "EndToEndCSNewConflicts")]
        public void TestBlackBoxEndToEndCSNewConflicts()
        {
            TestFolder = "EndToEndCSNewConflicts";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/newConflicts", "EndToEndVBNewConflicts")]
        public void TestBlackBoxEndToEndVBNewConflicts()
        {
            TestFolder = "EndToEndVBNewConflicts";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxNonRootUsingTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/nonRootUsing", "MappingNonRootUsing")]
        public void TestBlackBoxMappingNonRootUsing()
        {
            TestFolder = "MappingNonRootUsing";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nonRootUsing", "PreTransformCSNonRootUsing")]
        public void TestBlackBoxPreTransformCSNonRootUsing()
        {
            TestFolder = "PreTransformCSNonRootUsing";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nonRootUsing", "PostTransformCSNonRootUsing")]
        public void TestBlackBoxPostTransformCSNonRootUsing()
        {
            TestFolder = "PostTransformCSNonRootUsing";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nonRootUsing", "PreTransformVBNonRootUsing")]
        public void TestBlackBoxPreTransformVBNonRootUsing()
        {
            TestFolder = "PreTransformVBNonRootUsing";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nonRootUsing", "PostTransformVBNonRootUsing")]
        public void TestBlackBoxPostTransformVBNonRootUsing()
        {
            TestFolder = "PostTransformVBNonRootUsing";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nonRootUsing", "EndToEndCSNonRootUsing")]
        public void TestBlackBoxEndToEndCSNonRootUsing()
        {
            TestFolder = "EndToEndCSNonRootUsing";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nonRootUsing", "EndToEndVBNonRootUsing")]
        public void TestBlackBoxEndToEndVBNonRootUsing()
        {
            TestFolder = "EndToEndVBNonRootUsing";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxNothingTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/nothing", "MappingNothing")]
        public void TestBlackBoxMappingNothing()
        {
            TestFolder = "MappingNothing";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nothing", "PreTransformCSNothing")]
        public void TestBlackBoxPreTransformCSNothing()
        {
            TestFolder = "PreTransformCSNothing";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nothing", "PostTransformCSNothing")]
        public void TestBlackBoxPostTransformCSNothing()
        {
            TestFolder = "PostTransformCSNothing";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nothing", "PreTransformVBNothing")]
        public void TestBlackBoxPreTransformVBNothing()
        {
            TestFolder = "PreTransformVBNothing";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nothing", "PostTransformVBNothing")]
        public void TestBlackBoxPostTransformVBNothing()
        {
            TestFolder = "PostTransformVBNothing";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nothing", "EndToEndCSNothing")]
        public void TestBlackBoxEndToEndCSNothing()
        {
            TestFolder = "EndToEndCSNothing";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nothing", "EndToEndVBNothing")]
        public void TestBlackBoxEndToEndVBNothing()
        {
            TestFolder = "EndToEndVBNothing";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxOldConflictsTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/oldConflicts", "MappingOldConflicts")]
        public void TestBlackBoxMappingOldConflicts()
        {
            TestFolder = "MappingOldConflicts";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/oldConflicts", "PreTransformCSOldConflicts")]
        public void TestBlackBoxPreTransformCSOldConflicts()
        {
            TestFolder = "PreTransformCSOldConflicts";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/oldConflicts", "PostTransformCSOldConflicts")]
        public void TestBlackBoxPostTransformCSOldConflicts()
        {
            TestFolder = "PostTransformCSOldConflicts";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/oldConflicts", "PreTransformVBOldConflicts")]
        public void TestBlackBoxPreTransformVBOldConflicts()
        {
            TestFolder = "PreTransformVBOldConflicts";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/oldConflicts", "PostTransformVBOldConflicts")]
        public void TestBlackBoxPostTransformVBOldConflicts()
        {
            TestFolder = "PostTransformVBOldConflicts";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/oldConflicts", "EndToEndCSOldConflicts")]
        public void TestBlackBoxEndToEndCSOldConflicts()
        {
            TestFolder = "EndToEndCSOldConflicts";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/oldConflicts", "EndToEndVBOldConflicts")]
        public void TestBlackBoxEndToEndVBOldConflicts()
        {
            TestFolder = "EndToEndVBOldConflicts";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxReturnClassTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/returnClass", "MappingReturnClass")]
        public void TestBlackBoxMappingReturnClass()
        {
            TestFolder = "MappingReturnClass";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/returnClass", "PreTransformCSReturnClass")]
        public void TestBlackBoxPreTransformCSReturnClass()
        {
            TestFolder = "PreTransformCSReturnClass";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/returnClass", "PostTransformCSReturnClass")]
        public void TestBlackBoxPostTransformCSReturnClass()
        {
            TestFolder = "PostTransformCSReturnClass";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/returnClass", "PreTransformVBReturnClass")]
        public void TestBlackBoxPreTransformVBReturnClass()
        {
            TestFolder = "PreTransformVBReturnClass";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/returnClass", "PostTransformVBReturnClass")]
        public void TestBlackBoxPostTransformVBReturnClass()
        {
            TestFolder = "PostTransformVBReturnClass";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/returnClass", "EndToEndCSReturnClass")]
        public void TestBlackBoxEndToEndCSReturnClass()
        {
            TestFolder = "EndToEndCSReturnClass";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/returnClass", "EndToEndVBReturnClass")]
        public void TestBlackBoxEndToEndVBReturnClass()
        {
            TestFolder = "EndToEndVBReturnClass";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxTypeofTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/typeof", "MappingTypeof")]
        public void TestBlackBoxMappingTypeof()
        {
            TestFolder = "MappingTypeof";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/typeof", "PreTransformCSTypeof")]
        public void TestBlackBoxPreTransformCSTypeof()
        {
            TestFolder = "PreTransformCSTypeof";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/typeof", "PostTransformCSTypeof")]
        public void TestBlackBoxPostTransformCSTypeof()
        {
            TestFolder = "PostTransformCSTypeof";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/typeof", "PreTransformVBTypeof")]
        public void TestBlackBoxPreTransformVBTypeof()
        {
            TestFolder = "PreTransformVBTypeof";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/typeof", "PostTransformVBTypeof")]
        public void TestBlackBoxPostTransformVBTypeof()
        {
            TestFolder = "PostTransformVBTypeof";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/typeof", "EndToEndCSTypeof")]
        public void TestBlackBoxEndToEndCSTypeof()
        {
            TestFolder = "EndToEndCSTypeof";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/typeof", "EndToEndVBTypeof")]
        public void TestBlackBoxEndToEndVBTypeof()
        {
            TestFolder = "EndToEndVBTypeof";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxUnusedTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/unused", "MappingUnused")]
        public void TestBlackBoxMappingUnused()
        {
            TestFolder = "MappingUnused";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/unused", "PreTransformCSUnused")]
        public void TestBlackBoxPreTransformCSUnused()
        {
            TestFolder = "PreTransformCSUnused";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/unused", "PostTransformCSUnused")]
        public void TestBlackBoxPostTransformCSUnused()
        {
            TestFolder = "PostTransformCSUnused";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/unused", "PreTransformVBUnused")]
        public void TestBlackBoxPreTransformVBUnused()
        {
            TestFolder = "PreTransformVBUnused";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/unused", "PostTransformVBUnused")]
        public void TestBlackBoxPostTransformVBUnused()
        {
            TestFolder = "PostTransformVBUnused";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/unused", "EndToEndCSUnused")]
        public void TestBlackBoxEndToEndCSUnused()
        {
            TestFolder = "EndToEndCSUnused";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/unused", "EndToEndVBUnused")]
        public void TestBlackBoxEndToEndVBUnused()
        {
            TestFolder = "EndToEndVBUnused";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BlackBoxVariablesTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/variables", "MappingVariables")]
        public void TestBlackBoxMappingVariables()
        {
            TestFolder = "MappingVariables";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/variables", "PreTransformCSVariables")]
        public void TestBlackBoxPreTransformCSVariables()
        {
            TestFolder = "PreTransformCSVariables";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/variables", "PostTransformCSVariables")]
        public void TestBlackBoxPostTransformCSVariables()
        {
            TestFolder = "PostTransformCSVariables";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/variables", "PreTransformVBVariables")]
        public void TestBlackBoxPreTransformVBVariables()
        {
            TestFolder = "PreTransformVBVariables";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/variables", "PostTransformVBVariables")]
        public void TestBlackBoxPostTransformVBVariables()
        {
            TestFolder = "PostTransformVBVariables";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/variables", "EndToEndCSVariables")]
        public void TestBlackBoxEndToEndCSVariables()
        {
            TestFolder = "EndToEndCSVariables";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/variables", "EndToEndVBVariables")]
        public void TestBlackBoxEndToEndVBVariables()
        {
            TestFolder = "EndToEndVBVariables";
            RunEndToEndVBTest();
        }
    }


}
