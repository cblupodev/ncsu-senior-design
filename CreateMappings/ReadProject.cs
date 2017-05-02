using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFSQLConnector;

namespace CreateMappings
{
    public class ReadProject
    {

        public static string CustomAttributeName = "ModelIdentifierAttribute";
        public static int sdkId;

        // args 0 = folder for old sdk
        // args 1 = folder for new sdk
        // args 2 = name for sdk (database object)
        public static void Main(string[] args)
        {
            if (args.Length == 3)
            {
                ReadProject rp = new ReadProject();

                rp.Run(args[0], args[1], args[2]);
            }
            else
            {
                Console.WriteLine("Incorrect amount of arguments");
                Console.WriteLine("Usage:   <path to old sdk folder> <path to new sdk folder> <sdk name>");
            }
        }

        private void Run(string oldFolderPath, string newFolderPath, string sdkName)
        {
            Console.WriteLine("Creating new SDK id in database");
            SDKSQLConnector.GetInstance().SaveSDK(sdkName, newFolderPath);
            sdkId = SDKSQLConnector.GetInstance().GetByName(sdkName).id;
            Console.WriteLine("Start reading dlls for ModelIdentiers");           
            ReadFolderDllFiles(oldFolderPath, true);
            ReadFolderDllFiles(newFolderPath, false);

            Console.WriteLine("Mappings were saved to database");
        }

        // itereate over all the dll files in a folder
        // for each file, find the custom attributes
        public void ReadFolderDllFiles(string folderPath, bool isOld)
        {

            ReadFile rf = new ReadFile();
            FileHelper.verifyFolderExists(folderPath);
            string[] dllFiles = Directory.GetFiles(folderPath, "*.dll", SearchOption.AllDirectories); // get all the dll file paths

            foreach (string dll in dllFiles)
            {
                rf.FindCustomAttributes(dll, isOld);
            }
        }
    }
}
