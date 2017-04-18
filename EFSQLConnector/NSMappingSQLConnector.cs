using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSQLConnector
{
    public class NSMappingSQLConnector
    {
        private static NSMappingSQLConnector instance;
        private MappingContext dbConnection = new MappingContext();

        public static NSMappingSQLConnector GetInstance()
        {
            if (instance == null)
            {
                instance = new NSMappingSQLConnector();
            }
            return instance;
        }

        public namespace_map GetOrCreateOldNSMap(int sdkId, string ns)
        {
            var query = from nm in dbConnection.namespace_map
                        where nm.sdk_id == sdkId && nm.old_namespace == ns
                        select nm;
            if (query.Any())
            {
                return query.First();
            }
            else
            {
                namespace_map nsMap = new namespace_map
                {
                    sdk_id = sdkId,
                    old_namespace = ns
                };
                dbConnection.namespace_map.Add(nsMap);
                try
                {
                    dbConnection.SaveChanges();
                }
                catch (Exception)
                {
                    //Do nothing
                }
                return nsMap;
            }
        }

        public void UpdateOrCreateNSMapping(namespace_map nsMap, sdk_map2 sdkMap, string newNS)
        {
            var query = from nm in dbConnection.namespace_map
                        where nm.sdk_id == nsMap.sdk_id && nm.old_namespace == nsMap.old_namespace && nm.new_namespace == newNS
                        select nm;
            if (!query.Any())
            {
                query = from nm in dbConnection.namespace_map
                            where nm.sdk_id == nsMap.sdk_id && nm.old_namespace == nsMap.old_namespace && nm.new_namespace == null
                            select nm;
                if (!query.Any())
                {
                    namespace_map splitNsMap = new namespace_map
                    {
                        sdk_id = nsMap.sdk_id,
                        old_namespace = nsMap.old_namespace,
                        new_namespace = newNS
                    };
                    sdkMap.namespace_map_id = 0;
                    sdkMap.namespace_map = splitNsMap;
                }
                else
                {
                    nsMap.new_namespace = newNS;
                }
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
        public HashSet<String> GetAllNamespaces(int sdkId)
        {
            var query = (from nm in dbConnection.namespace_map where nm.sdk_id == sdkId select nm.old_namespace).Distinct();
            HashSet<String> namespaceSet = new HashSet<String>(query);
            return namespaceSet;
        }

        public List<namespace_map> GetNamespaceMapsFromOldNamespace(int sdkId, string oldNamespace)
        {
            var query = (from nm in dbConnection.namespace_map
                         where nm.old_namespace == oldNamespace && nm.sdk_id == sdkId
                         select nm);
            if (query.Any())
            {
                return query.ToList();
            }
            return null;
        }

        public Dictionary<String, String> GetOldToNewNamespaceMap(int sdkId)
        {
            var query = (from nm in dbConnection.namespace_map
                         where nm.sdk_id == sdkId
                         select new
                         {
                             col1 = nm.old_namespace,
                             col2 = nm.new_namespace
                         }).Distinct().ToDictionary(sm => sm.col1, sm => sm.col2, StringComparer.OrdinalIgnoreCase);
            return query;
        }

    }
}
