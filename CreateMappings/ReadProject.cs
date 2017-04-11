using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFSQLConnector;

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

            SDKSQLConnector.GetInstance().SaveSDK(sdkName, newFolderPath);
            sdkId = SDKSQLConnector.GetInstance().getByName(sdkName).id;
            readFolderDllFiles(oldFolderPath, true);
            readFolderDllFiles(newFolderPath, false);

            Console.WriteLine("Mappings were saved to database");
        }

        // itereate over all the dll files in a folder
        // for each file ,find the custom attributes
        public void readFolderDllFiles(string folderPath, bool isOld) {

            ReadFile rf = new ReadFile();
            FileHelper.verifyFolderExists(folderPath);
            string[] dllFiles = Directory.GetFiles(folderPath, "*.dll", SearchOption.AllDirectories); // get all the dll file paths

            foreach (string dll in dllFiles)
            {
                rf.findCustomAttributes(dll, isOld);
            }
        }

       
    }
}
