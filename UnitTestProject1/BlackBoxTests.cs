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
    public class AliasTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/alias", "MappingAlias")]
        public void TestMappingAlias()
        {
            TestFolder = "MappingAlias";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/alias", "PreTransformCSAlias")]
        public void TestPreTransformCSAlias()
        {
            TestFolder = "PreTransformCSAlias";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/alias", "PostTransformCSAlias")]
        public void TestPostTransformCSAlias()
        {
            TestFolder = "PostTransformCSAlias";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/alias", "PreTransformVBAlias")]
        public void TestPreTransformVBAlias()
        {
            TestFolder = "PreTransformVBAlias";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/alias", "PostTransformVBAlias")]
        public void TestPostTransformVBAlias()
        {
            TestFolder = "PostTransformVBAlias";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/alias", "EndToEndCSAlias")]
        public void TestEndToEndCSAlias()
        {
            TestFolder = "EndToEndCSAlias";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/alias", "EndToEndVBAlias")]
        public void TestEndToEndVBAlias()
        {
            TestFolder = "EndToEndVBAlias";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class AssemblyChangeTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/assemblyChange", "MappingAssemblyChange")]
        public void TestMappingAssemblyChange()
        {
            TestFolder = "MappingAssemblyChange";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyChange", "PreTransformCSAssemblyChange")]
        public void TestPreTransformCSAssemblyChange()
        {
            TestFolder = "PreTransformCSAssemblyChange";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyChange", "PostTransformCSAssemblyChange")]
        public void TestPostTransformCSAssemblyChange()
        {
            TestFolder = "PostTransformCSAssemblyChange";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyChange", "PreTransformVBAssemblyChange")]
        public void TestPreTransformVBAssemblyChange()
        {
            TestFolder = "PreTransformVBAssemblyChange";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyChange", "PostTransformVBAssemblyChange")]
        public void TestPostTransformVBAssemblyChange()
        {
            TestFolder = "PostTransformVBAssemblyChange";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyChange", "EndToEndCSAssemblyChange")]
        public void TestEndToEndCSAssemblyChange()
        {
            TestFolder = "EndToEndCSAssemblyChange";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyChange", "EndToEndVBAssemblyChange")]
        public void TestEndToEndVBAssemblyChange()
        {
            TestFolder = "EndToEndVBAssemblyChange";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class AssemblyMergeTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/assemblyMerge", "MappingAssemblyMerge")]
        public void TestMappingAssemblyMerge()
        {
            TestFolder = "MappingAssemblyMerge";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyMerge", "PreTransformCSAssemblyMerge")]
        public void TestPreTransformCSAssemblyMerge()
        {
            TestFolder = "PreTransformCSAssemblyMerge";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyMerge", "PostTransformCSAssemblyMerge")]
        public void TestPostTransformCSAssemblyMerge()
        {
            TestFolder = "PostTransformCSAssemblyMerge";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyMerge", "PreTransformVBAssemblyMerge")]
        public void TestPreTransformVBAssemblyMerge()
        {
            TestFolder = "PreTransformVBAssemblyMerge";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyMerge", "PostTransformVBAssemblyMerge")]
        public void TestPostTransformVBAssemblyMerge()
        {
            TestFolder = "PostTransformVBAssemblyMerge";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyMerge", "EndToEndCSAssemblyMerge")]
        public void TestEndToEndCSAssemblyMerge()
        {
            TestFolder = "EndToEndCSAssemblyMerge";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblyMerge", "EndToEndVBAssemblyMerge")]
        public void TestEndToEndVBAssemblyMerge()
        {
            TestFolder = "EndToEndVBAssemblyMerge";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class AssemblySplitTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/assemblySplit", "MappingAssemblySplit")]
        public void TestMappingAssemblySplit()
        {
            TestFolder = "MappingAssemblySplit";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblySplit", "PreTransformCSAssemblySplit")]
        public void TestPreTransformCSAssemblySplit()
        {
            TestFolder = "PreTransformCSAssemblySplit";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblySplit", "PostTransformCSAssemblySplit")]
        public void TestPostTransformCSAssemblySplit()
        {
            TestFolder = "PostTransformCSAssemblySplit";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblySplit", "PreTransformVBAssemblySplit")]
        public void TestPreTransformVBAssemblySplit()
        {
            TestFolder = "PreTransformVBAssemblySplit";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblySplit", "PostTransformVBAssemblySplit")]
        public void TestPostTransformVBAssemblySplit()
        {
            TestFolder = "PostTransformVBAssemblySplit";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblySplit", "EndToEndCSAssemblySplit")]
        public void TestEndToEndCSAssemblySplit()
        {
            TestFolder = "EndToEndCSAssemblySplit";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/assemblySplit", "EndToEndVBAssemblySplit")]
        public void TestEndToEndVBAssemblySplit()
        {
            TestFolder = "EndToEndVBAssemblySplit";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BasicTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/basic", "MappingBasic")]
        public void TestMappingBasic()
        {
            TestFolder = "MappingBasic";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basic", "PreTransformCSBasic")]
        public void TestPreTransformCSBasic()
        {
            TestFolder = "PreTransformCSBasic";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basic", "PostTransformCSBasic")]
        public void TestPostTransformCSBasic()
        {
            TestFolder = "PostTransformCSBasic";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basic", "PreTransformVBBasic")]
        public void TestPreTransformVBBasic()
        {
            TestFolder = "PreTransformVBBasic";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basic", "PostTransformVBBasic")]
        public void TestPostTransformVBBasic()
        {
            TestFolder = "PostTransformVBBasic";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basic", "EndToEndCSBasic")]
        public void TestEndToEndCSBasic()
        {
            TestFolder = "EndToEndCSBasic";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basic", "EndToEndVBBasic")]
        public void TestEndToEndVBBasic()
        {
            TestFolder = "EndToEndVBBasic";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class BasicNamespaceTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/basicNamespace", "MappingBasicNamespace")]
        public void TestMappingBasicNamespace()
        {
            TestFolder = "MappingBasicNamespace";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basicNamespace", "PreTransformCSBasicNamespace")]
        public void TestPreTransformCSBasicNamespace()
        {
            TestFolder = "PreTransformCSBasicNamespace";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basicNamespace", "PostTransformCSBasicNamespace")]
        public void TestPostTransformCSBasicNamespace()
        {
            TestFolder = "PostTransformCSBasicNamespace";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basicNamespace", "PreTransformVBBasicNamespace")]
        public void TestPreTransformVBBasicNamespace()
        {
            TestFolder = "PreTransformVBBasicNamespace";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basicNamespace", "PostTransformVBBasicNamespace")]
        public void TestPostTransformVBBasicNamespace()
        {
            TestFolder = "PostTransformVBBasicNamespace";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basicNamespace", "EndToEndCSBasicNamespace")]
        public void TestEndToEndCSBasicNamespace()
        {
            TestFolder = "EndToEndCSBasicNamespace";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/basicNamespace", "EndToEndVBBasicNamespace")]
        public void TestEndToEndVBBasicNamespace()
        {
            TestFolder = "EndToEndVBBasicNamespace";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class CastingTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/casting", "MappingCasting")]
        public void TestMappingCasting()
        {
            TestFolder = "MappingCasting";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/casting", "PreTransformCSCasting")]
        public void TestPreTransformCSCasting()
        {
            TestFolder = "PreTransformCSCasting";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/casting", "PostTransformCSCasting")]
        public void TestPostTransformCSCasting()
        {
            TestFolder = "PostTransformCSCasting";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/casting", "PreTransformVBCasting")]
        public void TestPreTransformVBCasting()
        {
            TestFolder = "PreTransformVBCasting";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/casting", "PostTransformVBCasting")]
        public void TestPostTransformVBCasting()
        {
            TestFolder = "PostTransformVBCasting";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/casting", "EndToEndCSCasting")]
        public void TestEndToEndCSCasting()
        {
            TestFolder = "EndToEndCSCasting";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/casting", "EndToEndVBCasting")]
        public void TestEndToEndVBCasting()
        {
            TestFolder = "EndToEndVBCasting";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class ClassDeleteTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/classDelete", "MappingClassDelete")]
        public void TestMappingClassDelete()
        {
            TestFolder = "MappingClassDelete";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classDelete", "PreTransformCSClassDelete")]
        public void TestPreTransformCSClassDelete()
        {
            TestFolder = "PreTransformCSClassDelete";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classDelete", "PostTransformCSClassDelete")]
        public void TestPostTransformCSClassDelete()
        {
            TestFolder = "PostTransformCSClassDelete";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classDelete", "PreTransformVBClassDelete")]
        public void TestPreTransformVBClassDelete()
        {
            TestFolder = "PreTransformVBClassDelete";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classDelete", "PostTransformVBClassDelete")]
        public void TestPostTransformVBClassDelete()
        {
            TestFolder = "PostTransformVBClassDelete";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classDelete", "EndToEndCSClassDelete")]
        public void TestEndToEndCSClassDelete()
        {
            TestFolder = "EndToEndCSClassDelete";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classDelete", "EndToEndVBClassDelete")]
        public void TestEndToEndVBClassDelete()
        {
            TestFolder = "EndToEndVBClassDelete";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class ClassInClassTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/classInClass", "MappingClassInClass")]
        public void TestMappingClassInClass()
        {
            TestFolder = "MappingClassInClass";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classInClass", "PreTransformCSClassInClass")]
        public void TestPreTransformCSClassInClass()
        {
            TestFolder = "PreTransformCSClassInClass";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classInClass", "PostTransformCSClassInClass")]
        public void TestPostTransformCSClassInClass()
        {
            TestFolder = "PostTransformCSClassInClass";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classInClass", "PreTransformVBClassInClass")]
        public void TestPreTransformVBClassInClass()
        {
            TestFolder = "PreTransformVBClassInClass";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classInClass", "PostTransformVBClassInClass")]
        public void TestPostTransformVBClassInClass()
        {
            TestFolder = "PostTransformVBClassInClass";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classInClass", "EndToEndCSClassInClass")]
        public void TestEndToEndCSClassInClass()
        {
            TestFolder = "EndToEndCSClassInClass";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/classInClass", "EndToEndVBClassInClass")]
        public void TestEndToEndVBClassInClass()
        {
            TestFolder = "EndToEndVBClassInClass";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class EmptyDLLTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/emptyDLL", "MappingEmptyDLL")]
        public void TestMappingEmptyDLL()
        {
            TestFolder = "MappingEmptyDLL";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/emptyDLL", "PreTransformCSEmptyDLL")]
        public void TestPreTransformCSEmptyDLL()
        {
            TestFolder = "PreTransformCSEmptyDLL";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/emptyDLL", "PostTransformCSEmptyDLL")]
        public void TestPostTransformCSEmptyDLL()
        {
            TestFolder = "PostTransformCSEmptyDLL";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/emptyDLL", "PreTransformVBEmptyDLL")]
        public void TestPreTransformVBEmptyDLL()
        {
            TestFolder = "PreTransformVBEmptyDLL";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/emptyDLL", "PostTransformVBEmptyDLL")]
        public void TestPostTransformVBEmptyDLL()
        {
            TestFolder = "PostTransformVBEmptyDLL";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/emptyDLL", "EndToEndCSEmptyDLL")]
        public void TestEndToEndCSEmptyDLL()
        {
            TestFolder = "EndToEndCSEmptyDLL";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/emptyDLL", "EndToEndVBEmptyDLL")]
        public void TestEndToEndVBEmptyDLL()
        {
            TestFolder = "EndToEndVBEmptyDLL";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class ExtendsSDKClassTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/extendsSDKClass", "MappingExtendsSDKClass")]
        public void TestMappingExtendsSDKClass()
        {
            TestFolder = "MappingExtendsSDKClass";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extendsSDKClass", "PreTransformCSExtendsSDKClass")]
        public void TestPreTransformCSExtendsSDKClass()
        {
            TestFolder = "PreTransformCSExtendsSDKClass";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extendsSDKClass", "PostTransformCSExtendsSDKClass")]
        public void TestPostTransformCSExtendsSDKClass()
        {
            TestFolder = "PostTransformCSExtendsSDKClass";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extendsSDKClass", "PreTransformVBExtendsSDKClass")]
        public void TestPreTransformVBExtendsSDKClass()
        {
            TestFolder = "PreTransformVBExtendsSDKClass";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extendsSDKClass", "PostTransformVBExtendsSDKClass")]
        public void TestPostTransformVBExtendsSDKClass()
        {
            TestFolder = "PostTransformVBExtendsSDKClass";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extendsSDKClass", "EndToEndCSExtendsSDKClass")]
        public void TestEndToEndCSExtendsSDKClass()
        {
            TestFolder = "EndToEndCSExtendsSDKClass";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extendsSDKClass", "EndToEndVBExtendsSDKClass")]
        public void TestEndToEndVBExtendsSDKClass()
        {
            TestFolder = "EndToEndVBExtendsSDKClass";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class ExtraLibraryTests : BlackBoxBase
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
        public void TestMappingExtraLibrary()
        {
            TestFolder = "MappingExtraLibrary";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extraLibrary", "PreTransformCSExtraLibrary")]
        public void TestPreTransformCSExtraLibrary()
        {
            TestFolder = "PreTransformCSExtraLibrary";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extraLibrary", "PostTransformCSExtraLibrary")]
        public void TestPostTransformCSExtraLibrary()
        {
            TestFolder = "PostTransformCSExtraLibrary";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extraLibrary", "PreTransformVBExtraLibrary")]
        public void TestPreTransformVBExtraLibrary()
        {
            TestFolder = "PreTransformVBExtraLibrary";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extraLibrary", "PostTransformVBExtraLibrary")]
        public void TestPostTransformVBExtraLibrary()
        {
            TestFolder = "PostTransformVBExtraLibrary";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extraLibrary", "EndToEndCSExtraLibrary")]
        public void TestEndToEndCSExtraLibrary()
        {
            TestFolder = "EndToEndCSExtraLibrary";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/extraLibrary", "EndToEndVBExtraLibrary")]
        public void TestEndToEndVBExtraLibrary()
        {
            TestFolder = "EndToEndVBExtraLibrary";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class FullyQualifiedTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/fullyQualified", "MappingFullyQualified")]
        public void TestMappingFullyQualified()
        {
            TestFolder = "MappingFullyQualified";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualified", "PreTransformCSFullyQualified")]
        public void TestPreTransformCSFullyQualified()
        {
            TestFolder = "PreTransformCSFullyQualified";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualified", "PostTransformCSFullyQualified")]
        public void TestPostTransformCSFullyQualified()
        {
            TestFolder = "PostTransformCSFullyQualified";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualified", "PreTransformVBFullyQualified")]
        public void TestPreTransformVBFullyQualified()
        {
            TestFolder = "PreTransformVBFullyQualified";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualified", "PostTransformVBFullyQualified")]
        public void TestPostTransformVBFullyQualified()
        {
            TestFolder = "PostTransformVBFullyQualified";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualified", "EndToEndCSFullyQualified")]
        public void TestEndToEndCSFullyQualified()
        {
            TestFolder = "EndToEndCSFullyQualified";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualified", "EndToEndVBFullyQualified")]
        public void TestEndToEndVBFullyQualified()
        {
            TestFolder = "EndToEndVBFullyQualified";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class FullyQualifiedModelIdentifierTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/fullyQualifiedModelIdentifier", "MappingFullyQualifiedModelIdentifier")]
        public void TestMappingFullyQualifiedModelIdentifier()
        {
            TestFolder = "MappingFullyQualifiedModelIdentifier";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualifiedModelIdentifier", "PreTransformCSFullyQualifiedModelIdentifier")]
        public void TestPreTransformCSFullyQualifiedModelIdentifier()
        {
            TestFolder = "PreTransformCSFullyQualifiedModelIdentifier";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualifiedModelIdentifier", "PostTransformCSFullyQualifiedModelIdentifier")]
        public void TestPostTransformCSFullyQualifiedModelIdentifier()
        {
            TestFolder = "PostTransformCSFullyQualifiedModelIdentifier";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualifiedModelIdentifier", "PreTransformVBFullyQualifiedModelIdentifier")]
        public void TestPreTransformVBFullyQualifiedModelIdentifier()
        {
            TestFolder = "PreTransformVBFullyQualifiedModelIdentifier";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualifiedModelIdentifier", "PostTransformVBFullyQualifiedModelIdentifier")]
        public void TestPostTransformVBFullyQualifiedModelIdentifier()
        {
            TestFolder = "PostTransformVBFullyQualifiedModelIdentifier";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualifiedModelIdentifier", "EndToEndCSFullyQualifiedModelIdentifier")]
        public void TestEndToEndCSFullyQualifiedModelIdentifier()
        {
            TestFolder = "EndToEndCSFullyQualifiedModelIdentifier";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/fullyQualifiedModelIdentifier", "EndToEndVBFullyQualifiedModelIdentifier")]
        public void TestEndToEndVBFullyQualifiedModelIdentifier()
        {
            TestFolder = "EndToEndVBFullyQualifiedModelIdentifier";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class GenericsTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/generics", "MappingGenerics")]
        public void TestMappingGenerics()
        {
            TestFolder = "MappingGenerics";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/generics", "PreTransformCSGenerics")]
        public void TestPreTransformCSGenerics()
        {
            TestFolder = "PreTransformCSGenerics";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/generics", "PostTransformCSGenerics")]
        public void TestPostTransformCSGenerics()
        {
            TestFolder = "PostTransformCSGenerics";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/generics", "PreTransformVBGenerics")]
        public void TestPreTransformVBGenerics()
        {
            TestFolder = "PreTransformVBGenerics";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/generics", "PostTransformVBGenerics")]
        public void TestPostTransformVBGenerics()
        {
            TestFolder = "PostTransformVBGenerics";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/generics", "EndToEndCSGenerics")]
        public void TestEndToEndCSGenerics()
        {
            TestFolder = "EndToEndCSGenerics";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/generics", "EndToEndVBGenerics")]
        public void TestEndToEndVBGenerics()
        {
            TestFolder = "EndToEndVBGenerics";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class InstantiatesSDKClassTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/instantiatesSDKClass", "MappingInstantiatesSDKClass")]
        public void TestMappingInstantiatesSDKClass()
        {
            TestFolder = "MappingInstantiatesSDKClass";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/instantiatesSDKClass", "PreTransformCSInstantiatesSDKClass")]
        public void TestPreTransformCSInstantiatesSDKClass()
        {
            TestFolder = "PreTransformCSInstantiatesSDKClass";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/instantiatesSDKClass", "PostTransformCSInstantiatesSDKClass")]
        public void TestPostTransformCSInstantiatesSDKClass()
        {
            TestFolder = "PostTransformCSInstantiatesSDKClass";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/instantiatesSDKClass", "PreTransformVBInstantiatesSDKClass")]
        public void TestPreTransformVBInstantiatesSDKClass()
        {
            TestFolder = "PreTransformVBInstantiatesSDKClass";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/instantiatesSDKClass", "PostTransformVBInstantiatesSDKClass")]
        public void TestPostTransformVBInstantiatesSDKClass()
        {
            TestFolder = "PostTransformVBInstantiatesSDKClass";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/instantiatesSDKClass", "EndToEndCSInstantiatesSDKClass")]
        public void TestEndToEndCSInstantiatesSDKClass()
        {
            TestFolder = "EndToEndCSInstantiatesSDKClass";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/instantiatesSDKClass", "EndToEndVBInstantiatesSDKClass")]
        public void TestEndToEndVBInstantiatesSDKClass()
        {
            TestFolder = "EndToEndVBInstantiatesSDKClass";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class MethodCallOnParameterTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/methodCallOnParameter", "MappingMethodCallOnParameter")]
        public void TestMappingMethodCallOnParameter()
        {
            TestFolder = "MappingMethodCallOnParameter";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/methodCallOnParameter", "PreTransformCSMethodCallOnParameter")]
        public void TestPreTransformCSMethodCallOnParameter()
        {
            TestFolder = "PreTransformCSMethodCallOnParameter";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/methodCallOnParameter", "PostTransformCSMethodCallOnParameter")]
        public void TestPostTransformCSMethodCallOnParameter()
        {
            TestFolder = "PostTransformCSMethodCallOnParameter";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/methodCallOnParameter", "PreTransformVBMethodCallOnParameter")]
        public void TestPreTransformVBMethodCallOnParameter()
        {
            TestFolder = "PreTransformVBMethodCallOnParameter";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/methodCallOnParameter", "PostTransformVBMethodCallOnParameter")]
        public void TestPostTransformVBMethodCallOnParameter()
        {
            TestFolder = "PostTransformVBMethodCallOnParameter";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/methodCallOnParameter", "EndToEndCSMethodCallOnParameter")]
        public void TestEndToEndCSMethodCallOnParameter()
        {
            TestFolder = "EndToEndCSMethodCallOnParameter";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/methodCallOnParameter", "EndToEndVBMethodCallOnParameter")]
        public void TestEndToEndVBMethodCallOnParameter()
        {
            TestFolder = "EndToEndVBMethodCallOnParameter";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class MultiAssemblyTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/multiAssembly", "MappingMultiAssembly")]
        public void TestMappingMultiAssembly()
        {
            TestFolder = "MappingMultiAssembly";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiAssembly", "PreTransformCSMultiAssembly")]
        public void TestPreTransformCSMultiAssembly()
        {
            TestFolder = "PreTransformCSMultiAssembly";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiAssembly", "PostTransformCSMultiAssembly")]
        public void TestPostTransformCSMultiAssembly()
        {
            TestFolder = "PostTransformCSMultiAssembly";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiAssembly", "PreTransformVBMultiAssembly")]
        public void TestPreTransformVBMultiAssembly()
        {
            TestFolder = "PreTransformVBMultiAssembly";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiAssembly", "PostTransformVBMultiAssembly")]
        public void TestPostTransformVBMultiAssembly()
        {
            TestFolder = "PostTransformVBMultiAssembly";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiAssembly", "EndToEndCSMultiAssembly")]
        public void TestEndToEndCSMultiAssembly()
        {
            TestFolder = "EndToEndCSMultiAssembly";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiAssembly", "EndToEndVBMultiAssembly")]
        public void TestEndToEndVBMultiAssembly()
        {
            TestFolder = "EndToEndVBMultiAssembly";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class MultiBasicTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/multiBasic", "MappingMultiBasic")]
        public void TestMappingMultiBasic()
        {
            TestFolder = "MappingMultiBasic";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiBasic", "PreTransformCSMultiBasic")]
        public void TestPreTransformCSMultiBasic()
        {
            TestFolder = "PreTransformCSMultiBasic";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiBasic", "PostTransformCSMultiBasic")]
        public void TestPostTransformCSMultiBasic()
        {
            TestFolder = "PostTransformCSMultiBasic";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiBasic", "PreTransformVBMultiBasic")]
        public void TestPreTransformVBMultiBasic()
        {
            TestFolder = "PreTransformVBMultiBasic";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiBasic", "PostTransformVBMultiBasic")]
        public void TestPostTransformVBMultiBasic()
        {
            TestFolder = "PostTransformVBMultiBasic";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiBasic", "EndToEndCSMultiBasic")]
        public void TestEndToEndCSMultiBasic()
        {
            TestFolder = "EndToEndCSMultiBasic";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiBasic", "EndToEndVBMultiBasic")]
        public void TestEndToEndVBMultiBasic()
        {
            TestFolder = "EndToEndVBMultiBasic";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class MultiTypeGenericTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/multiTypeGeneric", "MappingMultiTypeGeneric")]
        public void TestMappingMultiTypeGeneric()
        {
            TestFolder = "MappingMultiTypeGeneric";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiTypeGeneric", "PreTransformCSMultiTypeGeneric")]
        public void TestPreTransformCSMultiTypeGeneric()
        {
            TestFolder = "PreTransformCSMultiTypeGeneric";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiTypeGeneric", "PostTransformCSMultiTypeGeneric")]
        public void TestPostTransformCSMultiTypeGeneric()
        {
            TestFolder = "PostTransformCSMultiTypeGeneric";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiTypeGeneric", "PreTransformVBMultiTypeGeneric")]
        public void TestPreTransformVBMultiTypeGeneric()
        {
            TestFolder = "PreTransformVBMultiTypeGeneric";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiTypeGeneric", "PostTransformVBMultiTypeGeneric")]
        public void TestPostTransformVBMultiTypeGeneric()
        {
            TestFolder = "PostTransformVBMultiTypeGeneric";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiTypeGeneric", "EndToEndCSMultiTypeGeneric")]
        public void TestEndToEndCSMultiTypeGeneric()
        {
            TestFolder = "EndToEndCSMultiTypeGeneric";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/multiTypeGeneric", "EndToEndVBMultiTypeGeneric")]
        public void TestEndToEndVBMultiTypeGeneric()
        {
            TestFolder = "EndToEndVBMultiTypeGeneric";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class NamespaceInNamespaceTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/namespaceInNamespace", "MappingNamespaceInNamespace")]
        public void TestMappingNamespaceInNamespace()
        {
            TestFolder = "MappingNamespaceInNamespace";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceInNamespace", "PreTransformCSNamespaceInNamespace")]
        public void TestPreTransformCSNamespaceInNamespace()
        {
            TestFolder = "PreTransformCSNamespaceInNamespace";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceInNamespace", "PostTransformCSNamespaceInNamespace")]
        public void TestPostTransformCSNamespaceInNamespace()
        {
            TestFolder = "PostTransformCSNamespaceInNamespace";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceInNamespace", "PreTransformVBNamespaceInNamespace")]
        public void TestPreTransformVBNamespaceInNamespace()
        {
            TestFolder = "PreTransformVBNamespaceInNamespace";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceInNamespace", "PostTransformVBNamespaceInNamespace")]
        public void TestPostTransformVBNamespaceInNamespace()
        {
            TestFolder = "PostTransformVBNamespaceInNamespace";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceInNamespace", "EndToEndCSNamespaceInNamespace")]
        public void TestEndToEndCSNamespaceInNamespace()
        {
            TestFolder = "EndToEndCSNamespaceInNamespace";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceInNamespace", "EndToEndVBNamespaceInNamespace")]
        public void TestEndToEndVBNamespaceInNamespace()
        {
            TestFolder = "EndToEndVBNamespaceInNamespace";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class NamespaceMergeTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/namespaceMerge", "MappingNamespaceMerge")]
        public void TestMappingNamespaceMerge()
        {
            TestFolder = "MappingNamespaceMerge";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceMerge", "PreTransformCSNamespaceMerge")]
        public void TestPreTransformCSNamespaceMerge()
        {
            TestFolder = "PreTransformCSNamespaceMerge";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceMerge", "PostTransformCSNamespaceMerge")]
        public void TestPostTransformCSNamespaceMerge()
        {
            TestFolder = "PostTransformCSNamespaceMerge";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceMerge", "PreTransformVBNamespaceMerge")]
        public void TestPreTransformVBNamespaceMerge()
        {
            TestFolder = "PreTransformVBNamespaceMerge";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceMerge", "PostTransformVBNamespaceMerge")]
        public void TestPostTransformVBNamespaceMerge()
        {
            TestFolder = "PostTransformVBNamespaceMerge";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceMerge", "EndToEndCSNamespaceMerge")]
        public void TestEndToEndCSNamespaceMerge()
        {
            TestFolder = "EndToEndCSNamespaceMerge";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceMerge", "EndToEndVBNamespaceMerge")]
        public void TestEndToEndVBNamespaceMerge()
        {
            TestFolder = "EndToEndVBNamespaceMerge";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class NamespaceSplitTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/namespaceSplit", "MappingNamespaceSplit")]
        public void TestMappingNamespaceSplit()
        {
            TestFolder = "MappingNamespaceSplit";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplit", "PreTransformCSNamespaceSplit")]
        public void TestPreTransformCSNamespaceSplit()
        {
            TestFolder = "PreTransformCSNamespaceSplit";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplit", "PostTransformCSNamespaceSplit")]
        public void TestPostTransformCSNamespaceSplit()
        {
            TestFolder = "PostTransformCSNamespaceSplit";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplit", "PreTransformVBNamespaceSplit")]
        public void TestPreTransformVBNamespaceSplit()
        {
            TestFolder = "PreTransformVBNamespaceSplit";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplit", "PostTransformVBNamespaceSplit")]
        public void TestPostTransformVBNamespaceSplit()
        {
            TestFolder = "PostTransformVBNamespaceSplit";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplit", "EndToEndCSNamespaceSplit")]
        public void TestEndToEndCSNamespaceSplit()
        {
            TestFolder = "EndToEndCSNamespaceSplit";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplit", "EndToEndVBNamespaceSplit")]
        public void TestEndToEndVBNamespaceSplit()
        {
            TestFolder = "EndToEndVBNamespaceSplit";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class NamespaceSplitMergeTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/namespaceSplitMerge", "MappingNamespaceSplitMerge")]
        public void TestMappingNamespaceSplitMerge()
        {
            TestFolder = "MappingNamespaceSplitMerge";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplitMerge", "PreTransformCSNamespaceSplitMerge")]
        public void TestPreTransformCSNamespaceSplitMerge()
        {
            TestFolder = "PreTransformCSNamespaceSplitMerge";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplitMerge", "PostTransformCSNamespaceSplitMerge")]
        public void TestPostTransformCSNamespaceSplitMerge()
        {
            TestFolder = "PostTransformCSNamespaceSplitMerge";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplitMerge", "PreTransformVBNamespaceSplitMerge")]
        public void TestPreTransformVBNamespaceSplitMerge()
        {
            TestFolder = "PreTransformVBNamespaceSplitMerge";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplitMerge", "PostTransformVBNamespaceSplitMerge")]
        public void TestPostTransformVBNamespaceSplitMerge()
        {
            TestFolder = "PostTransformVBNamespaceSplitMerge";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplitMerge", "EndToEndCSNamespaceSplitMerge")]
        public void TestEndToEndCSNamespaceSplitMerge()
        {
            TestFolder = "EndToEndCSNamespaceSplitMerge";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/namespaceSplitMerge", "EndToEndVBNamespaceSplitMerge")]
        public void TestEndToEndVBNamespaceSplitMerge()
        {
            TestFolder = "EndToEndVBNamespaceSplitMerge";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class NewConflictsTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/newConflicts", "MappingNewConflicts")]
        public void TestMappingNewConflicts()
        {
            TestFolder = "MappingNewConflicts";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/newConflicts", "PreTransformCSNewConflicts")]
        public void TestPreTransformCSNewConflicts()
        {
            TestFolder = "PreTransformCSNewConflicts";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/newConflicts", "PostTransformCSNewConflicts")]
        public void TestPostTransformCSNewConflicts()
        {
            TestFolder = "PostTransformCSNewConflicts";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/newConflicts", "PreTransformVBNewConflicts")]
        public void TestPreTransformVBNewConflicts()
        {
            TestFolder = "PreTransformVBNewConflicts";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/newConflicts", "PostTransformVBNewConflicts")]
        public void TestPostTransformVBNewConflicts()
        {
            TestFolder = "PostTransformVBNewConflicts";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/newConflicts", "EndToEndCSNewConflicts")]
        public void TestEndToEndCSNewConflicts()
        {
            TestFolder = "EndToEndCSNewConflicts";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/newConflicts", "EndToEndVBNewConflicts")]
        public void TestEndToEndVBNewConflicts()
        {
            TestFolder = "EndToEndVBNewConflicts";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class NonRootUsingTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/nonRootUsing", "MappingNonRootUsing")]
        public void TestMappingNonRootUsing()
        {
            TestFolder = "MappingNonRootUsing";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nonRootUsing", "PreTransformCSNonRootUsing")]
        public void TestPreTransformCSNonRootUsing()
        {
            TestFolder = "PreTransformCSNonRootUsing";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nonRootUsing", "PostTransformCSNonRootUsing")]
        public void TestPostTransformCSNonRootUsing()
        {
            TestFolder = "PostTransformCSNonRootUsing";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nonRootUsing", "PreTransformVBNonRootUsing")]
        public void TestPreTransformVBNonRootUsing()
        {
            TestFolder = "PreTransformVBNonRootUsing";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nonRootUsing", "PostTransformVBNonRootUsing")]
        public void TestPostTransformVBNonRootUsing()
        {
            TestFolder = "PostTransformVBNonRootUsing";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nonRootUsing", "EndToEndCSNonRootUsing")]
        public void TestEndToEndCSNonRootUsing()
        {
            TestFolder = "EndToEndCSNonRootUsing";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nonRootUsing", "EndToEndVBNonRootUsing")]
        public void TestEndToEndVBNonRootUsing()
        {
            TestFolder = "EndToEndVBNonRootUsing";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class NothingTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/nothing", "MappingNothing")]
        public void TestMappingNothing()
        {
            TestFolder = "MappingNothing";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nothing", "PreTransformCSNothing")]
        public void TestPreTransformCSNothing()
        {
            TestFolder = "PreTransformCSNothing";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nothing", "PostTransformCSNothing")]
        public void TestPostTransformCSNothing()
        {
            TestFolder = "PostTransformCSNothing";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nothing", "PreTransformVBNothing")]
        public void TestPreTransformVBNothing()
        {
            TestFolder = "PreTransformVBNothing";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nothing", "PostTransformVBNothing")]
        public void TestPostTransformVBNothing()
        {
            TestFolder = "PostTransformVBNothing";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nothing", "EndToEndCSNothing")]
        public void TestEndToEndCSNothing()
        {
            TestFolder = "EndToEndCSNothing";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/nothing", "EndToEndVBNothing")]
        public void TestEndToEndVBNothing()
        {
            TestFolder = "EndToEndVBNothing";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class OldConflictsTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/oldConflicts", "MappingOldConflicts")]
        public void TestMappingOldConflicts()
        {
            TestFolder = "MappingOldConflicts";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/oldConflicts", "PreTransformCSOldConflicts")]
        public void TestPreTransformCSOldConflicts()
        {
            TestFolder = "PreTransformCSOldConflicts";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/oldConflicts", "PostTransformCSOldConflicts")]
        public void TestPostTransformCSOldConflicts()
        {
            TestFolder = "PostTransformCSOldConflicts";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/oldConflicts", "PreTransformVBOldConflicts")]
        public void TestPreTransformVBOldConflicts()
        {
            TestFolder = "PreTransformVBOldConflicts";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/oldConflicts", "PostTransformVBOldConflicts")]
        public void TestPostTransformVBOldConflicts()
        {
            TestFolder = "PostTransformVBOldConflicts";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/oldConflicts", "EndToEndCSOldConflicts")]
        public void TestEndToEndCSOldConflicts()
        {
            TestFolder = "EndToEndCSOldConflicts";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/oldConflicts", "EndToEndVBOldConflicts")]
        public void TestEndToEndVBOldConflicts()
        {
            TestFolder = "EndToEndVBOldConflicts";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class ReturnClassTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/returnClass", "MappingReturnClass")]
        public void TestMappingReturnClass()
        {
            TestFolder = "MappingReturnClass";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/returnClass", "PreTransformCSReturnClass")]
        public void TestPreTransformCSReturnClass()
        {
            TestFolder = "PreTransformCSReturnClass";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/returnClass", "PostTransformCSReturnClass")]
        public void TestPostTransformCSReturnClass()
        {
            TestFolder = "PostTransformCSReturnClass";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/returnClass", "PreTransformVBReturnClass")]
        public void TestPreTransformVBReturnClass()
        {
            TestFolder = "PreTransformVBReturnClass";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/returnClass", "PostTransformVBReturnClass")]
        public void TestPostTransformVBReturnClass()
        {
            TestFolder = "PostTransformVBReturnClass";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/returnClass", "EndToEndCSReturnClass")]
        public void TestEndToEndCSReturnClass()
        {
            TestFolder = "EndToEndCSReturnClass";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/returnClass", "EndToEndVBReturnClass")]
        public void TestEndToEndVBReturnClass()
        {
            TestFolder = "EndToEndVBReturnClass";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class TypeofTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/typeof", "MappingTypeof")]
        public void TestMappingTypeof()
        {
            TestFolder = "MappingTypeof";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/typeof", "PreTransformCSTypeof")]
        public void TestPreTransformCSTypeof()
        {
            TestFolder = "PreTransformCSTypeof";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/typeof", "PostTransformCSTypeof")]
        public void TestPostTransformCSTypeof()
        {
            TestFolder = "PostTransformCSTypeof";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/typeof", "PreTransformVBTypeof")]
        public void TestPreTransformVBTypeof()
        {
            TestFolder = "PreTransformVBTypeof";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/typeof", "PostTransformVBTypeof")]
        public void TestPostTransformVBTypeof()
        {
            TestFolder = "PostTransformVBTypeof";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/typeof", "EndToEndCSTypeof")]
        public void TestEndToEndCSTypeof()
        {
            TestFolder = "EndToEndCSTypeof";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/typeof", "EndToEndVBTypeof")]
        public void TestEndToEndVBTypeof()
        {
            TestFolder = "EndToEndVBTypeof";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class UnusedTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/unused", "MappingUnused")]
        public void TestMappingUnused()
        {
            TestFolder = "MappingUnused";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/unused", "PreTransformCSUnused")]
        public void TestPreTransformCSUnused()
        {
            TestFolder = "PreTransformCSUnused";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/unused", "PostTransformCSUnused")]
        public void TestPostTransformCSUnused()
        {
            TestFolder = "PostTransformCSUnused";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/unused", "PreTransformVBUnused")]
        public void TestPreTransformVBUnused()
        {
            TestFolder = "PreTransformVBUnused";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/unused", "PostTransformVBUnused")]
        public void TestPostTransformVBUnused()
        {
            TestFolder = "PostTransformVBUnused";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/unused", "EndToEndCSUnused")]
        public void TestEndToEndCSUnused()
        {
            TestFolder = "EndToEndCSUnused";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/unused", "EndToEndVBUnused")]
        public void TestEndToEndVBUnused()
        {
            TestFolder = "EndToEndVBUnused";
            RunEndToEndVBTest();
        }
    }

    [TestClass]
    public class VariablesTests : BlackBoxBase
    {
        [TestMethod]
        [DeploymentItem("tests/variables", "MappingVariables")]
        public void TestMappingVariables()
        {
            TestFolder = "MappingVariables";
            RunMappingTest();
        }
        [TestMethod]
        [DeploymentItem("tests/variables", "PreTransformCSVariables")]
        public void TestPreTransformCSVariables()
        {
            TestFolder = "PreTransformCSVariables";
            RunPreTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/variables", "PostTransformCSVariables")]
        public void TestPostTransformCSVariables()
        {
            TestFolder = "PostTransformCSVariables";
            RunPostTransformCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/variables", "PreTransformVBVariables")]
        public void TestPreTransformVBVariables()
        {
            TestFolder = "PreTransformVBVariables";
            RunPreTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/variables", "PostTransformVBVariables")]
        public void TestPostTransformVBVariables()
        {
            TestFolder = "PostTransformVBVariables";
            RunPostTransformVBTest();
        }
        [TestMethod]
        [DeploymentItem("tests/variables", "EndToEndCSVariables")]
        public void TestEndToEndCSVariables()
        {
            TestFolder = "EndToEndCSVariables";
            RunEndToEndCSTest();
        }
        [TestMethod]
        [DeploymentItem("tests/variables", "EndToEndVBVariables")]
        public void TestEndToEndVBVariables()
        {
            TestFolder = "EndToEndVBVariables";
            RunEndToEndVBTest();
        }
    }

}
