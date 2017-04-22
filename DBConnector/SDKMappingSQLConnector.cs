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
        
        public Boolean SaveSDKMappings(List<Mapping> mappings, int sdk_id)
        {
            List<sdk_map> dbMappings = new List<sdk_map>();

            foreach (var m in mappings)
            {
                sdk_map dbMapping = new sdk_map
                {
                    model_identifier = m.ModelIdentifierGUID,
                    old_namespace = m.OldNamespace,
                    old_classname = m.OldClassName,
                    old_assembly_path = m.OldDllPath,
                    new_namespace = m.NewNamespace,
                    new_classname = m.NewClassName,
                    new_assembly_path = m.NewDllPath,
                    sdk_id = sdk_id
                };
                dbMappings.Add(dbMapping);
            }
            dbConnection.sdk_maps.InsertAllOnSubmit(dbMappings);

            try
            {
                dbConnection.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public Boolean SaveSDKMappings(List<GenericMapping> oldMappingsToSave, List<GenericMapping> newMappingsToSave, int sdk_id)
        {
            List<sdk_map> dbMappings = new List<sdk_map>();

            foreach (var m in oldMappingsToSave)
            {
                sdk_map dbMapping = new sdk_map
                {
                    model_identifier = m.ModelIdentifierGUID,
                    old_namespace = m.Namespace,
                    old_classname = m.ClassName,
                    old_assembly_path = m.DllPath,
                    sdk_id = m.SdkId
                };
                dbMappings.Add(dbMapping);
            }
            dbConnection.sdk_maps.InsertAllOnSubmit(dbMappings);

            try
            {
                dbConnection.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            List<String> ids = oldMappingsToSave.Select(m => m.ModelIdentifierGUID).ToList();
            var query = from sm in dbConnection.sdk_maps
                        where ids.Contains(sm.model_identifier) && sm.sdk_id == sdk_id
                        select sm;

            foreach (sdk_map dbMapping in query)
            {
                var mapping = newMappingsToSave.Where(mts => mts.ModelIdentifierGUID == dbMapping.model_identifier).First();
                dbMapping.new_classname = mapping.ClassName;
                dbMapping.new_namespace = mapping.Namespace;
                dbMapping.new_assembly_path = mapping.DllPath;
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

        public Boolean SaveSDKMappings(List<GenericMapping> mappingsToSave, int sdk_id)
        {
            List<sdk_map> dbMappings = new List<sdk_map>();

            List<String> ids = mappingsToSave.Select(m => m.ModelIdentifierGUID).ToList();
            var query = from sm in dbConnection.sdk_maps
                        where ids.Contains(sm.model_identifier) && sm.sdk_id == sdk_id
                        select sm;

            if (query.Any())
            {
                foreach (sdk_map dbMapping in query)
                {
                    var mapping = mappingsToSave.Where(mts => mts.ModelIdentifierGUID == dbMapping.model_identifier).First();
                    dbMapping.new_classname = mapping.ClassName;
                    dbMapping.new_namespace = mapping.Namespace;
                    dbMapping.new_assembly_path = mapping.DllPath;
                    dbMapping.new_assembly_full_name = mapping.NewDllFullName;
                }
            }
            else
            {

                foreach (var m in mappingsToSave)
                {
                    sdk_map dbMapping = new sdk_map
                    {
                        model_identifier = m.ModelIdentifierGUID,
                        old_namespace = m.Namespace,
                        old_classname = m.ClassName,
                        old_assembly_path = m.DllPath,
                        sdk_id = m.SdkId
                    };
                    dbMappings.Add(dbMapping);
                }
                dbConnection.sdk_maps.InsertAllOnSubmit(dbMappings);
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

        public List<Mapping> GetAllSDKMapsBySDKId(int sdk_id)
        {
            return GetAllByWhereClause(m => m.sdk_id == sdk_id);
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

        public HashSet<String> GetAllNewDllPaths(int sdkId)
        {
            var query = (from sm in dbConnection.sdk_maps where sm.sdk_id == sdkId select sm.new_assembly_path).Distinct();
            HashSet<String> newdllSet = new HashSet<String>(query);
            return newdllSet;
        }

        public Dictionary<String, String> GetAllNewDllPathsWithFullName(int sdkId)
        {
            var query = (from sm in dbConnection.sdk_maps where sm.sdk_id == sdkId
                select new
                {
                    new_assembly_path = sm.new_assembly_path,
                    new_assembly_full_name = sm.new_assembly_full_name
                }).Distinct().ToDictionary(sm => sm.new_assembly_path, sm => sm.new_assembly_full_name, StringComparer.OrdinalIgnoreCase);
            return query;
        }

        public class NewDllPathWithFullName
        {
            public string new_assembly_path { get; set; }
            public int new_assembly_full_name { get; set; }
        }

        public HashSet<String> GetAllOldDllPaths(int sdkId)
        {
            var query = (from sm in dbConnection.sdk_maps where sm.sdk_id == sdkId select sm.old_assembly_path).Distinct();
            HashSet<String> olddllSet = new HashSet<String>(query);
            return olddllSet;
        }

        public Dictionary<String, String> GetOldToNewNamespaceMap(int sdkId)
        {
            var query = (from sm in dbConnection.sdk_maps
                         where sm.sdk_id == sdkId
                         select new
                         {
                             col1 = sm.old_namespace,
                             col2 = sm.new_namespace
                         }).Distinct();
                         
            
            return query.ToDictionary(sm => sm.col1, sm => sm.col2, StringComparer.OrdinalIgnoreCase);
        }

        public Dictionary<String, Dictionary<String, String>> GetNamespaceToClassnameMapMap(int sdkId)
        {
            Dictionary < String, Dictionary<String, String>> namespaceToClassNameSetMap = new Dictionary<String, Dictionary<String, String>>();
            var query = (from sm in dbConnection.sdk_maps where sm.sdk_id == sdkId select sm.old_namespace).Distinct();
            foreach (var ns in query)
            {
                namespaceToClassNameSetMap.Add(ns, GetClassnameMap(ns, sdkId));
            }
            return namespaceToClassNameSetMap;
        }

        private Dictionary<String, String> GetClassnameMap(string ns, int sdkId)
        {
            var query = (from sm in dbConnection.sdk_maps
                         where sm.old_namespace == ns && sm.sdk_id == sdkId
                         select new
                         {
                             col1 = sm.old_classname,
                             col2 = sm.new_classname
                         }).ToDictionary(sm => sm.col1, sm => sm.col2, StringComparer.OrdinalIgnoreCase);
            return query;
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
            } catch(Exception)
            {
                //Do nothing
            }
            return null;
        }

        public Boolean DeleteMappingBySDKId(int sdkId)
        {
            var mappings = dbConnection.sdk_maps.Where(m => m.sdk_id == sdkId);
            if (mappings.Any())
            {
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
            return true;
        }
    }
}
