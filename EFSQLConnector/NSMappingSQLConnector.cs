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

        public void UpdateOrCreateNSMapping(namespace_map nsMap, string newNS)
        {
            var query = from nm in dbConnection.namespace_map
                        where nm.sdk_id == nsMap.sdk_id && nm.old_namespace == nsMap.old_namespace
                        select nm;
            var tempNsMap = query.First();
            if (tempNsMap.new_namespace != null && tempNsMap.new_namespace != newNS)
            {
                namespace_map splitNsMap = new namespace_map
                {
                    sdk_id = nsMap.sdk_id,
                    old_namespace = tempNsMap.old_namespace,
                    new_namespace = newNS
                };
                dbConnection.namespace_map.Add(splitNsMap);
            }
            else
            {
                nsMap.new_namespace = newNS;
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

    }
}
