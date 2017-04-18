using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSQLConnector
{
    public class AssemblyMappingSQLConnector
    {
        private static AssemblyMappingSQLConnector instance;
        private MappingContext dbConnection = new MappingContext();

        public static AssemblyMappingSQLConnector GetInstance()
        {
            if (instance == null)
            {
                instance = new AssemblyMappingSQLConnector();
            }
            return instance;
        }

        public assembly_map GetOrCreateOldAssemblyMap(int sdkId, string dllPath)
        {
            var query = from am in dbConnection.assembly_map
                        where am.sdk_id == sdkId && am.old_path == dllPath
                        select am;
            if (query.Any())
            {
                return query.First();    
            }
            else
            {
                assembly_map asMap = new assembly_map
                {
                    sdk_id = sdkId,
                    old_path = dllPath
                };
                dbConnection.assembly_map.Add(asMap);
                try
                {
                    dbConnection.SaveChanges();
                }
                catch (Exception)
                {
                    //Do nothing
                }
                return asMap;
            }
        }

        public void UpdateAssemblyMapping(assembly_map asMap, string dllPath, string assemFullName)
        {
            asMap.new_path = dllPath;
            asMap.name = assemFullName;
            try
            {
                dbConnection.SaveChanges();
            }
            catch (Exception)
            {
                //Do nothing
            }
        }

        public HashSet<String> GetAllNewDllPaths(int sdkId)
        {
            var query = (from am in dbConnection.assembly_map where am.sdk_id == sdkId select am.new_path).Distinct();
            HashSet<String> newdllSet = new HashSet<String>(query);
            return newdllSet;
        }

        public HashSet<String> GetAllOldDllPaths(int sdkId)
        {
            var query = (from am in dbConnection.assembly_map where am.sdk_id == sdkId select am.old_path).Distinct();
            HashSet<String> olddllSet = new HashSet<String>(query);
            return olddllSet;
        }

        public Dictionary<String, String> GetAllNewDllPathsWithFullName(int sdkId)
        {
            var query = (from am in dbConnection.assembly_map
                         where am.sdk_id == sdkId && am.new_path != null
                         select new
                         {
                             new_assembly_path = am.new_path,
                             new_assembly_full_name = am.name
                         })
                         .Distinct().ToDictionary(am => am.new_assembly_path, am => am.new_assembly_full_name, StringComparer.OrdinalIgnoreCase);
            return query;
        }
    }
}
