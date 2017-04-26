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
    public class SDKMappingSQLConnectorTests
    {
        private SDKMappingSQLConnector instance;
        private string name;
        private int id;
        [TestInitialize]
        public void Setup()
        {
            name = Guid.NewGuid().ToString();
            SDKSQLConnector.GetInstance().SaveSDK(name, "");
            id = SDKSQLConnector.GetInstance().getByName(name).id;
            instance = SDKMappingSQLConnector.GetInstance();
        }
        [TestCleanup]
        public void Teardown()
        {
            SDKSQLConnector.GetInstance().DeleteSDKByName(name);
        }
        
        private void AssertDatabaseState(List<sdk_map2> expected, string message)
        {
            AssertAditional.ListEquals(expected, instance.GetAllBySdkId(id),
                (a, b) =>
                {
                    return a.model_identifier == b.model_identifier &&
                    a.namespace_map_id == b.namespace_map_id &&
                    a.new_classname == b.new_classname &&
                    a.old_classname == b.old_classname &&
                    a.assembly_map_id == b.assembly_map_id &&
                    a.sdk_id == b.sdk_id;
                }, message);
            expected.ForEach(entry =>
            {
                AssertAditional.SDKMapEquals(entry, instance.GetSDKMappingByIdentifiers(id, entry.model_identifier), message);
            });
        }

        [TestMethod()]
        public void TestSDKMappingSQLConnectorSaveOldSDKMapping()
        {
            var nsMapA = NSMappingSQLConnector.GetInstance().GetOrCreateOldNSMap(id, "space");
            var asMapA = AssemblyMappingSQLConnector.GetInstance().GetOrCreateOldAssemblyMap(id, "path");
            var nsMapB = NSMappingSQLConnector.GetInstance().GetOrCreateOldNSMap(id, "space2");
            var asMapB = AssemblyMappingSQLConnector.GetInstance().GetOrCreateOldAssemblyMap(id, "path2");

            var mapA = new sdk_map2
            {
                namespace_map_id = nsMapA.id,
                assembly_map_id = asMapA.id,
                model_identifier = "A",
                old_classname = "oldA",
                sdk_id = id
            };
            var mapB = new sdk_map2
            {
                namespace_map_id = nsMapA.id,
                assembly_map_id = asMapB.id,
                model_identifier = "B",
                old_classname = "oldB",
                sdk_id = id
            };
            var mapC = new sdk_map2
            {
                namespace_map_id = nsMapB.id,
                assembly_map_id = asMapA.id,
                model_identifier = "C",
                old_classname = "oldC",
                sdk_id = id
            };
            var mapD = new sdk_map2
            {
                namespace_map_id = nsMapB.id,
                assembly_map_id = asMapB.id,
                model_identifier = "D",
                old_classname = "oldD",
                sdk_id = id
            };

            var expected = new List<sdk_map2>();
            AssertDatabaseState(expected, "initial value");

            instance.SaveOldSDKMapping(id, "A", "oldA", nsMapA, asMapA);
            expected.Add(mapA);
            AssertDatabaseState(expected, "first value");


            instance.SaveOldSDKMapping(id, "B", "oldB", nsMapA, asMapB);
            expected.Add(mapB);
            AssertDatabaseState(expected, "second value");


            instance.SaveOldSDKMapping(id, "C", "oldC", nsMapB, asMapA);
            expected.Add(mapC);
            AssertDatabaseState(expected, "third value");


            instance.SaveOldSDKMapping(id, "D", "oldD", nsMapB, asMapB);
            expected.Add(mapD);
            AssertDatabaseState(expected, "fourth value");

            Assert.Fail();
        }

        [TestMethod()]
        public void TestSDKMappingSQLConnectorGetSDKMappingByIdentifiers()
        {
            Assert.IsNull(instance.GetSDKMappingByIdentifiers(-1, "a"), "shouldn't have gotten something from invalid id -1");

            var nsMapA = NSMappingSQLConnector.GetInstance().GetOrCreateOldNSMap(id, "space");
            var asMapA = AssemblyMappingSQLConnector.GetInstance().GetOrCreateOldAssemblyMap(id, "path");
            var nsMapB = NSMappingSQLConnector.GetInstance().GetOrCreateOldNSMap(id, "space2");
            var asMapB = AssemblyMappingSQLConnector.GetInstance().GetOrCreateOldAssemblyMap(id, "path2");

            var expectedA = new sdk_map2
            {
                namespace_map_id = nsMapA.id,
                assembly_map_id = asMapA.id,
                model_identifier = "modelA",
                old_classname = "clazz",
                sdk_id = id,
            };
            var expectedB = new sdk_map2
            {
                namespace_map_id = nsMapB.id,
                assembly_map_id = asMapB.id,
                model_identifier = "modelB",
                old_classname = "clazz2",
                sdk_id = id,
            };

            Assert.IsNull(instance.GetSDKMappingByIdentifiers(id, "modelA"), "value should be initially null");
            Assert.IsNull(instance.GetSDKMappingByIdentifiers(id, "modelB"), "value should be initially null");

            instance.SaveOldSDKMapping(id, "modelA", "clazz", nsMapA, asMapA);
            AssertAditional.SDKMapEquals(expectedA, instance.GetSDKMappingByIdentifiers(id, "modelA"), "");
            Assert.IsNull(instance.GetSDKMappingByIdentifiers(id, "ab"), "value shouldn't exist yet");
            
            instance.SaveOldSDKMapping(id, "modelB", "clazz2", nsMapB, asMapB);
            AssertAditional.SDKMapEquals(expectedA, instance.GetSDKMappingByIdentifiers(id, "modelA"), "");
            AssertAditional.SDKMapEquals(expectedB, instance.GetSDKMappingByIdentifiers(id, "modelB"), "");
        }

        [TestMethod()]
        public void TestSDKMappingSQLConnectorSaveFullSDKMap()
        {
            var nsA = new namespace_map()
            {
                old_namespace = "oldA",
                new_namespace = "newA"
            };
            var nsB = new namespace_map()
            {
                old_namespace = "oldB",
                new_namespace = "newB"
            };
            var asA = new assembly_map()
            {
                old_path = "pathAOld",
                new_path = "pathANew"
            };
            var asB = new assembly_map()
            {
                old_path = "pathBOld",
                new_path = "pathBNew"
            };

            var mapA = new sdk_map2
            {
                namespace_map = nsA,
                assembly_map = asA,
                model_identifier = "A",
                old_classname = "oldA",
                new_classname = "newA"
            };
            var mapB = new sdk_map2
            {
                namespace_map = nsA,
                assembly_map = asB,
                model_identifier = "B",
                old_classname = "oldB",
                new_classname = "newB"
            };
            var mapC = new sdk_map2
            {
                namespace_map = nsB,
                assembly_map = asA,
                model_identifier = "C",
                old_classname = "oldC",
                new_classname = "newC"
            };
            var mapD = new sdk_map2
            {
                namespace_map = nsB,
                assembly_map = asB,
                model_identifier = "D",
                old_classname = "oldD",
                new_classname = "newD"
            };

            instance.SaveFullSDKMap(id, new List<sdk_map2> { mapA, mapB, mapC, mapD });

            AssertDatabaseState(new List<sdk_map2>
            {
                new sdk_map2 {namespace_map_id = nsA.id, assembly_map_id = asA.id, model_identifier = "A",
                    old_classname = "oldA", new_classname = "newA", sdk_id = id},
                new sdk_map2 {namespace_map_id = nsA.id, assembly_map_id = asB.id, model_identifier = "B",
                    old_classname = "oldB", new_classname = "newB", sdk_id = id},
                new sdk_map2 {namespace_map_id = nsB.id, assembly_map_id = asA.id, model_identifier = "C",
                    old_classname = "oldC", new_classname = "newC", sdk_id = id},
                new sdk_map2 {namespace_map_id = nsB.id, assembly_map_id = asB.id, model_identifier = "D",
                    old_classname = "oldD", new_classname = "newD", sdk_id = id}
            }, "");
        }

        [TestMethod()]
        public void TestSDKMappingSQLConnectorGetAllBySdkId()
        {
            Assert.AreEqual(0, instance.GetAllBySdkId(id), "database should have been initialized with no entries");
            Assert.AreEqual(0, instance.GetAllBySdkId(-1), "invalid database shouldn't contain anything");
        }

        [TestMethod()]
        public void TestSDKMappingSQLConnectorUpdateSDKMapping()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void TestSDKMappingSQLConnectorGetSDKMapFromClassAndNamespace()
        {
            Assert.Fail();
        }
    }
}