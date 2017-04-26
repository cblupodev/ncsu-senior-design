using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.WhiteBox
{
    [TestClass]
    [DeploymentItem("specialTests/FileHelper", "FileHelper")]
    public class FileHelperTests
    {
        private TextLogger logger;
        [TestInitialize]
        public void Setup()
        {
            logger = new TextLogger(Console.Out);
            Console.SetOut(logger);
        }
        [TestCleanup]
        public void Teardown()
        {
            Console.SetOut(logger.GetTarget());
        }
        [TestMethod]
        public void TestFileHelperVerifyFileCreateMapping()
        {
            var path = Path.Combine("FileHelper", "file");
            CreateMappings.FileHelper.verifyFileExists(path);
            Assert.AreEqual("", logger.GetAndClearValue(), "file should have been found");
            path = Path.Combine("FileHelper", "notAFile");
            CreateMappings.FileHelper.verifyFileExists(path);
            Assert.AreEqual("this file doesn't exist   " + path + Environment.NewLine, logger.GetAndClearValue(), "file should not have been found");
            path = Path.Combine("FileHelper", "folder");
            CreateMappings.FileHelper.verifyFileExists(path);
            Assert.AreEqual("this file doesn't exist   " + path + Environment.NewLine, logger.GetAndClearValue(), "folders aren't files");
            CreateMappings.FileHelper.verifyFileExists(null);
            Assert.AreEqual("this file doesn't exist   " + Environment.NewLine, logger.GetAndClearValue(), "a null path shouldn't be accepted");
        }
        [TestMethod]
        public void TestFileHelperVerifyFolderCreateMapping()
        {
            var path = Path.Combine("FileHelper", "folder");
            CreateMappings.FileHelper.verifyFolderExists(path);
            Assert.AreEqual("", logger.GetAndClearValue(), "folder should have been found");
            path = Path.Combine("FileHelper", "notAFolder");
            CreateMappings.FileHelper.verifyFolderExists(path);
            Assert.AreEqual("this directory doesn't exist   " + path + Environment.NewLine, logger.GetAndClearValue(), "folder should not have been found");
            path = Path.Combine("FileHelper", "file");
            CreateMappings.FileHelper.verifyFolderExists(path);
            Assert.AreEqual("this directory doesn't exist   " + path + Environment.NewLine, logger.GetAndClearValue(), "files aren't folders");
            CreateMappings.FileHelper.verifyFolderExists(null);
            Assert.AreEqual("this directory doesn't exist   " + Environment.NewLine, logger.GetAndClearValue(), "a null path shouldn't be accepted");
        }
        [TestMethod]
        public void TestFileHelperVerifyFileTransformClient()
        {
            var path = Path.Combine("FileHelper", "file");
            TransformClient.Helper.verifyFileExists(path);
            Assert.AreEqual("", logger.GetAndClearValue(), "file should have been found");
            path = Path.Combine("FileHelper", "notAFile");
            TransformClient.Helper.verifyFileExists(path);
            Assert.AreEqual("this file doesn't exist   " + path + Environment.NewLine, logger.GetAndClearValue(), "file should not have been found");
            path = Path.Combine("FileHelper", "folder");
            TransformClient.Helper.verifyFileExists(path);
            Assert.AreEqual("this file doesn't exist   " + path + Environment.NewLine, logger.GetAndClearValue(), "folders aren't files");
            TransformClient.Helper.verifyFileExists(null);
            Assert.AreEqual("this file doesn't exist   " + Environment.NewLine, logger.GetAndClearValue(), "a null path shouldn't be accepted");
        }
        [TestMethod]
        public void TestFileHelperVerifyFolderTransformClient()
        {
            var path = Path.Combine("FileHelper", "folder");
            TransformClient.Helper.verifyFolderExists(path);
            Assert.AreEqual("", logger.GetAndClearValue(), "folder should have been found");
            path = Path.Combine("FileHelper", "notAFolder");
            TransformClient.Helper.verifyFolderExists(path);
            Assert.AreEqual("this directory doesn't exist   " + path + Environment.NewLine, logger.GetAndClearValue(), "folder should not have been found");
            path = Path.Combine("FileHelper", "file");
            TransformClient.Helper.verifyFolderExists(path);
            Assert.AreEqual("this directory doesn't exist   " + path + Environment.NewLine, logger.GetAndClearValue(), "files aren't folders");
            TransformClient.Helper.verifyFolderExists(null);
            Assert.AreEqual("this directory doesn't exist   " + Environment.NewLine, logger.GetAndClearValue(), "a null path shouldn't be accepted");
        }
    }
}
