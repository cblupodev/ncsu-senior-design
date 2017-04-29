using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Editing;
using System.Xml.Linq;
using EFSQLConnector;

namespace TransformClient
{
    public class TransformProject
    {

        SDKMappingSQLConnector sdkMappingConnector = SDKMappingSQLConnector.GetInstance();
        AssemblyMappingSQLConnector asMappingConnector = AssemblyMappingSQLConnector.GetInstance();
        NSMappingSQLConnector nsMappingConnector = NSMappingSQLConnector.GetInstance();

        public static int sdkId;

        // args 0 = .csproj file path
        // args 1 = sdk name
        public static void Main(string[] args)
        {
            sdkId = SDKSQLConnector.GetInstance().GetByName(args[1]).id;
            (new TransformProject()).ProcessProject(args);
        }

        public void ProcessProject(string[] args)
        {
            Helper.verifyFileExists(args[0]);
            var filePath = args[0];
            if (filePath.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase) || filePath.EndsWith(".vbproj", StringComparison.OrdinalIgnoreCase))
            {
                // put all changes in new project folder
                // create copies of the project so don't have to overwrite the original files
                string projectParentFolder = new FileInfo(filePath).DirectoryName;
                string transformed_folder = projectParentFolder + "_transformed";
                Console.WriteLine("Copying project directory");
                Console.WriteLine("Removing the old transformed directory before copying contents");
                CopyDirectory(transformed_folder, projectParentFolder);
                Console.WriteLine("Begin transforming files");

                // Proccess the project here
                Project proj = MSBuildWorkspace.Create().OpenProjectAsync(filePath).Result;
                HashSet<String> namespaceSet = nsMappingConnector.GetAllNamespaces(sdkId);
                Dictionary<String, HashSet<String>> namespaceToClassnameSetMap = new Dictionary<string, HashSet<string>>();

                foreach (Document doc in proj.Documents)
                {
                    var semanticModel = doc.GetSemanticModelAsync().Result;
                    var syntaxTree = doc.GetSyntaxTreeAsync().Result;

                    // do processing here
                    var documentEditor = DocumentEditor.CreateAsync(doc).Result;

                    if (IsDocCSharp(doc))
                    {
                        TransformFileCSharp ft = new TransformFileCSharp(documentEditor);
                        syntaxTree = ft.ReplaceSyntax();
                    }

                    if (IsDocVB(doc))
                    {
                        TransformFileVBasic ft = new TransformFileVBasic(documentEditor);
                        syntaxTree = ft.ReplaceSyntax();
                    }

                    File.WriteAllText(doc.FilePath.Replace(projectParentFolder, transformed_folder), syntaxTree.GetText().ToString());

                    Console.WriteLine("Transformed   " + doc.FilePath);
                }

                HashSet<String> olddllSet = asMappingConnector.GetAllOldDllPaths(sdkId);
                TransformXml(olddllSet, sdkId, proj.FilePath, projectParentFolder, transformed_folder, Path.GetExtension(proj.FilePath));

                Console.WriteLine("Transformed   " + proj.FilePath);
            }
            else
            {
                Console.WriteLine("first parameter must be .csproj or .vbproj");
            }
        }

        private bool IsDocVB(Document doc)
        {
            return Path.GetExtension(doc.FilePath).Equals(".vb");
        }

        private bool IsDocCSharp(Document doc)
        {
            return Path.GetExtension(doc.FilePath).Equals(".cs");
        }

        // Taken from stackoverflow "Recursive delete of files and directories in C#"
        // id = 925192
        private static void RecursiveDeleteDirectory(DirectoryInfo baseDir)
        {
            try
            {
                if (!baseDir.Exists)
                    return;

                foreach (var dir in baseDir.EnumerateDirectories())
                {
                    RecursiveDeleteDirectory(dir);
                }
                baseDir.Delete(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // Taken from stackoverflow "Copy the entire contents of a directory in C#"
        // id = 58744
        private static void CopyDirectory(string destPath, string sourcePath)
        {
            // automatically remove a directory of the same name so don't have to manually remove it every time you run TransformClient
            RecursiveDeleteDirectory(new DirectoryInfo(destPath));

            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);

                foreach (string file in Directory.GetFiles(sourcePath))
                {
                    string dest = Path.Combine(destPath, Path.GetFileName(file));
                    File.Copy(file, dest);
                }

                foreach (string folder in Directory.GetDirectories(sourcePath))
                {
                    string dest = Path.Combine(destPath, Path.GetFileName(folder));
                    CopyDirectory(dest, folder);
                }
            }
        }

        private void TransformXml(HashSet<String> olddllSet, int sdkid, string csprojFilePath, string currentParent, string newParent, string projectFileExtension)
        {
            string xmlElementOutputPathName = "OutputPath";
            string xmlElementReferenceName = "Reference";
            string xmlElementHintPathName = "HintPath";

            // find the namsespace by calling Descendents() on the Root and drill down into the properties to find the namsespace you need
            XNamespace ns = XNamespace.Get("http://schemas.microsoft.com/developer/msbuild/2003");
            XDocument xdoc = XDocument.Load(csprojFilePath);

            // transform the xml
            string hintPathRoot = GetOldSdkReferenceHintPath(olddllSet, csprojFilePath, xmlElementHintPathName, xmlElementReferenceName, xdoc, ns);
            RemoveOldDllReferences(olddllSet, csprojFilePath, xmlElementHintPathName, xmlElementOutputPathName, xmlElementReferenceName, ns, xdoc);
            // this needs to be done in this order
            string newRelativeOutputPath = ChangeOutputPath(xmlElementOutputPathName, ns, xdoc);
            AddNewDllReferences(csprojFilePath, hintPathRoot, newRelativeOutputPath, xmlElementHintPathName, xmlElementReferenceName, ns, xdoc);

            // save the xml
            xdoc.Save(csprojFilePath.Replace(currentParent, newParent));
        }

        // get the containing folder of the hintpath with the least amount of folders in the hintpath
        // so we can be sure the hintpath we are using to add new references doesn't have extra folders in it
        // so can use the value when adding new references
        private string GetOldSdkReferenceHintPath(HashSet<String> olddllSet, string csprojFilePath, string xmlElementHintPathName, string xmlElementReferenceName, XDocument xdoc, XNamespace ns)
        {
            // need to anchor the working directory to the fodler of the .csproj file so that relative path names will point to the correct location
            string originalCurrentWorkingDirectory = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(new FileInfo(csprojFilePath).DirectoryName);

            // get list of the hintpaths pointing to dlls in the old sdk
            var hintpaths = from reference in xdoc.Descendants(ns + xmlElementReferenceName)
                            where reference.Element(ns + xmlElementHintPathName) != null
                            // where olddllSet.Contains(Path.GetFullPath(reference.Descendants(ns + xmlElementHintPathName).First().Value))
                            select (reference.Descendants(ns + xmlElementHintPathName).First().Value.Split('\\'));

            // find the path with the least amount of folders
            int currentMinLength = Int32.MaxValue;
            int currentMinIndex = 0;
            for (int i = 0; i < hintpaths.Count(); i++)
            {
                if (hintpaths.ElementAt(i).Length < currentMinLength)
                {
                    currentMinLength = hintpaths.ElementAt(i).Length;
                    currentMinIndex = i;
                }
            }

            // return the path with the least amoutn of folders and leave the filename off
            // do it this way with the arrays to preserve relative folder paths
            string[] pathWithLeastFolders = hintpaths.ElementAt(currentMinIndex);
            pathWithLeastFolders.SetValue("", pathWithLeastFolders.Length - 1);

            Directory.SetCurrentDirectory(originalCurrentWorkingDirectory);

            return String.Join("\\", pathWithLeastFolders);
        }

        // add <Reference> tags to the new sdk
        private void AddNewDllReferences(string csprojFilePath, string hintPathRoot, string newRelativeOutputPath, string xmlElementHintPathName, string xmlElementReferenceName, XNamespace ns, XDocument xdoc)
        {
            asMappingConnector.GetAllNewDllPaths(sdkId);

            var elements = asMappingConnector.GetAllNewDllPathsWithFullName(sdkId);

            string originalCurrentWorkingDirectory = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(new FileInfo(csprojFilePath).DirectoryName);

            // find the common path
            string commonPath = GetLongestCommonPrefix(new string[] { Path.GetFullPath(hintPathRoot), elements.ElementAt(0).Key });
            // From stackoverflow: "Then you may need to cut the prefix on the right hand"
            string[] commonPathSplit = commonPath.Split('\\');
            commonPathSplit.SetValue("", commonPathSplit.Length - 1);
            commonPath = String.Join("\\", commonPathSplit);

            // find how many folders up the common path is from the hintpath root
            // number of relative paths
            string[] hintSplit = Path.GetFullPath(csprojFilePath).Split('\\');
            int numberOfRelativePathsDifference = hintSplit.Length - commonPathSplit.Length;

            // create relative path
            string prependedRelativePath = "";
            for (int i = 0; i < numberOfRelativePathsDifference; i++)
            {
                prependedRelativePath += "..\\";
            }

            Directory.SetCurrentDirectory(originalCurrentWorkingDirectory);

            foreach (var element in elements)
            {
                // new hint path = number of relative paths + (new path - common path)
                string hintpathRightSide = element.Key.Substring(commonPath.Length);
                string hintpath = prependedRelativePath + hintpathRightSide;

                // create new reference element
                XElement addedref = new XElement(ns + xmlElementReferenceName,
                    new XAttribute("Include", element.Value),
                        new XElement(ns + "SpecificVersion", "False"),
                        new XElement(ns + xmlElementHintPathName, hintpath),
                        new XElement(ns + "Private", "False")
                    );
                // add the element to the xml
                xdoc.Descendants(ns + "ItemGroup").First().AddFirst(addedref);
            }
        }

        // Taken from stackoverflow "Find common parent-path in list of files and directories"
        // id = 24866683
        private static string GetLongestCommonPrefix(string[] s)
        {
            int k = s[0].Length;
            for (int i = 1; i < s.Length; i++)
            {
                k = Math.Min(k, s[i].Length);
                for (int j = 0; j < k; j++)
                    if (s[i][j] != s[0][j])
                    {
                        k = j;
                        break;
                    }
            }
            return s[0].Substring(0, k);
        }

        // remove <Reference> tags to the old sdk
        private static void RemoveOldDllReferences(HashSet<String> olddllSet, string csprojFilePath, string xmlElementHintPathName, string xmlElementOutputPathName, string xmlElementReferenceName, XNamespace ns, XDocument xdoc)
        {
            // need to anchor the working directory to the fodler of the .csproj file so that relative path names will point to the correct location
            string originalCurrentWorkingDirectory = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(new FileInfo(csprojFilePath).DirectoryName);

            var references = from reference in xdoc.Descendants(ns + xmlElementReferenceName)
                             where reference.Element(ns + xmlElementHintPathName) != null
                             // that checks if the reference is part of the old sdk
                             // if the reference is not part of the old sdk then don't include it in the selection output
                             // because if it is included in the output then it will get removed
                             where olddllSet.Contains(Path.GetFullPath(reference.Descendants(ns + xmlElementHintPathName).First().Value))
                             select reference;

            while (references.Count() > 0)
            {
                references.First().Remove();
            }

            // change the current working directory back
            Directory.SetCurrentDirectory(originalCurrentWorkingDirectory);
        }

        // Change output path to the new one
        private string ChangeOutputPath(string xmlElementOutputPathName, XNamespace ns, XDocument xdoc)
        {
            string newoutPutPath = SDKSQLConnector.GetInstance().GetOutputPathById(sdkId);
            string newEndPath = newoutPutPath.Split('\\').Last();

            var outputPathElements = from outp in xdoc.Descendants(ns + xmlElementOutputPathName)
                                     select outp;
            foreach (var e in outputPathElements)
            {
                var oldar = e.Value.Split('\\');
                oldar[oldar.Count() - 2] = newEndPath; // replace the old parent folder with the new one
                newoutPutPath = String.Join("\\", oldar);
                e.SetValue(newoutPutPath);
            }
            return newoutPutPath;
        }
    }
}