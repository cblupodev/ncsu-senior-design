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
            FujitsuConnectorDataContext dbConnection = new FujitsuConnectorDataContext();

            //sdk_mapping newMapping = new sdk_mapping
            //{
            //    model_identifier = "456",
            //    new_sdk = "test.old",
            //    old_sdk = "test.new"
            //};



            sdk newSDK = new sdk
            {
                name = "test2"
            };

            //dbConnection.sdk_mappings.InsertOnSubmit(newMapping);

            dbConnection.sdks.InsertOnSubmit(newSDK);

            try
            {
                dbConnection.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            //var query = dbConnection.sdk_mappings.Where(m => m.model_identifier == "456");

            //foreach (var q in query)
            //{
            //    Console.WriteLine(q.model_identifier);
            //    Console.WriteLine(q.old_namespace);
            //    Console.WriteLine(q.new_namespace);
            //    Console.ReadLine();
            //}
        }
    }
}
