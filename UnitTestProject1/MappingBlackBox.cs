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
    public class MappingBlackBox
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

            set
            {
                testFolder = new FileInfo(value).FullName;
            }
        }


        static string testFolder;
        static string pathToCreateMappings = null;
        static string sdkNameId = null;

        static MappingBlackBox()
        {
            var dir = new DirectoryInfo(".");
            while (dir != null && dir.GetFiles("*.sln").Length == 0)
            {
                dir = dir.Parent;
            }
            if (dir == null)
            {
                Assert.Fail("could not find parent solution");
                return;
            }
            var soln = MSBuildWorkspace.Create().OpenSolutionAsync(dir.GetFiles("*.sln")[0].FullName).Result;
            foreach (var proj in soln.Projects)
            {
                if (proj.Name.Equals("CreateMappings"))
                {
                    pathToCreateMappings = proj.OutputFilePath;
                }
            }
            if (pathToCreateMappings == null)
            {
                Assert.Fail("Couldn't find create mapping program");
                return;
            }
        }

        List<Mapping> expectedMappings;

        public virtual void LoadExpectedMappings()
        {
            int model_identifier_id = -1;
            int old_namespace_id = -1;
            int old_classname_id = -1;
            int new_namespace_id = -1;
            int new_classname_id = -1;
            int old_assembly_path_id = -1;
            int new_assembly_path_id = -1;
            if ( ! File.Exists(Path.Combine(TestFolder, "expectedSql.txt")) )
            {
                Assert.Fail("expectedSql.txt does not exist");
            }
            var lines = File.ReadAllLines(Path.Combine(TestFolder, "expectedSql.txt"));
            if ( lines.Length <= 0 )
            {
                Assert.Fail("expectedSql.txt is empty");
            }
            var lineOne = lines[0].Split('\t');
            Assert.AreEqual(7, lineOne.Length, "Impropper number of headers");
            for ( var i = 0; i < lineOne.Length; i++)
            {
                switch(lineOne[i])
                {
                    case "model_identifier":
                        model_identifier_id = i;
                        break;
                    case "old_namespace":
                        old_namespace_id = i;
                        break;
                    case "old_classname":
                        old_classname_id = i;
                        break;
                    case "new_namespace":
                        new_namespace_id = i;
                        break;
                    case "new_classname":
                        new_classname_id = i;
                        break;
                    case "old_assembly_path":
                        old_assembly_path_id = i;
                        break;
                    case "new_assembly_path":
                        new_assembly_path_id = i;
                        break;
                    default:
                        Assert.Fail("invalid header " + lineOne[i]);
                        break;
                }
            }
            expectedMappings = new List<Mapping>();
            for ( var i = 1; i < lines.Length; i++ )
            {
                var curLine = lines[i].Split('\t');
                Assert.AreEqual(lineOne.Length, curLine.Length, "wrong number of entries on line " + i);
                expectedMappings.Add(new Mapping(curLine[old_namespace_id], curLine[new_namespace_id],
                    curLine[model_identifier_id], curLine[old_classname_id], curLine[new_classname_id],
                    curLine[old_assembly_path_id], curLine[new_namespace_id]));
            }
        }

        public virtual void ResetDatabase()
        {
            SDKSQLConnector.GetInstance().DeleteSDKByName(sdkNameId);
        }

        public virtual void RunMapping()
        {
            var createMapping = new Process();
            createMapping.StartInfo.FileName = pathToCreateMappings;
            createMapping.StartInfo.Arguments = "\"" + Path.Combine(TestFolder, "bin1") + "\" \"" + 
                Path.Combine(TestFolder, "bin2") + "\" \""+ sdkNameId + "\"";
            createMapping.Start();
            createMapping.WaitForExit();
            Assert.AreEqual(0, createMapping.ExitCode, "Error running the creating mappings");
        }

        public virtual void VerifyMapping()
        {
            var sdkId = SDKSQLConnector.GetInstance().getByName(sdkNameId).id;
            var actualMappings = SDKMappingSQLConnector.GetInstance().GetAllByWhereClause(m => m.id == sdkId);
            Assert.AreEqual(expectedMappings.Count, actualMappings.Count, "Wrong number of generated mappings");
            foreach (var expect in actualMappings)
            {
                var found = false;
                foreach (var actual in expectedMappings)
                {
                    if (expect.ModelIdentifierGUID.Equals(actual.ModelIdentifierGUID))
                    {
                        found = true;
                        Assert.AreEqual(expect.NewClassName, actual.NewClassName, "NewClassName mismatch");
                        Assert.AreEqual(expect.NewNamespace, actual.NewNamespace, "NewNamespace mismatch");
                        Assert.AreEqual(expect.OldClassName, actual.OldClassName, "OldClassName mismatch");
                        Assert.AreEqual(expect.OldNamespace, actual.OldNamespace, "OldNamespace mismatch");
                        Assert.AreEqual(
                            Path.GetFullPath(Path.Combine(TestFolder, expect.NewDllPath)),
                            Path.GetFullPath(actual.NewDllPath),
                            "NewDllPath mismatch");
                        Assert.AreEqual(
                            Path.GetFullPath(Path.Combine(TestFolder, expect.OldDllPath)),
                            Path.GetFullPath(actual.OldDllPath),
                            "NewDllPath mismatch");
                        break;
                    }
                }
                if (!found) {
                    Assert.Fail("Could not find actual mapping for " + expect.ModelIdentifierGUID);
                }
            }
        }

        // call this method in the test method
        public virtual void RunTest()
        {
            sdkNameId = Path.GetFileName(TestFolder);
            LoadExpectedMappings();
            ResetDatabase();
            RunMapping();
            VerifyMapping();
        }


        [TestMethod]
        [DeploymentItem("tests/alias", "alias")]
        public void TestMappingAlias()
        {
            testFolder = "alias";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/assemblyChange", "assemblyChange")]
        public void TestMappingAssemblyChange()
        {
            testFolder = "assemblyChange";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/assemblyMerge", "assemblyMerge")]
        public void TestMappingAssemblyMerge()
        {
            testFolder = "assemblyMerge";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/assemblySplit", "assemblySplit")]
        public void TestMappingAssemblySplit()
        {
            testFolder = "assemblySplit";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/basic", "basic")]
        public void TestMappingBasic()
        {
            testFolder = "basic";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/basicNamespace", "basicNamespace")]
        public void TestMappingBasicNamespace()
        {
            testFolder = "basicNamespace";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/casting", "casting")]
        public void TestMappingCasting()
        {
            testFolder = "casting";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/classInClass", "classInClass")]
        public void TestMappingClassInClass()
        {
            testFolder = "classInClass";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/extendsSDKClass", "extendsSDKClass")]
        public void TestMappingExtendsSDKClass()
        {
            testFolder = "extendsSDKClass";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/extraLibrary", "extraLibrary")]
        public void TestMappingExtraLibrary()
        {
            testFolder = "extraLibrary";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/fullyQualified", "fullyQualified")]
        public void TestMappingFullyQualified()
        {
            testFolder = "fullyQualified";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/fullyQualifiedModelIdentifier", "fullyQualifiedModelIdentifier")]
        public void TestMappingFullyQualifiedModelIdentifier()
        {
            testFolder = "fullyQualifiedModelIdentifier";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/generics", "generics")]
        public void TestMappingGenerics()
        {
            testFolder = "generics";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/instantiatesSDKClass", "instantiatesSDKClass")]
        public void TestMappingInstantiatesSDKClass()
        {
            testFolder = "instantiatesSDKClass";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/multiAssembly", "multiAssembly")]
        public void TestMappingMultiAssembly()
        {
            testFolder = "multiAssembly";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/multiBasic", "multiBasic")]
        public void TestMappingMultiBasic()
        {
            testFolder = "multiBasic";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/namespaceInNamespace", "namespaceInNamespace")]
        public void TestMappingNamespaceInNamespace()
        {
            testFolder = "namespaceInNamespace";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/newConflicts", "newConflicts")]
        public void TestMappingNewConflicts()
        {
            testFolder = "newConflicts";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/nonRootUsing", "nonRootUsing")]
        public void TestMappingNonRootUsing()
        {
            testFolder = "nonRootUsing";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/nothing", "nothing")]
        public void TestMappingNothing()
        {
            testFolder = "nothing";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/oldConflicts", "oldConflicts")]
        public void TestMappingOldConflicts()
        {
            testFolder = "oldConflicts";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/typeof", "typeof")]
        public void TestMappingTypeof()
        {
            testFolder = "typeof";
            RunTest();
        }

        [TestMethod]
        [DeploymentItem("tests/unused", "unused")]
        public void TestMappingUnused()
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
//  echo "        public void TestMapping${d^}()"
//  echo "        {"
//  echo "            testFolder = \"$d\";"
//  echo "            RunTest();"
//  echo "        }"
//done
