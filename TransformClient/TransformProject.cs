using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Editing;
using NamespaceRefactorer;
using DBConnector;
using System.Xml.Linq;

namespace NamespaceRefactorer
{
    public class TransformProject
    {

        SDKMappingSQLConnector mappingConnector = SDKMappingSQLConnector.GetInstance();
        public static int sdkId;

        // args 0 = .csproj file path
        // args 1 = the current working directory folder that will correctly navigate to the output folder when relative path names are used in the output path
        // args 2 = sdk name
        public static void Main(string[] args)
        {
            sdkId = SDKSQLConnector.GetInstance().getByName(args[2]).id;
            (new TransformProject()).Run(args);
        }
                
        public void Run(string[] args)
        {
            var filePath = args[0];
            Helper.verifyFolderExists(args[1]);
            Directory.SetCurrentDirectory(args[1]);
            if (filePath.EndsWith(".soln", StringComparison.OrdinalIgnoreCase))
            {
                ProcessSolution(MSBuildWorkspace.Create().OpenSolutionAsync(filePath).Result);
            }
            else if (filePath.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase)
                || filePath.EndsWith(".vbproj", StringComparison.OrdinalIgnoreCase))
            {
                ProcessProject(MSBuildWorkspace.Create().OpenProjectAsync(filePath).Result, args[2]);
            }

        }

        void ProcessSolution(Solution soln)
        {
            foreach (Project proj in soln.Projects)
            {
                //ProcessProject(proj);
            }
        }

        void ProcessProject(Project proj, string sdkid)
        {
            HashSet<String> namespaceSet =  mappingConnector.GetAllNamespaces(sdkId);
            Dictionary<String, HashSet<String>> namespaceToClassnameSetMap = new Dictionary<string, HashSet<string>>();
            foreach (Document doc in proj.Documents)
            {
                if (isDocCSharp(doc))
                {
                    // ProcessDocumentCSharp(doc, namespaceSet, namespaceToClassnameSetMap); UNCOMMENT THIS
                }

                if (isDocVB(doc))
                {
                    ProcessDocumentVB(doc);
                }
            }
            HashSet<String> newdllSet = mappingConnector.GetAllNewDllPaths(sdkId);
            HashSet<String> olddllSet = mappingConnector.GetAllOldDllPaths(sdkId);
            // Don't remove the line below, cblupo
            transformXml(proj.FilePath, newdllSet, olddllSet, sdkId);
            Console.WriteLine("Project file edited to use new references");
        }

        private void ProcessDocumentVB(Document doc)
        {
            throw new NotImplementedException();
        }

        private bool isDocVB(Document doc)
        {
            return Path.GetExtension(doc.FilePath).Equals(".vb");
        }

        private bool isDocCSharp(Document doc)
        {
            return Path.GetExtension(doc.FilePath).Equals(".cs");
        }

        void ProcessDocumentCSharp(Document doc, HashSet<String> namespaceSet, Dictionary<String, HashSet<String>> namespaceToClassnameSetMap)
        {
            var semanticModel = doc.GetSemanticModelAsync().Result;
            var syntaxTree = doc.GetSyntaxTreeAsync().Result;

            // do processing here
            var documentEditor = DocumentEditor.CreateAsync(doc).Result; 

            TransformFile ft = new TransformFile(documentEditor);

            syntaxTree = ft.replaceSyntax();
            File.WriteAllText(doc.FilePath, syntaxTree.GetText().ToString());
            Console.WriteLine("Transformed   " + doc.FilePath);
        }

        public void transformXml(string csprojFilePath, HashSet<String> newdllSet, HashSet<String> olddllSet, int sdkid)
        {
            string xmlElementOutputPathName = "OutputPath";
            string xmlElementReferenceName = "Reference";
            string xmlElementHintPathName = "HintPath";

            csprojFilePath = @"C:\Users\Christopher Lupo\Desktop\2017SpringTeam25\tests\alias\clientC#\Client\Client - original.csproj"; // magic
            // find the namsespace by calling Descendents() on the Root and drill down into the properties to find the namsespace you need
            XNamespace ns = XNamespace.Get("http://schemas.microsoft.com/developer/msbuild/2003");
            XDocument xdoc = XDocument.Load(csprojFilePath);

            // transform the xml
            removeOldDllReferences(xmlElementOutputPathName, xmlElementReferenceName, xmlElementHintPathName, ns, xdoc, olddllSet);
            string newRelativeOutputPath = changeOutputPath(xmlElementOutputPathName, ns, xdoc);
            addNewDllReferences(xmlElementHintPathName, xmlElementReferenceName, ns, xdoc, newRelativeOutputPath);

            // save the xml
            xdoc.Save(@"C:\Users\Christopher Lupo\Desktop\2017SpringTeam25\tests\alias\clientC#\Client\Client - transformed.csproj"); // magic
        }

        private void addNewDllReferences(string xmlElementHintPathName, string xmlElementReferenceName, XNamespace ns, XDocument xdoc, string newRelativeOutputPath)
        {
            mappingConnector.GetAllNewDllPaths(sdkId);

            HashSet<string> newDlls = mappingConnector.GetAllNewDllPaths(sdkId);

            foreach (var dll in newDlls)
            {
                XElement addedref = new XElement(xmlElementReferenceName, new XAttribute("Include", "SDK, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL"),
                        new XElement("SpecificVersion", "False"),
                        new XElement(xmlElementHintPathName, newRelativeOutputPath + Path.GetFileName(dll)),
                        new XElement("Private", "False")
                    );
                xdoc.Descendants(ns + "ItemGroup").First().AddFirst(addedref);
            }
        }

        private static void removeOldDllReferences(string xmlElementOutputPathName, string xmlElementReferenceName, string xmlElementHintPathName, XNamespace ns, XDocument xdoc, HashSet<String> olddllSet)
        {
            string oldOutputPath = (from outp in xdoc.Descendants(ns + xmlElementOutputPathName)
                                    select outp).First().Value;

            var references = from reference in xdoc.Descendants(ns + xmlElementReferenceName)
                             where reference.Element(ns + xmlElementHintPathName) != null
                             // that checks if the reference is part of the old sdk
                             // if the reference is not part of the old sdk then don't include it in the selection output
                             // because if it is included in the output then it will get removed
                             where olddllSet.Contains(Path.GetFullPath(oldOutputPath + Path.GetFileName(reference.Descendants(ns + xmlElementHintPathName).First().Value)))                          
                             select reference;
            try
            {
                foreach (var r in references)
                {
                    r.Remove();
                }
            }
            catch (NullReferenceException nre)
            {
                // null exception is thrown because the reference is remove from the list, so just ignore
            }
        }

        private string changeOutputPath(string xmlElementOutputPathName, XNamespace ns, XDocument xdoc)
        {
            string newoutPutPath = SDKSQLConnector.GetInstance().getOutputPathById(sdkId);
            string newEndPath = newoutPutPath.Split('\\').Last();

            var outputPathElements = from outp in xdoc.Descendants(ns + xmlElementOutputPathName)
                                     select outp;
            foreach (var e in outputPathElements)
            {
                string oldpath = e.Value;
                var oldar = oldpath.Split('\\');
                oldar[oldar.Count() - 2] = newEndPath; // replace the old parent folder with the new one
                newoutPutPath = String.Join("\\", oldar);
                e.SetValue(newoutPutPath);
            }
            return newoutPutPath;
        }
    }
}
