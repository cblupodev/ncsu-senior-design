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
    // param 0 = old client project with the old sdk dlls
    // param 1 = new sdk dlls
    public class ReadProject
    {

        public static string CustomAttributeName = "ModelIdentifier";

        // key = model identifier
        // value = mapping
        // http://stackoverflow.com/questions/1273139/c-sharp-java-hashmap-equivalent
        public static Dictionary<string, Mapping> Mappings = new Dictionary<string, Mapping>();

        // param 0 = folder for client project
        // param 1 = folder for new sdk
        public static void Main(string[] args)
        {
            ReadProject rp = new ReadProject();
            rp.run(args[0],args[1]);
        }

        private void run(string clientFolderPath, string newSDKFolderPath)
        {
            readFolderDllFiles(clientFolderPath);
            readFolderDllFiles(newSDKFolderPath);

            SDKMappingSQLConnector.GetInstance().SaveSDKMappings(Mappings.Values.ToList());
        }

        // itereate over all the dll files in a folder
        // for each file ,find the custom attributes
        public void readFolderDllFiles(string folderPath) {

            ReadFile rf = new ReadFile();
            Helper.verifyFolderExists(folderPath);
            string[] dllFiles = Directory.GetFiles(folderPath, "*.dll", SearchOption.AllDirectories); // get all the dll file paths

            foreach (string dll in dllFiles)
            {
                rf.findCustomAttributes(dll);
            }
        }
    }
}
