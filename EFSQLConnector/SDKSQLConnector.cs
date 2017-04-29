using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EFSQLConnector
{
    public class SDKSQLConnector
    {
        private static SDKSQLConnector instance;
        private MappingContext dbConnection = new MappingContext();

        public static SDKSQLConnector GetInstance()
        {
            if (instance == null)
            {
                instance = new SDKSQLConnector();
            }
            return instance;
        }

        public int SaveSDK(string sdkName, string outputPath)
        {
            sdk2 dbSdk = new sdk2
            {
                name = sdkName,
                output_path = outputPath
            };
            dbConnection.sdk2.Add(dbSdk);

            try
            {
                return dbConnection.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("This probably happened because that SDK name already exists");
                Environment.Exit(1);
            }
            return -1;
        }

        public sdk2 GetByName(string sdkName)
        {
            return GetByWhereClause(s => s.name == sdkName);
        }

        public sdk2 GetById(int sdkId)
        {
            return GetByWhereClause(s => s.id == sdkId);
        }

        public string GetOutputPathById(int sdkId)
        {
            var entry = GetByWhereClause(s => s.id == sdkId);
            return entry != null ? entry.output_path : null;
        }

        private sdk2 GetByWhereClause(Expression<Func<sdk2, bool>> whereClause)
        {
            var res = dbConnection.sdk2.Where(whereClause);
            try
            {
                sdk2 row = res.Single();
                return row;
            }
            catch (Exception)
            {
                //Do nothing
            }
            return null;
        }

        public int DeleteSDKByName(string name)
        {
            var sdk = dbConnection.sdk2.Where(s => s.name == name);
            if (sdk.Any())
            {
                //Should do cascading delete...
                dbConnection.sdk2.Remove(sdk.First());
                return dbConnection.SaveChanges();
            }
            return -1;
        }

    }
}
