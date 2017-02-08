using DBConnector;
using NamespaceRefactorer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamespaceRefactorer
{
    // param 0 = folder path
    // interacts with the entire project to run the refactorer
    public class ProjectTransform
    {
        FujitsuConnectorDataContext dbConnection = new FujitsuConnectorDataContext();

        public static void Main(string[] args)
        {
            ProjectTransform projectTransform = new ProjectTransform();
            projectTransform.run(args);
        }

        // http://stackoverflow.com/questions/4791140/how-do-i-start-a-program-with-arguments-when-debugging
        public void run(string[] args)
        {
            Helper.verifyFolderExists(args[0]);
            ProcessFolder(args[0]);
            var query = dbConnection.sdk_mappings.Where(m => m.old_namespace == "456");
        }

        // Process all files in the directory passed in, recurse on any directories 
        // that are found, and process the files they contain.
        public void ProcessFolder(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory, "*.cs");
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessFolder(subdirectory);
        }

        private void ProcessFile(string fileName)
        {
            FileTransform fileTranform = new FileTransform(fileName);
            fileTranform.findOldUsingsAndReplacIfCertainClassesFound(dbConnection);
        }
    }
}
