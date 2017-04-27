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
    public class SDKSQLConnectorTests
    {
        private SDKSQLConnector instance;
        private string name;
        private int id;
        private string name2;
        private int id2;
        [TestInitialize]
        public void Setup()
        {
            instance = SDKSQLConnector.GetInstance();
            name = Guid.NewGuid().ToString();
            instance.SaveSDK(name, "path");
            id = instance.GetByName(name).id;
            name2 = Guid.NewGuid().ToString();
            instance.SaveSDK(name2, "path2");
            id2 = instance.GetByName(name2).id;
        }
        [TestCleanup]
        public void Teardown()
        {
            SDKSQLConnector.GetInstance().DeleteSDKByName(name);
            SDKSQLConnector.GetInstance().DeleteSDKByName(name2);
        }

        [TestMethod()]
        public void TestSDKSQLConnectorGetByName()
        {
            Assert.IsNull(instance.GetByName(Guid.NewGuid().ToString()), "invalid name should result in null");
            AssertAditional.SDKEquals(new sdk2 { id = id, name = name, output_path = "path" }, instance.GetByName(name), "");
            AssertAditional.SDKEquals(new sdk2 { id = id2, name = name2, output_path = "path2" }, instance.GetByName(name2), "");
        }

        [TestMethod()]
        public void TestSDKSQLConnectorGetById()
        {
            Assert.IsNull(instance.GetById(-1), "invalid id should result in null");
            AssertAditional.SDKEquals(new sdk2 { id = id, name = name, output_path = "path" }, instance.GetById(id), "");
            AssertAditional.SDKEquals(new sdk2 { id = id2, name = name2, output_path = "path2" }, instance.GetById(id2), "");
        }

        [TestMethod()]
        public void TestSDKSQLConnectorGetOutputPathById()
        {
            Assert.IsNull(instance.GetOutputPathById(-1), "output path should be null for invalid id");
            Assert.AreEqual("path", instance.GetOutputPathById(id));
            Assert.AreEqual("path2", instance.GetOutputPathById(id2));
        }

        [TestMethod()]
        public void TestSDKSQLConnectorDeleteSDKByName()
        {
            instance.DeleteSDKByName(name);
            Assert.IsNull(instance.GetByName(name), "get by name should be null after delete");
            Assert.IsNull(instance.GetById(id), "get by id should be null after delete");
            AssertAditional.SDKEquals(new sdk2 { id = id2, name = name2, output_path = "path2" },
                instance.GetById(id2), "other entries shouldn't be deleted");

            instance.DeleteSDKByName(name2);
            Assert.IsNull(instance.GetByName(name2), "get by name should be null after delete");
            Assert.IsNull(instance.GetById(id2), "get by id should be null after delete");
        }
    }
}