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
            id = SDKSQLConnector.GetInstance().GetByName(name).id;
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
                old_namespace = "space",
                sdk_id = id
            };
            AssertAditional.NamespaceMapEquals(expectA, mapA, "issue on first create");

            var mapB = instance.GetOrCreateOldNSMap(id, "space2");
            var expectB = new namespace_map
            {
                old_namespace = "space2",
                sdk_id = id
            };
            AssertAditional.NamespaceMapEquals(expectB, mapB, "issue on second create");
            AssertAditional.NamespaceMapEquals(expectA, mapA, "impropper modification of A");

            var mapC = instance.GetOrCreateOldNSMap(id, "space");
            var expectC = new namespace_map
            {
                old_namespace = "space",
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
            
            var asMap = AssemblyMappingSQLConnector.GetInstance().GetOrCreateOldAssemblyMap(id, "path");
            SDKMappingSQLConnector.GetInstance().SaveOldSDKMapping(id, "A", "A", mapA, asMap);
            var targetA = SDKMappingSQLConnector.GetInstance().GetSDKMappingByIdentifiers(id, "A");
            SDKMappingSQLConnector.GetInstance().SaveOldSDKMapping(id, "B", "B", mapB, asMap);
            var targetB = SDKMappingSQLConnector.GetInstance().GetSDKMappingByIdentifiers(id, "B");

            instance.UpdateOrCreateNSMapping(mapA, targetA, "spaceNew");
            SDKMappingSQLConnector.GetInstance().UpdateSDKMapping(targetA, "A");
            var newA = instance.GetNamespaceMapsFromOldNamespace(id, "space").First(x => x.id == targetA.namespace_map_id);
            var expectNewA = new namespace_map
            {
                old_namespace = "space",
                sdk_id = id,
                new_namespace = "spaceNew"
            };
            AssertAditional.NamespaceMapEquals(expectNewA, newA, "issue on update");
            
            instance.UpdateOrCreateNSMapping(mapB, targetB, "spaceNew2");
            SDKMappingSQLConnector.GetInstance().UpdateSDKMapping(targetB, "B");
            var newB = instance.GetNamespaceMapsFromOldNamespace(id, "space").First(x => x.new_namespace == "spaceNew2");
            var expectNewB = new namespace_map
            {
                old_namespace = "space",
                sdk_id = id,
                new_namespace = "spaceNew2"
            };
            AssertAditional.NamespaceMapEquals(expectNewB, newB, "issue on splitting");
            AssertAditional.NamespaceMapEquals(expectNewA, newA, "impropper modification of A");
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
            Assert.IsNull( instance.GetNamespaceMapsFromOldNamespace(id, "space"), "initial value");

            var mapA = instance.GetOrCreateOldNSMap(id, "space");
            var mapB = instance.GetOrCreateOldNSMap(id, "space");
            AssertAditional.ListEquals(new List<namespace_map> { mapA },
                instance.GetNamespaceMapsFromOldNamespace(id, "space"), equals, "null new value");
            Assert.IsNull(instance.GetNamespaceMapsFromOldNamespace(id, "space2"), "null new value different space");
            
            var asMap = AssemblyMappingSQLConnector.GetInstance().GetOrCreateOldAssemblyMap(id, "path");
            SDKMappingSQLConnector.GetInstance().SaveOldSDKMapping(id, "A", "A", mapA, asMap);
            var targetA = SDKMappingSQLConnector.GetInstance().GetSDKMappingByIdentifiers(id, "A");
            SDKMappingSQLConnector.GetInstance().SaveOldSDKMapping(id, "B", "B", mapB, asMap);
            var targetB = SDKMappingSQLConnector.GetInstance().GetSDKMappingByIdentifiers(id, "B");

            instance.UpdateOrCreateNSMapping(mapA, targetA, "spaceNew");
            SDKMappingSQLConnector.GetInstance().UpdateSDKMapping(targetA, "A");
            instance.UpdateOrCreateNSMapping(mapB, targetB, "spaceNew2");
            SDKMappingSQLConnector.GetInstance().UpdateSDKMapping(targetB, "B");
            mapA = instance.GetNamespaceMapsFromOldNamespace(id, "space").First(x => x.new_namespace == "spaceNew");
            mapB = instance.GetNamespaceMapsFromOldNamespace(id, "space").First(x => x.new_namespace == "spaceNew2");
            AssertAditional.ListEquals(new List<namespace_map> { mapA, mapB },
                instance.GetNamespaceMapsFromOldNamespace(id, "space"), equals, "split value");
            Assert.IsNull(instance.GetNamespaceMapsFromOldNamespace(id, "space2"), "split value different space");

            var mapC = instance.GetOrCreateOldNSMap(id, "space2");
            SDKMappingSQLConnector.GetInstance().SaveOldSDKMapping(id, "C", "C", mapC, asMap);
            var targetC = SDKMappingSQLConnector.GetInstance().GetSDKMappingByIdentifiers(id, "C");
            instance.UpdateOrCreateNSMapping(mapC, targetC, "space");
            SDKMappingSQLConnector.GetInstance().UpdateSDKMapping(targetC, "C");
            AssertAditional.ListEquals(new List<namespace_map> { mapC },
                instance.GetNamespaceMapsFromOldNamespace(id, "space2"), equals, "add value to different space");
            AssertAditional.ListEquals(new List<namespace_map> { mapA, mapB },
                instance.GetNamespaceMapsFromOldNamespace(id, "space"), equals, "original space should be umodified");

        }
    }
}