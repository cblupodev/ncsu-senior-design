using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Entity;
using NamespaceRefactorerC; // TODO change the namespace name to remove the C, if you do this you have conflicts though, fix that conflict as well

namespace NamespaceRefactorer
{
    class TestDBConnection
    {
        static void Main(String[] args)
        {

            //Create a new database context
            DataClasses1DataContext db = new DataClasses1DataContext();

            //Set the values for the sdk_mapping table. This should be in a loop in our real project that goes through and creates
            //stuff one by one. We should then put all of the mappings into a list of some sort, so we only have to make one database
            //call to save instead of a database call for each one - but for now this is simple
            //As a side note, you will not be able to run this if the model identifier already exists in the db...so change these values
            //if you want to use with it.
            sdk_mapping newMapping = new sdk_mapping
            {
                model_identifier = "123"
               , new_sdk = "NewSDKIsBetter"
               , old_sdk = "OldSDKIsBetter"
            };

            //This tells the database context to insert the values into the table when we call the submit function
            db.sdk_mappings.InsertOnSubmit(newMapping);

            //We try to save the changes, printing an exception if it fails
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //This is how to retrieve items from the databse. This is basically SQL syntax mised with function calls.
            //First you declare your variable (results). Then you tell it to get the data from our db connection, and specify the
            //table, sdk_mappings. Then you give it tany clauses such as where, orderby, etc. You can also specify any aggregate functions
            //such as joins, etc. This first example selects with one where clause, the second example selects with multiple where clauses
            //Once you have the results, you can iterate over it and do stuff
            var results = db.sdk_mappings.Where(m => m.model_identifier == "12345");

            foreach (var r in results)
            {
                Console.WriteLine(r.model_identifier);
                Console.WriteLine(r.new_sdk);
                Console.WriteLine(r.old_sdk);
            }

            results = db.sdk_mappings.Where(m => m.model_identifier == "12345" 
                                                || m.model_identifier == "123456");

            foreach (var r in results)
            {
                Console.WriteLine(r.model_identifier);
                Console.WriteLine(r.new_sdk);
                Console.WriteLine(r.old_sdk);
            }
        }
      
    }
}
