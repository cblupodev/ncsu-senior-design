using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamespaceRefactorer
{
    public class Helper
    {
        public static void verifyFileExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("file doesn't exist", filePath);
            }
        }

        public static void verifyFolderExists(string filePath)
        {
            Console.WriteLine(filePath);
            if (!Directory.Exists(filePath))
            {
                throw new DirectoryNotFoundException("directory doesn't exist");
            }
        }
    }
}
