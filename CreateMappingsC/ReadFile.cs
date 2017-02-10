using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NamespaceRefactorer;
using CreateMappings;
using DBConnector;

namespace NamespaceRefactorer
{
    public class ReadFile
    {
        // find the custom attributes in a dll and add mapping to the dictionary
        public void findCustomAttributes(string dllPath)
        {

            Helper.verifyFileExists(dllPath);
            var assem = Assembly.LoadFile(dllPath);

            var types = assem.GetTypes(); // the types will tell you if there are custom data attributes
            foreach (var type in types) // itereate over old dll to find custom attributes
            {
                foreach (var attr in type.CustomAttributes)
                {
                    if (attr.AttributeType.Name.Equals(ReadProject.CustomAttributeName))
                    {
                        string modelIdentifier = (string)attr.ConstructorArguments.First().Value;
                        MappingAgnostic ma = new MappingAgnostic(type.Namespace, modelIdentifier, type.Name, dllPath);
                        ReadProject.Mappings.Add(ma);
                    }
                }
            }
        }
    }
}
