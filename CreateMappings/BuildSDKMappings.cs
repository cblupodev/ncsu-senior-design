using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NamespaceRefactorer;

namespace NamespaceRefactorer
{
    public class BuildSDKMappings
    {
        // param 0 = old sdk dll file
        // param 1 = new sdk dll file
        public static void Main(string[] args)
        {
            BuildSDKMappings bsm = new BuildSDKMappings();
            bsm.run(args[0], args[1]);
        }

        private object customAttributeName = "ModelIdentifier";

        public void run(string oldSDK, string newSDK)
        {
            string[] mockOldUsings = { "FujitsuSDKOld" }; // magic
        }

        // return list of custom atribute strings that exist in the file
        public List<Mapping> findCustomerAttributes(string oldDllPath, string newDllPath)
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
                        // Mapping mapping = new Mapping();
                        Mapping mapping = new NamespaceRefactorer.Mapping(type.Namespace, (string)attr.ConstructorArguments.First().Value, type.Name);
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
