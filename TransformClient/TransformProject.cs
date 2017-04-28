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
                CopyDirectory(projectParentFolder, transformed_folder);

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
                TransformXml(proj.FilePath, olddllSet, Path.GetExtension(proj.FilePath), sdkId, projectParentFolder, transformed_folder);

                Console.WriteLine("Project file edited to use new references");
            } else
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

        private static void CopyDirectory(string sourcePath, string destPath)
        {
            // automatically remove a directory of the same name so don't have to manually remove it every time you run TransformClient
            RecursiveDeleteDirectory(new DirectoryInfo(destPath));

            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }

            foreach (string file in Directory.GetFiles(sourcePath))
            {
                string dest = Path.Combine(destPath, Path.GetFileName(file));
                File.Copy(file, dest);
            }

            foreach (string folder in Directory.GetDirectories(sourcePath))
            {
                string dest = Path.Combine(destPath, Path.GetFileName(folder));
                CopyDirectory(folder, dest);
            }
        }

        private void TransformXml(string csprojFilePath, HashSet<String> olddllSet, string projectFileExtension, int sdkid, string currentParent, string newParent)
        {
            string xmlElementOutputPathName = "OutputPath";
            string xmlElementReferenceName = "Reference";
            string xmlElementHintPathName = "HintPath";

            // find the namsespace by calling Descendents() on the Root and drill down into the properties to find the namsespace you need
            XNamespace ns = XNamespace.Get("http://schemas.microsoft.com/developer/msbuild/2003");
            XDocument xdoc = XDocument.Load(csprojFilePath);

            // transform the xml
            RemoveOldDllReferences(xmlElementOutputPathName, xmlElementReferenceName, xmlElementHintPathName, ns, xdoc, olddllSet, csprojFilePath);
            // this needs to be done in this order
            string newRelativeOutputPath = ChangeOutputPath(xmlElementOutputPathName, ns, xdoc);
            AddNewDllReferences(xmlElementHintPathName, xmlElementReferenceName, ns, xdoc, newRelativeOutputPath);

            // save the xml
            xdoc.Save(csprojFilePath.Replace(currentParent, newParent));
        }

        private void AddNewDllReferences(string xmlElementHintPathName, string xmlElementReferenceName, XNamespace ns, XDocument xdoc, string newRelativeOutputPath)
        {
            asMappingConnector.GetAllNewDllPaths(sdkId);

            var elements = asMappingConnector.GetAllNewDllPathsWithFullName(sdkId);

            foreach (var element in elements)
            {
                // create new reference element
                XElement addedref = new XElement(ns + xmlElementReferenceName, 
                    new XAttribute("Include", element.Value),
                        new XElement(ns + "SpecificVersion", "False"),
                        new XElement(ns + xmlElementHintPathName, newRelativeOutputPath + Path.GetFileName(element.Key)),
                        new XElement(ns + "Private", "False")
                    );
                // add the element to the xml
                xdoc.Descendants(ns + "ItemGroup").First().AddFirst(addedref);
            }
        }

        private static void RemoveOldDllReferences(string xmlElementOutputPathName, string xmlElementReferenceName, string xmlElementHintPathName, XNamespace ns, XDocument xdoc, HashSet<String> olddllSet, string csprojFilePath)
        {
            string oldOutputPath = (from outp in xdoc.Descendants(ns + xmlElementOutputPathName)
                                    select outp).First().Value;
            // need to anchor the output path to the folder the project file is in otherwise ../.. relative paths would navigate based on where this executable is
            // if the path has root in it then don't need to worry about getting the full path
            if (Path.IsPathRooted(oldOutputPath) == false)
            {
                // this handles this kind of case: C:/users/user1/SDK1/aaa/bbb/ccc/../../bin1. Where relative paths are in the middle of the path
                oldOutputPath = Path.GetFullPath(Path.Combine(new FileInfo(csprojFilePath).DirectoryName, oldOutputPath));
            }

            var references = from reference in xdoc.Descendants(ns + xmlElementReferenceName)
                             where reference.Element(ns + xmlElementHintPathName) != null
                             // that checks if the reference is part of the old sdk
                             // if the reference is not part of the old sdk then don't include it in the selection output
                             // because if it is included in the output then it will get removed
                             where olddllSet.Contains(Path.GetFullPath(oldOutputPath + Path.GetFileName(reference.Descendants(ns + xmlElementHintPathName).First().Value)))
                             select reference;
            while ( references.Count() > 0 )
            {
                references.First().Remove();
            }
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