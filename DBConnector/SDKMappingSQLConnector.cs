using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DBConnector
{
    public class SDKMappingSQLConnector
    {
        private static SDKMappingSQLConnector instance;
        private FujitsuConnectorDataContext dbConnection = new FujitsuConnectorDataContext();

        public static SDKMappingSQLConnector GetInstance()
        {
            if (instance == null)
            {
                instance = new SDKMappingSQLConnector();
            }
            return instance;
        }

        public Boolean SaveSDKMapping(GenericMapping mappingToSave)
        {
            return SaveSDKMappings2(new List<GenericMapping>() { mappingToSave });
        }
        
        public Boolean SaveSDKMappings2(List<GenericMapping> mappingsToSave)
        {
            List<sdk_map> dbMappings = new List<sdk_map>();

            List<String> ids = mappingsToSave.Select(m => m.ModelIdentifierGUID).ToList();
            var query = from sm in dbConnection.sdk_maps
                        where ids.Contains(sm.model_identifier)
                        select sm;

            if (!query.Any())
            {
                foreach (var m in mappingsToSave)
                {
                    sdk_map dbMapping = new sdk_map
                    {
                        model_identifier = m.ModelIdentifierGUID,
                        old_namespace = m.Namespace,
                        old_classname = m.ClassName,
                        old_assembly_path = m.dllPath,
                        sdk_id = m.sdkId
                    };
                    dbMappings.Add(dbMapping);
                }
                dbConnection.sdk_maps.InsertAllOnSubmit(dbMappings);
            }
            else
            {
                foreach (sdk_map dbMapping in query)
                {
                    var mapping = mappingsToSave.Where(mts => mts.ModelIdentifierGUID == dbMapping.model_identifier).First();
                    dbMapping.new_classname = mapping.ClassName;
                    dbMapping.new_namespace = mapping.Namespace;
                    dbMapping.new_assembly_path = mapping.dllPath;
                }
            }
            
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

        public List<Mapping> GetAll()
        {
            Expression<Func<sdk_map, bool>> whereClause = m => true;
            return GetAllByWhereClause(whereClause);
        }

        public List<Mapping> GetAllByWhereClause(Expression<Func<sdk_map, bool>> whereClause)
        {
            var rows = dbConnection.sdk_maps.Where(whereClause);
            List<Mapping> mappings = new List<Mapping>();
            foreach (var row in rows)
            {
                Mapping mapping = new Mapping(row.old_namespace, row.new_namespace, row.model_identifier, row.old_classname, row.new_classname, row.old_assembly_path, row.new_assembly_path);
                mappings.Add(mapping);
            }
            return mappings;
        }

        public Mapping GetByModelidentifier(String modelID)
        {
            Expression<Func<sdk_map, bool>> whereClause = m => m.model_identifier == modelID;
            return GetByWhereClause(whereClause);
        }

        public Mapping GetByOldNamespace(String oldNamespace, int sdkId)
        {
            Expression<Func<sdk_map, bool>> whereClause = m => (m.old_namespace == oldNamespace) && (m.sdk_id == sdkId);
            List<Mapping> maps =  GetAllByWhereClause(whereClause);
            return maps.First();

        }

        public HashSet<String> GetAllNamespaces(int sdkId)
        {
            var query = (from sm in dbConnection.sdk_maps where sm.sdk_id == sdkId select sm.old_namespace).Distinct();
            HashSet<String> namespaceSet = new HashSet<String>(query);
            return namespaceSet;

        }

        public Dictionary<String, String> GetOldToNewNamespaceMap(int sdkId)
        {
            var query = (from sm in dbConnection.sdk_maps
                         where sm.sdk_id == sdkId
                         select new
                         {
                             col1 = sm.old_namespace,
                             col2 = sm.new_namespace
                         }).Distinct().ToDictionary(sm => sm.col1, sm => sm.col2, StringComparer.OrdinalIgnoreCase);
            return query;
        }

        public Dictionary<String, HashSet<String>> GetNamespaceToClassnameSetMap(int sdkId)
        {
            Dictionary<String, HashSet<String>> namespaceToClassNameSetMap = new Dictionary<String, HashSet<String>>();
            var query = (from sm in dbConnection.sdk_maps where sm.sdk_id == sdkId select sm.old_namespace).Distinct();
            foreach (var ns in query)
            {
                namespaceToClassNameSetMap.Add(ns, GetClassnameSet(ns, sdkId));
            }
            return namespaceToClassNameSetMap;
        }

        private HashSet<String> GetClassnameSet(string ns, int sdkId)
        {
            var query = (from sm in dbConnection.sdk_maps where sm.old_namespace == ns && sm.sdk_id == sdkId select sm.old_classname);
            HashSet<String> classnameSet = new HashSet<String>(query);
            return classnameSet;
        }

        public Mapping GetByFullyQualifiedName(string oldNamespace, string oldClassname, int sdkId)
        {
            Expression<Func<sdk_map, bool>> whereClause = m => (m.old_namespace == oldNamespace) && (m.old_classname == oldClassname) && (m.sdk_id == sdkId);
            return GetByWhereClause(whereClause);
        }

        private Mapping GetByWhereClause(Expression<Func<sdk_map, bool>> whereClause)
        {
            var res = dbConnection.sdk_maps.Where(whereClause);
            try
            {
                sdk_map row = res.Single();
                Mapping mapping = new Mapping(row.old_namespace, row.new_namespace, row.model_identifier, row.old_classname, row.new_classname, row.old_assembly_path, row.new_assembly_path);
                return mapping;
            } catch(Exception e)
            {
                //Do nothing
            }
            return null;
        }

        public Boolean DeleteMappingBySDKId(int sdkId)
        {
            var mappings = dbConnection.sdk_maps.Where(m => m.sdk_id == sdkId);
            dbConnection.sdk_maps.DeleteAllOnSubmit(mappings);

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
