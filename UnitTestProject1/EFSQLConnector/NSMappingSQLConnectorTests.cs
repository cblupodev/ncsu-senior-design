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
                id = -1
            };

            instance.UpdateOrCreateNSMapping(mapA, targetSdkMap, "spaceNew");
            var expectNewA = new namespace_map
            {
                old_namespace = "space",
                sdk_id = id,
                new_namespace = "spaceNew"
            };
            AssertAditional.NamespaceMapEquals(expectNewA, mapA, "issue on update");
            
            instance.UpdateOrCreateNSMapping(mapB, targetSdkMap, "spaceNew2");
            var expectNewB = new namespace_map
            {
                old_namespace = "space",
                sdk_id = id,
                new_namespace = "spaceNew2"
            };
            AssertAditional.NamespaceMapEquals(expectNewB, mapB, "issue on splitting");
            AssertAditional.SDKMapEqual(new sdk_map2 { namespace_map = mapB, namespace_map_id = mapB.id },
                targetSdkMap, "issue on splitting");
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
            Converter<namespace_map,object> getId = x => x.id;
            AssertAditional.ListUniqueAndEqualsById(new List<namespace_map> { },
                instance.GetNamespaceMapsFromOldNamespace(id, "space"), getId, "initial value");

            var mapA = instance.GetOrCreateOldNSMap(id, "space");
            AssertAditional.ListUniqueAndEqualsById(new List<namespace_map> { mapA },
                instance.GetNamespaceMapsFromOldNamespace(id, "space"), getId, "null new value");
            AssertAditional.ListUniqueAndEqualsById(new List<namespace_map> {  },
                instance.GetNamespaceMapsFromOldNamespace(id, "space2"), getId, "null new value different space");

            var targetSdkMap = new sdk_map2();

            var mapB = instance.GetOrCreateOldNSMap(id, "space");
            var mapC = instance.GetOrCreateOldNSMap(id, "space");
            instance.UpdateOrCreateNSMapping(mapB, targetSdkMap, "spaceNew");
            AssertAditional.ListUniqueAndEqualsById(new List<namespace_map> { mapA, mapB },
                instance.GetNamespaceMapsFromOldNamespace(id, "space"), getId, "add value");
            AssertAditional.ListUniqueAndEqualsById(new List<namespace_map> { },
                instance.GetNamespaceMapsFromOldNamespace(id, "space2"), getId, "add value different space");

            instance.UpdateOrCreateNSMapping(mapC, targetSdkMap, "spaceNew2");
            AssertAditional.ListUniqueAndEqualsById(new List<namespace_map> { mapA, mapB, mapC },
                instance.GetNamespaceMapsFromOldNamespace(id, "space"), getId, "split value");
            AssertAditional.ListUniqueAndEqualsById(new List<namespace_map> { },
                instance.GetNamespaceMapsFromOldNamespace(id, "space2"), getId, "split value different space");

            var mapD = instance.GetOrCreateOldNSMap(id, "space2");
            instance.UpdateOrCreateNSMapping(mapD, targetSdkMap, "space");
            AssertAditional.ListUniqueAndEqualsById(new List<namespace_map> { mapD },
                instance.GetNamespaceMapsFromOldNamespace(id, "space2"), getId, "add value to different space");
            AssertAditional.ListUniqueAndEqualsById(new List<namespace_map> { mapA, mapB, mapC },
                instance.GetNamespaceMapsFromOldNamespace(id, "space"), getId, "original space should be umodified");

        }
    }
}