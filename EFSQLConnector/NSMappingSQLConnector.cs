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

        public void UpdateNSMapping(namespace_map nsMap, string newNS)
        {
            nsMap.new_namespace = newNS;
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
