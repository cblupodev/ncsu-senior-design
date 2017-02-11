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

        public Boolean SaveSDKMapping(Mapping mappingToSave)
        {
            return SaveSDKMappings(new List<Mapping>() { mappingToSave });
        }

        public Boolean SaveSDKMappings(List<Mapping> mappingsToSave)
        {
            List<sdk_mapping> dbMappings = new List<sdk_mapping>();

            foreach(var m in mappingsToSave)
            {
                sdk_mapping dbMapping = new sdk_mapping
                {
                    model_identifier = m.ModelIdentifierGUID,
                    old_namespace = m.OldNamespace,
                    old_classname = m.OldClassName,
                    new_namespace = m.NewNamespace,
                    new_classname = m.NewClassName
                };
                dbMappings.Add(dbMapping);
            }

            dbConnection.sdk_mappings.InsertAllOnSubmit(dbMappings);

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
        public Boolean SaveSDKMappings2(List<Mapping> mappingsToSave)
        {
            List<sdk_mapping> dbMappings = new List<sdk_mapping>();

            List<String> ids = mappingsToSave.Select(m => m.ModelIdentifierGUID).ToList();
            dbConnection.sdk_mappings.InsertAllOnSubmit(dbMappings);
            var query = from sm in dbConnection.sdk_mappings
                        where ids.Contains(sm.model_identifier)
                        select sm;

            if (!query.Any())
            {
                foreach (var m in mappingsToSave)
                {
                    sdk_mapping dbMapping = new sdk_mapping
                    {
                        model_identifier = m.ModelIdentifierGUID,
                        old_namespace = m.OldNamespace,
                        old_classname = m.OldClassName,
                        //new_namespace = m.NewNamespace,
                        //new_classname = m.NewClassName
                    };
                    dbMappings.Add(dbMapping);
                }
            }
            else
            {
                foreach (sdk_mapping dbMapping in query)
                {
                    var mapping = mappingsToSave.Where(mts => mts.ModelIdentifierGUID == dbMapping.model_identifier).First();
                    dbMapping.new_classname = mapping.NewClassName;
                    dbMapping.new_namespace = mapping.NewNamespace;
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
            Expression<Func<sdk_mapping, bool>> whereClause = m => true;
            return GetAllByWhereClause(whereClause);
        }

        public List<Mapping> GetAllByWhereClause(Expression<Func<sdk_mapping, bool>> whereClause)
        {
            var rows = dbConnection.sdk_mappings.Where(whereClause);
            List<Mapping> mappings = new List<Mapping>();
            foreach (var row in rows)
            {
                Mapping mapping = new Mapping(row.old_namespace, row.new_namespace, row.model_identifier, row.old_classname, row.new_classname);
                mappings.Add(mapping);
            }
            return mappings;
        }

        public Mapping GetByModelidentifier(String modelID)
        {
            Expression<Func<sdk_mapping, bool>> whereClause = m => m.model_identifier == modelID;
            return GetByWhereClause(whereClause);
        }

        private Mapping GetByWhereClause(Expression<Func<sdk_mapping, bool>> whereClause)
        {
            var res = dbConnection.sdk_mappings.Where(whereClause);
            if (res.Count() == 1)
            {
                sdk_mapping row = res.First();
                Mapping mapping = new Mapping(row.old_namespace, row.new_namespace, row.model_identifier, row.old_classname, row.new_classname);
                return mapping;
            }
            return null;
        }
    }
}
