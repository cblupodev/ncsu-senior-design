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
            id = SDKSQLConnector.GetInstance().GetByName(name).id;
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

        // covered by other code, and there's no way to get actually assembly_map entries
        /*
        [TestMethod()]
        public void TestAssemblyMappingSQLConnectorUpdateAssemblyMappingTest()
        {
            var nsMap = NSMappingSQLConnector.GetInstance().GetOrCreateOldNSMap(id, "A");

            var mapA = instance.GetOrCreateOldAssemblyMap(id, "path");
            var mapB = instance.GetOrCreateOldAssemblyMap(id, "path");
            var mapC = instance.GetOrCreateOldAssemblyMap(id, "path");


            SDKMappingSQLConnector.GetInstance().SaveOldSDKMapping(id, "A", "A", nsMap, mapA);
            var targetA = SDKMappingSQLConnector.GetInstance().GetSDKMappingByIdentifiers(id, "A");
            SDKMappingSQLConnector.GetInstance().SaveOldSDKMapping(id, "B", "B", nsMap, mapB);
            var targetB = SDKMappingSQLConnector.GetInstance().GetSDKMappingByIdentifiers(id, "B");
            SDKMappingSQLConnector.GetInstance().SaveOldSDKMapping(id, "C", "C", nsMap, mapC);
            var targetC = SDKMappingSQLConnector.GetInstance().GetSDKMappingByIdentifiers(id, "C");

            instance.UpdateAssemblyMapping(mapA, targetA, "pathNew", "name");
            SDKMappingSQLConnector.GetInstance().UpdateSDKMapping(targetA, "A");
            var expectNewA = new assembly_map
            {
                old_path = "path",
                sdk_id = id,
                new_path = "pathNew",
                name = "name"
            };
            AssertAditional.AssemblyMapEquals(expectNewA, mapA, "issue on update");

            instance.UpdateAssemblyMapping(mapB, targetB, "pathNew2", "name");
            SDKMappingSQLConnector.GetInstance().UpdateSDKMapping(targetB, "B");
            var expectNewB = new assembly_map
            {
                old_path = "path",
                sdk_id = id,
                new_path = "pathNew2",
                name = "name"
            };
            AssertAditional.AssemblyMapEquals(expectNewB, mapB, "issue on double path");
            AssertAditional.AssemblyMapEquals(expectNewA, mapA, "impropper modification of A");


            instance.UpdateAssemblyMapping(mapC, targetC, "pathNew", "name2");
            SDKMappingSQLConnector.GetInstance().UpdateSDKMapping(targetC, "C");
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
        }*/

        [TestMethod()]
        public void TestAssemblyMappingSQLConnectorGetAllNewDllPathsTest()
        {
            var nsMap = NSMappingSQLConnector.GetInstance().GetOrCreateOldNSMap(id, "A");

            AssertAditional.SetEquals(new HashSet<string>{ }, instance.GetAllNewDllPaths(id), "initial value");

            var mapA = instance.GetOrCreateOldAssemblyMap(id, "path");
            SDKMappingSQLConnector.GetInstance().SaveOldSDKMapping(id, "A", "A", nsMap, mapA);
            var targetA = SDKMappingSQLConnector.GetInstance().GetSDKMappingByIdentifiers(id, "A");
            AssertAditional.SetEquals(new HashSet<string>{ null }, instance.GetAllNewDllPaths(id), "null entry");

            var mapB = instance.GetOrCreateOldAssemblyMap(id, "path2");
            SDKMappingSQLConnector.GetInstance().SaveOldSDKMapping(id, "B", "B", nsMap, mapB);
            var targetB = SDKMappingSQLConnector.GetInstance().GetSDKMappingByIdentifiers(id, "B");
            instance.UpdateAssemblyMapping(mapB, targetB, "pathNew", "name");
            SDKMappingSQLConnector.GetInstance().UpdateSDKMapping(targetB, "B");
            AssertAditional.SetEquals(new HashSet<string>{ null, "pathNew" }, instance.GetAllNewDllPaths(id), "first entry");

            var mapC = instance.GetOrCreateOldAssemblyMap(id, "path3");
            var mapD = instance.GetOrCreateOldAssemblyMap(id, "path3");
            SDKMappingSQLConnector.GetInstance().SaveOldSDKMapping(id, "C", "C", nsMap, mapA);
            var targetC = SDKMappingSQLConnector.GetInstance().GetSDKMappingByIdentifiers(id, "C");
            SDKMappingSQLConnector.GetInstance().SaveOldSDKMapping(id, "D", "D", nsMap, mapA);
            var targetD = SDKMappingSQLConnector.GetInstance().GetSDKMappingByIdentifiers(id, "D");
            instance.UpdateAssemblyMapping(mapC, targetC, "pathNew2", "name2");
            instance.UpdateAssemblyMapping(mapD, targetD, "pathNew3", "name3");
            SDKMappingSQLConnector.GetInstance().UpdateSDKMapping(targetC, "C");
            SDKMappingSQLConnector.GetInstance().UpdateSDKMapping(targetD, "D");
            AssertAditional.SetEquals(new HashSet<string> { null, "pathNew", "pathNew2", "pathNew3" },
                instance.GetAllNewDllPaths(id), "repeat old name");

            var mapE = instance.GetOrCreateOldAssemblyMap(id, "path4");
            SDKMappingSQLConnector.GetInstance().SaveOldSDKMapping(id, "E", "E", nsMap, mapA);
            var targetE = SDKMappingSQLConnector.GetInstance().GetSDKMappingByIdentifiers(id, "E");
            instance.UpdateAssemblyMapping(mapE, targetE, "pathNew", "name");
            SDKMappingSQLConnector.GetInstance().UpdateSDKMapping(targetE, "E");
            AssertAditional.SetEquals(new HashSet<string> { null, "pathNew", "pathNew2", "pathNew3" },
                instance.GetAllNewDllPaths(id), "dupplicate new entry");
        }

        [TestMethod()]
        public void TestAssemblyMappingSQLConnectorGetAllOldDllPathsTest()
        {
            var nsMap = NSMappingSQLConnector.GetInstance().GetOrCreateOldNSMap(id, "A");

            AssertAditional.SetEquals(new HashSet<string> { }, instance.GetAllOldDllPaths(id), "initial value");

            var mapA = instance.GetOrCreateOldAssemblyMap(id, "path");
            var mapB = instance.GetOrCreateOldAssemblyMap(id, "path");
            AssertAditional.SetEquals(new HashSet<string> { "path" }, instance.GetAllOldDllPaths(id), "added entry");

            instance.GetOrCreateOldAssemblyMap(id, "path2");
            AssertAditional.SetEquals(new HashSet<string> { "path", "path2" },
                instance.GetAllOldDllPaths(id), "added second entry");

            SDKMappingSQLConnector.GetInstance().SaveOldSDKMapping(id, "A", "A", nsMap, mapA);
            var targetA = SDKMappingSQLConnector.GetInstance().GetSDKMappingByIdentifiers(id, "A");
            SDKMappingSQLConnector.GetInstance().SaveOldSDKMapping(id, "B", "B", nsMap, mapA);
            var targetB = SDKMappingSQLConnector.GetInstance().GetSDKMappingByIdentifiers(id, "B");
            instance.UpdateAssemblyMapping(mapA, targetA, "pathNew", "name");
            instance.UpdateAssemblyMapping(mapB, targetB, "pathNew2", "name2");
            SDKMappingSQLConnector.GetInstance().UpdateSDKMapping(targetA, "A");
            SDKMappingSQLConnector.GetInstance().UpdateSDKMapping(targetA, "B");
            AssertAditional.SetEquals(new HashSet<string> { "path", "path2" },
                instance.GetAllOldDllPaths(id), "double count of first entry");
        }

        [TestMethod()]
        public void TestAssemblyMappingSQLConnectorGetAllNewDllPathsWithFullNameTest()
        {
            var nsMap = NSMappingSQLConnector.GetInstance().GetOrCreateOldNSMap(id, "A");

            AssertAditional.DictionaryEquals(new Dictionary<string, string> { },
                instance.GetAllNewDllPathsWithFullName(id), "initial value");

            var mapA = instance.GetOrCreateOldAssemblyMap(id, "path");
            SDKMappingSQLConnector.GetInstance().SaveOldSDKMapping(id, "A", "A", nsMap, mapA);
            var targetA = SDKMappingSQLConnector.GetInstance().GetSDKMappingByIdentifiers(id, "A");
            AssertAditional.DictionaryEquals(new Dictionary<string, string> { },
                instance.GetAllNewDllPathsWithFullName(id), "null entry");

            var mapB = instance.GetOrCreateOldAssemblyMap(id, "path2");
            SDKMappingSQLConnector.GetInstance().SaveOldSDKMapping(id, "B", "B", nsMap, mapA);
            var targetB = SDKMappingSQLConnector.GetInstance().GetSDKMappingByIdentifiers(id, "B");
            instance.UpdateAssemblyMapping(mapB, targetB, "pathNew", "name");
            SDKMappingSQLConnector.GetInstance().UpdateSDKMapping(targetB, "B");
            AssertAditional.DictionaryEquals(new Dictionary<string, string> { { "pathNew", "name" } },
                instance.GetAllNewDllPathsWithFullName(id), "first entry");

            var mapC = instance.GetOrCreateOldAssemblyMap(id, "path3");
            var mapD = instance.GetOrCreateOldAssemblyMap(id, "path3");
            SDKMappingSQLConnector.GetInstance().SaveOldSDKMapping(id, "C", "C", nsMap, mapA);
            var targetC = SDKMappingSQLConnector.GetInstance().GetSDKMappingByIdentifiers(id, "C");
            SDKMappingSQLConnector.GetInstance().SaveOldSDKMapping(id, "D", "D", nsMap, mapA);
            var targetD = SDKMappingSQLConnector.GetInstance().GetSDKMappingByIdentifiers(id, "D");
            instance.UpdateAssemblyMapping(mapC, targetC, "pathNew2", "name2");
            instance.UpdateAssemblyMapping(mapD, targetD, "pathNew3", "name3");
            SDKMappingSQLConnector.GetInstance().UpdateSDKMapping(targetB, "C");
            SDKMappingSQLConnector.GetInstance().UpdateSDKMapping(targetB, "D");
            AssertAditional.DictionaryEquals(new Dictionary<string, string> { { "pathNew", "name" }, { "pathNew2", "name2" },
                { "pathNew3","name3" } }, instance.GetAllNewDllPathsWithFullName(id), "repeat old name");

            var mapE = instance.GetOrCreateOldAssemblyMap(id, "path4");
            SDKMappingSQLConnector.GetInstance().SaveOldSDKMapping(id, "E", "E", nsMap, mapA);
            var targetE = SDKMappingSQLConnector.GetInstance().GetSDKMappingByIdentifiers(id, "E");
            instance.UpdateAssemblyMapping(mapE, targetE, "pathNew", "name");
            SDKMappingSQLConnector.GetInstance().UpdateSDKMapping(targetB, "E");
            AssertAditional.DictionaryEquals(new Dictionary<string, string> { { "pathNew", "name" }, { "pathNew2", "name2" },
                { "pathNew3","name3" } }, instance.GetAllNewDllPathsWithFullName(id), "dupplicate new entry");
        }
    }
}