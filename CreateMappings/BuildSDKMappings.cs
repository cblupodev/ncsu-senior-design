using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NamespaceRefactorer
{
    class BuildSDKMappings
    {

        private object customAttributeName = "ModelIdentifier";

        public BuildSDKMappings(string oldSDK, string newSDK)
        {
            string[] mockOldUsings = { "FujitsuSDKOld" }; // magic
        }

        // return list of custom atribute strings that exist in the file
        private List<Mapping> findCustomerAttributes(string oldDllPath, string newDllPath)
        {
            List<Mapping> mappings = new List<Mapping>();

            Helper.verifyFileExists(oldDllPath);
            Helper.verifyFileExists(newDllPath);
            var oldassem = Assembly.LoadFile(oldDllPath);
            var newassem = Assembly.LoadFile(newDllPath);

            var types = oldassem.GetTypes(); // the types will tell you if there are custom data attributes
            foreach (var type in types) // itereate over old dll to find custom attributes
            {
                foreach (var attr in type.CustomAttributes)
                {
                    if (attr.AttributeType.Name.Equals(customAttributeName))
                    {
                        Mapping mapping = new Mapping();
                        mapping.ModelIdentifierGUID = (string)attr.ConstructorArguments.First().Value;
                        mapping.ClassName = type.Name;
                        mapping.OldNamespace = type.Namespace;
                        mappings.Add(mapping);
                    }
                }
            }

            types = newassem.GetTypes();
            foreach (var type in types) // iterate new dll to find the custom attribute mappings
            {
                foreach (var attr in type.CustomAttributes)
                {
                    if (attr.AttributeType.Name.Equals(customAttributeName))
                    {
                        foreach (var mapping in mappings)
                        {
                            if (mapping.ModelIdentifierGUID.Equals((string)attr.ConstructorArguments.First().Value)) // if an existing GUID equals the guid then associate the namespace to the old mapping
                            {
                                mapping.NewNamespace = type.Namespace; // assoicate the model identifier to the new namespace
                            }
                        }
                    }
                }
            }

            return mappings;
        }
    }
}
