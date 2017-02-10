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

        // key = model identifier
        // value = mapping
        // http://stackoverflow.com/questions/1273139/c-sharp-java-hashmap-equivalent
        //public static Dictionary<string, MappingAgnostic> Mappings = new Dictionary<string, MappingAgnostic>();
        public static List<MappingAgnostic> Mappings = new List<MappingAgnostic>();

        // param 0 = folder for client project
        // param 1 = folder for new sdk
        public static void Main(string[] args)
        {
            ReadProject rp = new ReadProject();
            rp.run(args[0]);
        }

        private void run(string folderPath)
        {
            readFolderDllFiles(folderPath);

            SDKMappingSQLConnector.GetInstance().SaveSDKMappings(Mappings);
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
