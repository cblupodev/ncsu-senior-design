using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CreateMappings;
using EFSQLConnector;
using System.IO;

namespace CreateMappings
{
    public class ReadFile
    {
        // find the custom attributes in a dll and add mapping to the dictionary
        public void FindCustomAttributes(string dllPath, bool isOld)
        {

            FileHelper.verifyFileExists(dllPath);
            var dom = AppDomain.CreateDomain("test");
            var loadClass = (LoadingClass)dom.CreateInstanceFromAndUnwrap(typeof(LoadingClass).Assembly.Location, typeof(LoadingClass).FullName);
            try
            {
                loadClass.DoStuff(dllPath, isOld, ReadProject.sdkId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }
            AppDomain.Unload(dom);
            // TODO either figure out how to modify the mapList from DoStuff, or interact with the database dirrectly from DoStuff
            // TODO rename classes to something better
        }
    }

    public class LoadingClass : MarshalByRefObject
    {
        public void DoStuff(string dllPath, bool isOld, int sdkId)
        {
            var assem = Assembly.LoadFrom(dllPath);
            string assemFullName = assem.FullName;

            Console.WriteLine("Read from   " + dllPath);

            var types = assem.GetTypes(); // the types will tell you if there are custom data attributes
            foreach (var type in types) // itereate over old dll to find custom attributes
            {
                foreach (var attr in type.CustomAttributes)
                {
                    if (attr.AttributeType.Name.Equals(ReadProject.CustomAttributeName))
                    {
                        string modelIdentifier = (string)attr.ConstructorArguments.First().Value;
                        if (isOld)
                        {
                            namespace_map nsMap = NSMappingSQLConnector.GetInstance().GetOrCreateOldNSMap(sdkId, type.Namespace);
                            assembly_map asMap = AssemblyMappingSQLConnector.GetInstance().GetOrCreateOldAssemblyMap(sdkId, dllPath);
                            SDKMappingSQLConnector.GetInstance().SaveOldSDKMapping(sdkId, modelIdentifier, type.Name, nsMap, asMap);
                        }
                        else
                        {
                            sdk_map2 sdkMap = SDKMappingSQLConnector.GetInstance().GetSDKMappingByIdentifiers(sdkId, modelIdentifier);
                            if (sdkMap != null)
                            {
                                NSMappingSQLConnector.GetInstance().UpdateOrCreateNSMapping(sdkMap.namespace_map, sdkMap, type.Namespace);
                                AssemblyMappingSQLConnector.GetInstance().UpdateAssemblyMapping(sdkMap.assembly_map, dllPath, assemFullName);
                                SDKMappingSQLConnector.GetInstance().UpdateSDKMapping(sdkMap, type.Name);
                            }
                        }
                    }
                }
            }
        }
    }
}
