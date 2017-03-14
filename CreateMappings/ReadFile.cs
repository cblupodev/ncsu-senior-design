using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NamespaceRefactorer;
using CreateMappings;
using DBConnector;
using System.IO;

namespace NamespaceRefactorer
{
    public class ReadFile
    {
        // find the custom attributes in a dll and add mapping to the dictionary
        public void findCustomAttributes(string dllPath, List<GenericMapping> mapList)
        {

            Helper.verifyFileExists(dllPath);
            var dom = AppDomain.CreateDomain("test");
            var loadClass = (LoadingClass)dom.CreateInstanceAndUnwrap(typeof(LoadingClass).Assembly.FullName, typeof(LoadingClass).FullName);
            loadClass.DoStuff(dllPath, mapList);
            Console.ReadKey();
            // TODO either figure out how to modify the mapList from DoStuff, or interact with the database dirrectly from DoStuff
            // TODO rename classes to something better
        }
    }

    public class LoadingClass : MarshalByRefObject
    {
        public void DoStuff(string dllPath, List<GenericMapping> mapList)
        {
            var assem = Assembly.LoadFrom(dllPath);

            Console.WriteLine("Read from   " + dllPath);

            var types = assem.GetTypes(); // the types will tell you if there are custom data attributes
            foreach (var type in types) // itereate over old dll to find custom attributes
            {
                foreach (var attr in type.CustomAttributes)
                {
                    if (attr.AttributeType.Name.Equals(ReadProject.CustomAttributeName))
                    {
                        string modelIdentifier = (string)attr.ConstructorArguments.First().Value;
                        GenericMapping ma = new GenericMapping(type.Namespace, modelIdentifier, type.Name, dllPath, ReadProject.sdkId);
                        mapList.Add(ma);
                    }
                }
            }
        }
    }
}
