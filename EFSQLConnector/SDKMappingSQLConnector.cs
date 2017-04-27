using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace EFSQLConnector
{
    public class SDKMappingSQLConnector
    {
        private static SDKMappingSQLConnector instance;
        private MappingContext dbConnection = new MappingContext();

        public static SDKMappingSQLConnector GetInstance()
        {
            if (instance == null)
            {
                instance = new SDKMappingSQLConnector();
            }
            return instance;
        }

        public void SaveOldSDKMapping(int sdkId, string modelIdentifier, string className, namespace_map nsMap, assembly_map asMap)
        {
            sdk_map2 sdkMap = new sdk_map2
            {
                model_identifier = modelIdentifier,
                old_classname = className,
                sdk_id = sdkId,
                namespace_map_id = nsMap.id,
                assembly_map_id = asMap.id
            };
            dbConnection.sdk_map2.Add(sdkMap);
            try
            {
                dbConnection.SaveChanges();
            }
            catch (Exception)
            {
                //Do nothing
            }
        }

        public sdk_map2 GetSDKMappingByIdentifiers(int sdkId, string modelIdentifier)
        {
            Expression<Func<sdk_map2, bool>> whereClause = m => m.model_identifier == modelIdentifier && m.sdk_id == sdkId;
            return GetByWhereClause(whereClause);
        }

        private sdk_map2 GetByWhereClause(Expression<Func<sdk_map2, bool>> whereClause)
        {
            var res = dbConnection.sdk_map2.Where(whereClause);
            try
            {
                sdk_map2 row = res.Single();
                return row;
            }
            catch (Exception)
            {
                //Do nothing
            }
            return null;
        }

        public List<sdk_map2> GetAllBySdkId(int sdkId)
        {
            return dbConnection.sdk_map2.Where(s => s.sdk_id == sdkId).ToList();
        }

        public void UpdateSDKMapping(sdk_map2 sdkMap, string name)
        {
            sdkMap.new_classname = name;
            try
            {
                dbConnection.SaveChanges();
            }
            catch (Exception)
            {
                //Do nothing
            }
        }

        public sdk_map2 GetSDKMapFromClassAndNamespace(int sdkId, string nsString, string className)
        {
            var query = (from sm in dbConnection.sdk_map2
                         join nm in dbConnection.namespace_map on sm.namespace_map_id equals nm.id
                         where nm.old_namespace == nsString && sm.old_classname == className && nm.sdk_id == sdkId && sm.sdk_id == sdkId
                         select sm);
            if (query.Any())
            {
                return query.First();
            }
            return null;
        }
    }
}