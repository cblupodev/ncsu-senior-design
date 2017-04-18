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

        public void SaveFullSDKMap(int sdkId, List<sdk_map2> map)
        {
            foreach ( var item in map )
            {
                var sdkMap = new sdk_map2
                {
                    model_identifier = item.model_identifier,
                    old_classname = item.old_classname,
                    sdk_id = sdkId,
                    namespace_map = item.namespace_map,
                    assembly_map = item.assembly_map
                };
                dbConnection.sdk_map2.Add(sdkMap);
            }
            try
            {
                dbConnection.SaveChanges();
            }
            catch (Exception)
            {
                //Do nothing
            }
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