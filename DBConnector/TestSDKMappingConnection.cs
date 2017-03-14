using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnector
{
    class TestSDKMappingConnection
    {
        static void Main(String[] args)
        {
            SDKSQLConnector.GetInstance().SaveSDK("alabama", "uyvyu");
            int sdkId = SDKSQLConnector.GetInstance().getByName("alabama").id;
            List<GenericMapping> oldMappingList = new List<GenericMapping>();
            List<GenericMapping> newMappingList = new List<GenericMapping>();
            oldMappingList.Add(new GenericMapping("oldNamespace", "1234", "oldClassname1", "pathA1", sdkId));
            newMappingList.Add(new GenericMapping("newNamespace", "1234", "newClassname1", "pathA2", sdkId));
            oldMappingList.Add(new GenericMapping("oldNamespace", "4567", "oldClassname2", "pathB1", sdkId));
            newMappingList.Add(new GenericMapping("newNamespace", "4567", "newClassname2", "pathB2", sdkId));
            oldMappingList.Add(new GenericMapping("oldNamespace", "7890", "oldClassname3", "pathC1", sdkId));
            newMappingList.Add(new GenericMapping("newNamespace", "7890", "newClassname3", "pathC2", sdkId));
            oldMappingList.Add(new GenericMapping("oldNamespace1", "4321", "oldClassname4", "pathD1", sdkId));
            newMappingList.Add(new GenericMapping("newNamespace1", "4321", "newClassname5", "pathD2", sdkId));
            SDKMappingSQLConnector.GetInstance().SaveSDKMappings(oldMappingList, newMappingList, sdkId);
            HashSet<String> oldNamespacesSet = SDKMappingSQLConnector.GetInstance().GetAllNamespaces(sdkId);
            if (oldNamespacesSet.Contains("oldNamespace"))
            {
                Console.WriteLine("It worked");
            }
            else
            {
                Console.WriteLine("It did not work");
            }

            Dictionary<String, String> namespaceMap = SDKMappingSQLConnector.GetInstance().GetOldToNewNamespaceMap(sdkId);
            foreach(KeyValuePair<String, String> n in namespaceMap)
            {
                Console.WriteLine(n.ToString());
            }
            Console.ReadLine();
        }
    }
}
