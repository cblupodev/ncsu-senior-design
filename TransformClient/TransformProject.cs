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
        // args 1 = sdk name
        public static void Main(string[] args)
        {
            sdkId = SDKSQLConnector.GetInstance().getByName(args[1]).id;
            (new TransformProject()).Run(args);
        }
                
        public void Run(string[] args)
        {
            Helper.verifyFileExists(args[0]);
            var filePath = args[0];
            if (filePath.EndsWith(".soln", StringComparison.OrdinalIgnoreCase))
            {
                ProcessSolution(MSBuildWorkspace.Create().OpenSolutionAsync(filePath).Result);
            }
            else if (filePath.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase)
                || filePath.EndsWith(".vbproj", StringComparison.OrdinalIgnoreCase))
            {
                var aaaa = MSBuildWorkspace.Create().OpenProjectAsync(filePath).Result;
                ProcessProject(MSBuildWorkspace.Create().OpenProjectAsync(filePath).Result, args[1]);
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
                    // UNCOMMENT
                    // ProcessDocumentCSharp(doc, namespaceSet, namespaceToClassnameSetMap);
                }

                if (isDocVB(doc))
                {
                    ProcessDocumentVB(doc);
                }
            }
            HashSet<String> newdllSet = mappingConnector.GetAllNewDllPaths(sdkId);
            HashSet<String> olddllSet = mappingConnector.GetAllOldDllPaths(sdkId);
            // Don't remove the line below, cblupo
            transformXml(proj.FilePath, newdllSet, olddllSet, Path.GetExtension(proj.FilePath), sdkId);
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

        public void transformXml(string csprojFilePath, HashSet<String> newdllSet, HashSet<String> olddllSet, string projectFileExtension, int sdkid)
        {
            string xmlElementOutputPathName = "OutputPath";
            string xmlElementReferenceName = "Reference";
            string xmlElementHintPathName = "HintPath";

            // find the namsespace by calling Descendents() on the Root and drill down into the properties to find the namsespace you need
            XNamespace ns = XNamespace.Get("http://schemas.microsoft.com/developer/msbuild/2003");
            XDocument xdoc = XDocument.Load(csprojFilePath);

            // transform the xml
            removeOldDllReferences(xmlElementOutputPathName, xmlElementReferenceName, xmlElementHintPathName, ns, xdoc, olddllSet, csprojFilePath);
            // this needs to be done in this order
            string newRelativeOutputPath = changeOutputPath(xmlElementOutputPathName, ns, xdoc);
            addNewDllReferences(xmlElementHintPathName, xmlElementReferenceName, ns, xdoc, newRelativeOutputPath);

            // save the xml
            xdoc.Save((new FileInfo(csprojFilePath).DirectoryName+"\\transformed_proj_file"+projectFileExtension)); // magic
            // xdoc.Save(csprojFilePath); // UNCOMMENT this
        }

        private void addNewDllReferences(string xmlElementHintPathName, string xmlElementReferenceName, XNamespace ns, XDocument xdoc, string newRelativeOutputPath)
        {
            mappingConnector.GetAllNewDllPaths(sdkId);

            HashSet<string> newDlls = mappingConnector.GetAllNewDllPaths(sdkId);

            foreach (var dll in newDlls)
            {
                XElement addedref = new XElement(ns + xmlElementReferenceName, 
                    new XAttribute("Include", "SDK"), // magic
                        new XElement(ns + "SpecificVersion", "False"),
                        new XElement(ns + xmlElementHintPathName, newRelativeOutputPath + Path.GetFileName(dll)),
                        new XElement(ns + "Private", "False")
                    );
                xdoc.Descendants(ns + "ItemGroup").First().AddFirst(addedref);
            }
        }

        private static void removeOldDllReferences(string xmlElementOutputPathName, string xmlElementReferenceName, string xmlElementHintPathName, XNamespace ns, XDocument xdoc, HashSet<String> olddllSet, string csprojFilePath)
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
            try
            {
                foreach (var reference in references)
                {
                    reference.Remove();
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
                var oldar = e.Value.Split('\\');
                oldar[oldar.Count() - 2] = newEndPath; // replace the old parent folder with the new one
                newoutPutPath = String.Join("\\", oldar);
                e.SetValue(newoutPutPath);
            }
            return newoutPutPath;
        }
    }
}
