using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DBConnector
{
    public class SDKSQLConnector
    {
        private static SDKSQLConnector instance;
        private FujitsuConnectorDataContext dbConnection = new FujitsuConnectorDataContext();

        public static SDKSQLConnector GetInstance()
        {
            if (instance == null)
            {
                instance = new SDKSQLConnector();
            }
            return instance;
        }

        public Boolean SaveSDK(string sdkName)
        {
            sdk dbSdk = new sdk
            {
                name = sdkName
            };
            dbConnection.sdks.InsertOnSubmit(dbSdk);

            try
            {
                dbConnection.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public sdk getByName(string sdkName)
        {
            return GetByWhereClause(s => s.name == sdkName);
        }

        public sdk getById(int sdkId)
        {
            return GetByWhereClause(s => s.id == sdkId);
        }

        private sdk GetByWhereClause(Expression<Func<sdk, bool>> whereClause)
        {
            var res = dbConnection.sdks.Where(whereClause);
            try
            {
                sdk row = res.Single();
                return row;
            }
            catch (Exception e)
            {
                //Do nothing
            }
            return null;
        }

        public Boolean DeleteSDKByName(string name)
        {
            var sdk = dbConnection.sdks.Where(s => s.name == name).Single();
            //Delete foreign key constraints on sdk before deleting the sdk
            SDKMappingSQLConnector.GetInstance().DeleteMappingBySDKId(sdk.id);
            dbConnection.sdks.DeleteOnSubmit(sdk);

            try
            {
                dbConnection.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
