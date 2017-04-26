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
    public class NSMappingSQLConnectorTests
    {
        private NSMappingSQLConnector instance;
        private string name;
        private int id;
        [TestInitialize]
        public void Setup()
        {
            name = Guid.NewGuid().ToString();
            SDKSQLConnector.GetInstance().SaveSDK(name, "");
            id = SDKSQLConnector.GetInstance().getByName(name).id;
            instance = NSMappingSQLConnector.GetInstance();
        }
        [TestCleanup]
        public void Teardown()
        {
            SDKSQLConnector.GetInstance().DeleteSDKByName(name);
        }
        [TestMethod()]
        public void TestNSMappingSQLConnectorGetOrCreateOldNSMap()
        {
            var mapA = instance.GetOrCreateOldNSMap(id, "space");
            var expectA = new namespace_map
            {
                old_namespace = "path",
                sdk_id = id
            };
            AssertAditional.NamespaceMapEquals(expectA, mapA, "issue on first create");

            var mapB = instance.GetOrCreateOldNSMap(id, "space2");
            var expectB = new namespace_map
            {
                old_namespace = "path2",
                sdk_id = id
            };
            AssertAditional.NamespaceMapEquals(expectB, mapB, "issue on second create");
            AssertAditional.NamespaceMapEquals(expectA, mapA, "impropper modification of A");

            var mapC = instance.GetOrCreateOldNSMap(id, "space3");
            var expectC = new namespace_map
            {
                old_namespace = "space3",
                sdk_id = id,
                id = mapA.id
            };
            AssertAditional.NamespaceMapEquals(expectC, mapC, "issue on first get");
            AssertAditional.NamespaceMapEquals(expectB, mapB, "impropper modification of B");
            AssertAditional.NamespaceMapEquals(expectA, mapA, "impropper modification of A");
            instance.GetOrCreateOldNSMap(id, "space");
        }

        [TestMethod()]
        public void TestNSMappingSQLConnectorUpdateOrCreateNSMapping()
        {
            var mapA = instance.GetOrCreateOldNSMap(id, "space");
            var mapB = instance.GetOrCreateOldNSMap(id, "space");

            var targetSdkMap = new sdk_map2
            {
                namespace_map_id = mapA.id,
                namespace_map = mapA
            };

            instance.UpdateOrCreateNSMapping(mapA, targetSdkMap, "spaceNew");
            mapA = targetSdkMap.namespace_map;
            var expectNewA = new namespace_map
            {
                old_namespace = "space",
                sdk_id = id,
                new_namespace = "spaceNew"
            };
            AssertAditional.NamespaceMapEquals(expectNewA, mapA, "issue on update");
            
            instance.UpdateOrCreateNSMapping(mapB, targetSdkMap, "spaceNew2");
            mapB = targetSdkMap.namespace_map;
            var expectNewB = new namespace_map
            {
                old_namespace = "space",
                sdk_id = id,
                new_namespace = "spaceNew2"
            };
            AssertAditional.NamespaceMapEquals(expectNewB, mapB, "issue on splitting");
            Assert.AreNotEqual(0, mapB.id, "map probably isn't in the database");
            AssertAditional.NamespaceMapEquals(expectNewA, mapA, "impropper modification of A");
        }

        [TestMethod()]
        public void TestNSMappingSQLConnectorGetAllNamespaces()
        {
            AssertAditional.SetEquals(new HashSet<string> { }, instance.GetAllNamespaces(id), "initial value");

            var mapA = instance.GetOrCreateOldNSMap(id, "space");
            var mapB = instance.GetOrCreateOldNSMap(id, "space");
            AssertAditional.SetEquals(new HashSet<string> { "space" }, instance.GetAllNamespaces(id), "added entry");

            var mapC = instance.GetOrCreateOldNSMap(id, "space2");
            AssertAditional.SetEquals(new HashSet<string> { "space", "space2" },
                instance.GetAllNamespaces(id), "added second entry");

            var targetSdkMap = new sdk_map2();

            instance.UpdateOrCreateNSMapping(mapA, targetSdkMap, "spaceNew");
            instance.UpdateOrCreateNSMapping(mapA, targetSdkMap, "spaceNew2");
            AssertAditional.SetEquals(new HashSet<string> { "space", "space2" },
                instance.GetAllNamespaces(id), "split first entry");
        }

        [TestMethod()]
        public void TestNSMappingSQLConnectorGetNamespaceMapsFromOldNamespace()
        {
            Func<namespace_map,namespace_map,bool> equals = (a,b) => a.id == b.id;
            AssertAditional.ListEquals(new List<namespace_map> { },
                instance.GetNamespaceMapsFromOldNamespace(id, "space"), equals, "initial value");

            var mapA = instance.GetOrCreateOldNSMap(id, "space");
            AssertAditional.ListEquals(new List<namespace_map> { mapA },
                instance.GetNamespaceMapsFromOldNamespace(id, "space"), equals, "null new value");
            AssertAditional.ListEquals(new List<namespace_map> {  },
                instance.GetNamespaceMapsFromOldNamespace(id, "space2"), equals, "null new value different space");

            var targetSdkMap = new sdk_map2();

            var mapB = instance.GetOrCreateOldNSMap(id, "space");
            var mapC = instance.GetOrCreateOldNSMap(id, "space");
            instance.UpdateOrCreateNSMapping(mapB, targetSdkMap, "spaceNew");
            AssertAditional.ListEquals(new List<namespace_map> { mapA, mapB },
                instance.GetNamespaceMapsFromOldNamespace(id, "space"), equals, "add value");
            AssertAditional.ListEquals(new List<namespace_map> { },
                instance.GetNamespaceMapsFromOldNamespace(id, "space2"), equals, "add value different space");

            instance.UpdateOrCreateNSMapping(mapC, targetSdkMap, "spaceNew2");
            AssertAditional.ListEquals(new List<namespace_map> { mapA, mapB, mapC },
                instance.GetNamespaceMapsFromOldNamespace(id, "space"), equals, "split value");
            AssertAditional.ListEquals(new List<namespace_map> { },
                instance.GetNamespaceMapsFromOldNamespace(id, "space2"), equals, "split value different space");

            var mapD = instance.GetOrCreateOldNSMap(id, "space2");
            instance.UpdateOrCreateNSMapping(mapD, targetSdkMap, "space");
            AssertAditional.ListEquals(new List<namespace_map> { mapD },
                instance.GetNamespaceMapsFromOldNamespace(id, "space2"), equals, "add value to different space");
            AssertAditional.ListEquals(new List<namespace_map> { mapA, mapB, mapC },
                instance.GetNamespaceMapsFromOldNamespace(id, "space"), equals, "original space should be umodified");

        }
    }
}