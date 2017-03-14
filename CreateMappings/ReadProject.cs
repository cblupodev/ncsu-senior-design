using NamespaceRefactorer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBConnector;

namespace CreateMappings
{
    // param 0 = folder containing dlls
    public class ReadProject
    {

        public static string CustomAttributeName = "ModelIdentifier";
        public static int sdkId;

        // param 0 = folder for old sdk
        // param 1 = folder for new sdk
        // param 2 = name for sdk (database object)
        public static void Main(string[] args)
        {
            ReadProject rp = new ReadProject();
            rp.run(args[0], args[1], args[2]);
        }

        private void run(string oldFolderPath, string newFolderPath, string sdkName)
        {
            // key = model identifier
            // value = mapping
            // temporarily hold the old sdk partial mappings
            List<GenericMapping> oldMappings = new List<GenericMapping>();
            // temporarily hold the new sdk partial mappings
            List<GenericMapping> newMappings = new List<GenericMapping>();

            SDKSQLConnector.GetInstance().SaveSDK(sdkName, newFolderPath);
            sdkId = SDKSQLConnector.GetInstance().getByName(sdkName).id;
            readFolderDllFiles(oldFolderPath, oldMappings);
            readFolderDllFiles(newFolderPath, newMappings);
            

            SDKMappingSQLConnector.GetInstance().SaveSDKMappings(oldMappings, newMappings, sdkId);
            Console.WriteLine("Mappings were saved to database");
        }

        // itereate over all the dll files in a folder
        // for each file ,find the custom attributes
        public void readFolderDllFiles(string folderPath, List<GenericMapping> mapList) {

            ReadFile rf = new ReadFile();
            Helper.verifyFolderExists(folderPath);
            string[] dllFiles = Directory.GetFiles(folderPath, "*.dll", SearchOption.AllDirectories); // get all the dll file paths

            foreach (string dll in dllFiles)
            {
                rf.findCustomAttributes(dll, mapList);
            }
        }

       
    }
}
