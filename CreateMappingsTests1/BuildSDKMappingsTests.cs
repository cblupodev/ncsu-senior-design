using Microsoft.VisualStudio.TestTools.UnitTesting;
using NamespaceRefactorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamespaceRefactorer.Tests
{
    [TestClass()]
    public class BuildSDKMappingsTests
    {
        [TestMethod()]
        public void findCustomerAttributesTest()
        {
            BuildSDKMappings bsdkm = new BuildSDKMappings();
            List<Mapping> mappingsExpected = new List<Mapping>();
            mappingsExpected.Add(new Mapping("SDK", "SDK", "00000000-0000-4000-8000-00000001", "Sample1", "Change1"));
            List<Mapping> mappingsActual = bsdkm.findCustomerAttributes("alias_old.dll", "alias_new.dll");

            CollectionAssert.AreEquivalent(mappingsExpected, mappingsActual); // http://stackoverflow.com/questions/11055632/how-to-compare-lists-in-unit-testing
            Assert.Fail();
        }
    }
}