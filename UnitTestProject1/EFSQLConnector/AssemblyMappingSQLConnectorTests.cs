using Microsoft.VisualStudio.TestTools.UnitTesting;
using EFSQLConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.WhiteBox.EFSQLConnector
{
    [TestClass()]
    public class AssemblyMappingSQLConnectorTests
    {
        private AssemblyMappingSQLConnector instance;
        private string name;
        private int id;
        [TestInitialize]
        public void Setup()
        {
            name = Guid.NewGuid().ToString();
            SDKSQLConnector.GetInstance().SaveSDK(name, "");
            id = SDKSQLConnector.GetInstance().getByName(name).id;
            instance = AssemblyMappingSQLConnector.GetInstance();
        }
        [TestCleanup]
        public void Teardown()
        {
            SDKSQLConnector.GetInstance().DeleteSDKByName(name);
        }

        [TestMethod()]
        public void TestAssemblyMappingSQLConnectorGetOrCreateOldAssemblyMapTest()
        {
            var mapA = instance.GetOrCreateOldAssemblyMap(id, "path");
            var expectA = new assembly_map
            {
                old_path = "path",
                sdk_id = id
            };
            AssertAditional.AssemblyMapEquals(expectA, mapA, "issue on first create");

            var mapB = instance.GetOrCreateOldAssemblyMap(id, "path2");
            var expectB = new assembly_map
            {
                old_path = "path2",
                sdk_id = id
            };
            AssertAditional.AssemblyMapEquals(expectB, mapB, "issue on second create");
            AssertAditional.AssemblyMapEquals(expectA, mapA, "impropper modification of A");

            var mapC = instance.GetOrCreateOldAssemblyMap(id, "path");
            var expectC = new assembly_map
            {
                old_path = "path",
                sdk_id = id,
                id = mapA.id
            };
            AssertAditional.AssemblyMapEquals(expectC, mapC, "issue on first get");
            AssertAditional.AssemblyMapEquals(expectB, mapB, "impropper modification of B");
            AssertAditional.AssemblyMapEquals(expectA, mapA, "impropper modification of A");
        }

        [TestMethod()]
        public void TestAssemblyMappingSQLConnectorUpdateAssemblyMappingTest()
        {
            var mapA = instance.GetOrCreateOldAssemblyMap(id, "path");
            var mapB = instance.GetOrCreateOldAssemblyMap(id, "path");
            var mapC = instance.GetOrCreateOldAssemblyMap(id, "path");

            instance.UpdateAssemblyMapping(mapA, "pathNew", "name");
            var expectNewA = new assembly_map
            {
                old_path = "path",
                sdk_id = id,
                new_path = "pathNew",
                name = "name"
            };
            AssertAditional.AssemblyMapEquals(expectNewA, mapA, "issue on update");

            instance.UpdateAssemblyMapping(mapB, "pathNew2", "name");
            var expectNewB = new assembly_map
            {
                old_path = "path",
                sdk_id = id,
                new_path = "pathNew2",
                name = "name"
            };
            AssertAditional.AssemblyMapEquals(expectNewB, mapB, "issue on double path");
            AssertAditional.AssemblyMapEquals(expectNewA, mapA, "impropper modification of A");


            instance.UpdateAssemblyMapping(mapC, "pathNew", "name2");
            var expectNewC = new assembly_map
            {
                old_path = "path",
                sdk_id = id,
                new_path = "pathNew",
                name = "name2"
            };
            AssertAditional.AssemblyMapEquals(expectNewB, mapC, "issue on double name");
            AssertAditional.AssemblyMapEquals(expectNewA, mapB, "impropper modification of B");
            AssertAditional.AssemblyMapEquals(expectNewA, mapA, "impropper modification of A");
        }

        [TestMethod()]
        public void TestAssemblyMappingSQLConnectorGetAllNewDllPathsTest()
        {
            AssertAditional.SetEquals(new HashSet<string>{ }, instance.GetAllNewDllPaths(id), "initial value");

            var mapA = instance.GetOrCreateOldAssemblyMap(id, "path");
            AssertAditional.SetEquals(new HashSet<string>{ }, instance.GetAllNewDllPaths(id), "null entry");

            var mapB = instance.GetOrCreateOldAssemblyMap(id, "path2");
            instance.UpdateAssemblyMapping(mapB, "pathNew", "name");
            AssertAditional.SetEquals(new HashSet<string>{ "pathNew" }, instance.GetAllNewDllPaths(id), "first entry");

            var mapC = instance.GetOrCreateOldAssemblyMap(id, "path3");
            var mapD = instance.GetOrCreateOldAssemblyMap(id, "path3");
            instance.UpdateAssemblyMapping(mapC, "pathNew2", "name2");
            instance.UpdateAssemblyMapping(mapD, "pathNew3", "name3");
            AssertAditional.SetEquals(new HashSet<string> { "pathNew", "pathNew2", "pathNew3" },
                instance.GetAllNewDllPaths(id), "repeat old name");

            var mapE = instance.GetOrCreateOldAssemblyMap(id, "path4");
            instance.UpdateAssemblyMapping(mapE, "pathNew", "name");
            AssertAditional.SetEquals(new HashSet<string> { "pathNew", "pathNew2", "pathNew3" },
                instance.GetAllNewDllPaths(id), "dupplicate new entry");
        }

        [TestMethod()]
        public void TestAssemblyMappingSQLConnectorGetAllOldDllPathsTest()
        {
            AssertAditional.SetEquals(new HashSet<string> { }, instance.GetAllOldDllPaths(id), "initial value");

            var mapA = instance.GetOrCreateOldAssemblyMap(id, "path");
            var mapB = instance.GetOrCreateOldAssemblyMap(id, "path");
            AssertAditional.SetEquals(new HashSet<string> { "path" }, instance.GetAllOldDllPaths(id), "added entry");

            instance.GetOrCreateOldAssemblyMap(id, "path2");
            AssertAditional.SetEquals(new HashSet<string> { "path", "path2" },
                instance.GetAllOldDllPaths(id), "added second entry");

            instance.UpdateAssemblyMapping(mapA, "pathNew", "name");
            instance.UpdateAssemblyMapping(mapB, "pathNew2", "name2");
            AssertAditional.SetEquals(new HashSet<string> { "path", "path2" },
                instance.GetAllOldDllPaths(id), "double count of first entry");
        }

        [TestMethod()]
        public void TestAssemblyMappingSQLConnectorGetAllNewDllPathsWithFullNameTest()
        {
            AssertAditional.DictionaryEquals(new Dictionary<string, string> { },
                instance.GetAllNewDllPathsWithFullName(id), "initial value");

            var mapA = instance.GetOrCreateOldAssemblyMap(id, "path");
            AssertAditional.DictionaryEquals(new Dictionary<string, string> { },
                instance.GetAllNewDllPathsWithFullName(id), "null entry");

            var mapB = instance.GetOrCreateOldAssemblyMap(id, "path2");
            instance.UpdateAssemblyMapping(mapB, "pathNew", "name");
            AssertAditional.DictionaryEquals(new Dictionary<string, string> { { "pathNew", "name" } },
                instance.GetAllNewDllPathsWithFullName(id), "first entry");

            var mapC = instance.GetOrCreateOldAssemblyMap(id, "path3");
            var mapD = instance.GetOrCreateOldAssemblyMap(id, "path3");
            instance.UpdateAssemblyMapping(mapC, "pathNew2", "name2");
            instance.UpdateAssemblyMapping(mapD, "pathNew3", "name3");
            AssertAditional.DictionaryEquals(new Dictionary<string, string> { { "pathNew", "name" }, { "pathNew2", "name2" },
                { "pathNew3","name3" } }, instance.GetAllNewDllPathsWithFullName(id), "repeat old name");

            var mapE = instance.GetOrCreateOldAssemblyMap(id, "path4");
            instance.UpdateAssemblyMapping(mapE, "pathNew", "name");
            AssertAditional.DictionaryEquals(new Dictionary<string, string> { { "pathNew", "name" }, { "pathNew2", "name2" },
                { "pathNew3","name3" } }, instance.GetAllNewDllPathsWithFullName(id), "dupplicate new entry");
        }
    }
}