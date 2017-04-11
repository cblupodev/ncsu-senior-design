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

        private sdk_map2 GetSDKMappingByIdentifiers(int sdkId, string modelIdentifier)
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

        public namespace_map GetOldNSForSDKMapping(int sdkId, string modelIdentifier)
        {
            sdk_map2 sdkMap = GetSDKMappingByIdentifiers(sdkId, modelIdentifier);
            return sdkMap.namespace_map;
        }

        public assembly_map GetOldAssemblyForSDKMapping(int sdkId, string modelIdentifier)
        {
            sdk_map2 sdkMap = GetSDKMappingByIdentifiers(sdkId, modelIdentifier);
            return sdkMap.assembly_map;
        }

        public void SaveNewSDKMapping(int sdkId, string modelIdentifier, string name)
        {
            var query = from sm in dbConnection.sdk_map2
                        where sm.model_identifier == modelIdentifier && sm.sdk_id == sdkId
                        select sm;
            if (query.Any())
            {
                sdk_map2 sdkMap = query.First();
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
        }

        public Dictionary<String, String> GetOldToNewNamespaceMap(int sdkId)
        {
            var query = (from sm in dbConnection.sdk_map2
                         where sm.sdk_id == sdkId
                         select new
                         {
                             col1 = sm.namespace_map.old_namespace,
                             col2 = sm.namespace_map.new_namespace
                         }).Distinct().ToDictionary(sm => sm.col1, sm => sm.col2, StringComparer.OrdinalIgnoreCase);
            return query;
        }

        public Dictionary<String, Dictionary<String, String>> GetNamespaceToClassnameMapMap(int sdkId)
        {
            Dictionary<String, Dictionary<String, String>> namespaceToClassNameSetMap = new Dictionary<String, Dictionary<String, String>>();
            var query = (from sm in dbConnection.sdk_map2 where sm.sdk_id == sdkId select sm.namespace_map.old_namespace).Distinct();
            foreach (var ns in query)
            {
                namespaceToClassNameSetMap.Add(ns, GetClassnameMap(ns, sdkId));
            }
            return namespaceToClassNameSetMap;
        }

        private Dictionary<String, String> GetClassnameMap(string ns, int sdkId)
        {
            var query = (from sm in dbConnection.sdk_map2
                         where sm.namespace_map.old_namespace == ns && sm.sdk_id == sdkId
                         select new
                         {
                             col1 = sm.old_classname,
                             col2 = sm.new_classname
                         }).ToDictionary(sm => sm.col1, sm => sm.col2, StringComparer.OrdinalIgnoreCase);
            return query;
        }

    }
}
