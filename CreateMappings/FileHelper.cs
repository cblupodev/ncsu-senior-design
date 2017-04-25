﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateMappings
{
    public class FileHelper
    {
        public static void verifyFileExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("this file doesn't exist   "+filePath);
            }
        }

        public static void verifyFolderExists(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("this directory doesn't exist   "+folderPath);
            }
        }
    }
}
