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
            List<Mapping> map = bsdkm.findCustomerAttributes(
                @"C:\Users\Christopher Lupo\Documents\Visual Studio 2015\Projects\2017SpringTeam25\tests\alias\oldSDK\SDK\bin\Debug\SDK.dll",
                @"C:\Users\Christopher Lupo\Documents\Visual Studio 2015\Projects\2017SpringTeam25\tests\alias\newSDK\SDK\bin\Debug\SDK.dll");

            // TOOD overead Mapping.Equals so I can use Assert on it

            // namespace SDK
            // [ModelIdentifier("00000000-0000-4000-8000-00000001")]
            // old class Sample1       
            // new class Change1
        }
    }
}
