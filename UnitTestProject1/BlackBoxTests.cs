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
    [DeploymentItem("tests/alias", "alias")]
    public class AliasTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitAlias()
        {
            TestFolder = "alias";
        }
        [TestMethod]
        public void TestMappingAlias()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSAlias()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSAlias()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBAlias()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBAlias()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSAlias()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBAlias()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/assemblyChange", "assemblyChange")]
    public class AssemblyChangeTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitAssemblyChange()
        {
            TestFolder = "assemblyChange";
        }
        [TestMethod]
        public void TestMappingAssemblyChange()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSAssemblyChange()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSAssemblyChange()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBAssemblyChange()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBAssemblyChange()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSAssemblyChange()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBAssemblyChange()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/assemblyMerge", "assemblyMerge")]
    public class AssemblyMergeTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitAssemblyMerge()
        {
            TestFolder = "assemblyMerge";
        }
        [TestMethod]
        public void TestMappingAssemblyMerge()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSAssemblyMerge()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSAssemblyMerge()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBAssemblyMerge()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBAssemblyMerge()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSAssemblyMerge()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBAssemblyMerge()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/assemblySplit", "assemblySplit")]
    public class AssemblySplitTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitAssemblySplit()
        {
            TestFolder = "assemblySplit";
        }
        [TestMethod]
        public void TestMappingAssemblySplit()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSAssemblySplit()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSAssemblySplit()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBAssemblySplit()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBAssemblySplit()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSAssemblySplit()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBAssemblySplit()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/basic", "basic")]
    public class BasicTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitBasic()
        {
            TestFolder = "basic";
        }
        [TestMethod]
        public void TestMappingBasic()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSBasic()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSBasic()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBBasic()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBBasic()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSBasic()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBBasic()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/basicNamespace", "basicNamespace")]
    public class BasicNamespaceTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitBasicNamespace()
        {
            TestFolder = "basicNamespace";
        }
        [TestMethod]
        public void TestMappingBasicNamespace()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSBasicNamespace()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSBasicNamespace()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBBasicNamespace()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBBasicNamespace()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSBasicNamespace()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBBasicNamespace()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/casting", "casting")]
    public class CastingTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitCasting()
        {
            TestFolder = "casting";
        }
        [TestMethod]
        public void TestMappingCasting()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSCasting()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSCasting()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBCasting()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBCasting()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSCasting()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBCasting()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/classInClass", "classInClass")]
    public class ClassInClassTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitClassInClass()
        {
            TestFolder = "classInClass";
        }
        [TestMethod]
        public void TestMappingClassInClass()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSClassInClass()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSClassInClass()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBClassInClass()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBClassInClass()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSClassInClass()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBClassInClass()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/extendsSDKClass", "extendsSDKClass")]
    public class ExtendsSDKClassTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitExtendsSDKClass()
        {
            TestFolder = "extendsSDKClass";
        }
        [TestMethod]
        public void TestMappingExtendsSDKClass()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSExtendsSDKClass()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSExtendsSDKClass()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBExtendsSDKClass()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBExtendsSDKClass()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSExtendsSDKClass()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBExtendsSDKClass()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/extraLibrary", "extraLibrary")]
    public class ExtraLibraryTests : BlackBoxBase
    {
        public override void CompileLibraries()
        {
            base.CompileLibraries();
            CompileSolution(Path.Combine(TestFolder, "extraLibrary", "extraLibrary.sln"));
        }
        [TestInitialize]
        public void InitExtraLibrary()
        {
            TestFolder = "extraLibrary";
        }
        [TestMethod]
        public void TestMappingExtraLibrary()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSExtraLibrary()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSExtraLibrary()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBExtraLibrary()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBExtraLibrary()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSExtraLibrary()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBExtraLibrary()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/fullyQualified", "fullyQualified")]
    public class FullyQualifiedTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitFullyQualified()
        {
            TestFolder = "fullyQualified";
        }
        [TestMethod]
        public void TestMappingFullyQualified()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSFullyQualified()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSFullyQualified()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBFullyQualified()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBFullyQualified()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSFullyQualified()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBFullyQualified()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/fullyQualifiedModelIdentifier", "fullyQualifiedModelIdentifier")]
    public class FullyQualifiedModelIdentifierTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitFullyQualifiedModelIdentifier()
        {
            TestFolder = "fullyQualifiedModelIdentifier";
        }
        [TestMethod]
        public void TestMappingFullyQualifiedModelIdentifier()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSFullyQualifiedModelIdentifier()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSFullyQualifiedModelIdentifier()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBFullyQualifiedModelIdentifier()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBFullyQualifiedModelIdentifier()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSFullyQualifiedModelIdentifier()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBFullyQualifiedModelIdentifier()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/generics", "generics")]
    public class GenericsTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitGenerics()
        {
            TestFolder = "generics";
        }
        [TestMethod]
        public void TestMappingGenerics()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSGenerics()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSGenerics()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBGenerics()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBGenerics()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSGenerics()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBGenerics()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/instantiatesSDKClass", "instantiatesSDKClass")]
    public class InstantiatesSDKClassTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitInstantiatesSDKClass()
        {
            TestFolder = "instantiatesSDKClass";
        }
        [TestMethod]
        public void TestMappingInstantiatesSDKClass()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSInstantiatesSDKClass()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSInstantiatesSDKClass()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBInstantiatesSDKClass()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBInstantiatesSDKClass()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSInstantiatesSDKClass()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBInstantiatesSDKClass()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/multiAssembly", "multiAssembly")]
    public class MultiAssemblyTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitMultiAssembly()
        {
            TestFolder = "multiAssembly";
        }
        [TestMethod]
        public void TestMappingMultiAssembly()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSMultiAssembly()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSMultiAssembly()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBMultiAssembly()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBMultiAssembly()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSMultiAssembly()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBMultiAssembly()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/multiBasic", "multiBasic")]
    public class MultiBasicTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitMultiBasic()
        {
            TestFolder = "multiBasic";
        }
        [TestMethod]
        public void TestMappingMultiBasic()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSMultiBasic()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSMultiBasic()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBMultiBasic()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBMultiBasic()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSMultiBasic()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBMultiBasic()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/namespaceInNamespace", "namespaceInNamespace")]
    public class NamespaceInNamespaceTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitNamespaceInNamespace()
        {
            TestFolder = "namespaceInNamespace";
        }
        [TestMethod]
        public void TestMappingNamespaceInNamespace()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSNamespaceInNamespace()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSNamespaceInNamespace()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBNamespaceInNamespace()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBNamespaceInNamespace()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSNamespaceInNamespace()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBNamespaceInNamespace()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/newConflicts", "newConflicts")]
    public class NewConflictsTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitNewConflicts()
        {
            TestFolder = "newConflicts";
        }
        [TestMethod]
        public void TestMappingNewConflicts()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSNewConflicts()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSNewConflicts()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBNewConflicts()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBNewConflicts()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSNewConflicts()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBNewConflicts()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/nonRootUsing", "nonRootUsing")]
    public class NonRootUsingTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitNonRootUsing()
        {
            TestFolder = "nonRootUsing";
        }
        [TestMethod]
        public void TestMappingNonRootUsing()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSNonRootUsing()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSNonRootUsing()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBNonRootUsing()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBNonRootUsing()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSNonRootUsing()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBNonRootUsing()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/nothing", "nothing")]
    public class NothingTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitNothing()
        {
            TestFolder = "nothing";
        }
        [TestMethod]
        public void TestMappingNothing()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSNothing()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSNothing()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBNothing()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBNothing()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSNothing()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBNothing()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/oldConflicts", "oldConflicts")]
    public class OldConflictsTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitOldConflicts()
        {
            TestFolder = "oldConflicts";
        }
        [TestMethod]
        public void TestMappingOldConflicts()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSOldConflicts()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSOldConflicts()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBOldConflicts()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBOldConflicts()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSOldConflicts()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBOldConflicts()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/typeof", "typeof")]
    public class TypeofTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitTypeof()
        {
            TestFolder = "typeof";
        }
        [TestMethod]
        public void TestMappingTypeof()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSTypeof()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSTypeof()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBTypeof()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBTypeof()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSTypeof()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBTypeof()
        {
            RunPostTransformVBTest();
        }
    }

    [TestClass]
    [DeploymentItem("tests/unused", "unused")]
    public class UnusedTests : BlackBoxBase
    {
        [TestInitialize]
        public void InitUnused()
        {
            TestFolder = "unused";
        }
        [TestMethod]
        public void TestMappingUnused()
        {
            RunMappingTest();
        }
        [TestMethod]
        public void TestPreTransformCSUnused()
        {
            RunPreTransformCSTest();
        }
        [TestMethod]
        public void TestPostTransformCSUnused()
        {
            RunPostTransformCSTest();
        }
        [TestMethod]
        public void TestPreTransformVBUnused()
        {
            RunPreTransformVBTest();
        }
        [TestMethod]
        public void TestPostTransformVBUnused()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndCSUnused()
        {
            RunPostTransformVBTest();
        }
        [TestMethod]
        public void TestEndToEndVBUnused()
        {
            RunPostTransformVBTest();
        }
    }

}
