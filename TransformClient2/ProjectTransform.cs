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
    public class ProjectTransform
    {

        SDKMappingSQLConnector mappingConnector = SDKMappingSQLConnector.GetInstance();
        public static int sdkId;

        public static void Main(string[] args)
        {
            sdkId = SDKSQLConnector.GetInstance().getByName(args[1]).id;
            (new ProjectTransform()).Run(args);
        }

        public void Run(string[] args)
        {
            var filePath = args[0];
            if (filePath.EndsWith(".soln", StringComparison.OrdinalIgnoreCase))
            {
                ProcessSolution(MSBuildWorkspace.Create().OpenSolutionAsync(filePath).Result);
            }
            else if (filePath.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase)
                || filePath.EndsWith(".vbproj", StringComparison.OrdinalIgnoreCase))
            {
                ProcessProject(MSBuildWorkspace.Create().OpenProjectAsync(filePath).Result);
            }

        }

        void ProcessSolution(Solution soln)
        {
            foreach (Project proj in soln.Projects)
            {
                ProcessProject(proj);
            }
        }

        void ProcessProject(Project proj)
        {
            HashSet<String> namespaceSet =  mappingConnector.GetAllNamespaces(sdkId);
            //Dictionary<String, HashSet<String>> namespaceToClassnameSetMap = mappingConnector.GetNamespaceToClassnameSetMap(sdkId);
            Dictionary<String, HashSet<String>> namespaceToClassnameSetMap = new Dictionary<string, HashSet<string>>();
            foreach (Document doc in proj.Documents)
            {
                if (isDocCSharp(doc))
                {
                    ProcessDocumentCSharp(doc, namespaceSet, namespaceToClassnameSetMap);
                }

                if (isDocVB(doc))
                {
                    ProcessDocumentVB(doc);
                }
            }
            HashSet<String> newdllSet = mappingConnector.GetAllNewDllPaths(sdkId);
            HashSet<String> olddllSet = mappingConnector.GetAllOldDllPaths(sdkId);
            // Don't remove the line below, cblupo
            //transformXml(proj.FilePath, newdllSet, olddllSet);
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

            //do processing here
            var documentEditor = DocumentEditor.CreateAsync(doc).Result; //https://joshvarty.wordpress.com/2015/08/18/learn-roslyn-now-part-12-the-documenteditor/

            FileTransform ft = new FileTransform(documentEditor);

            syntaxTree = ft.findOldUsingsAndReplaceOldSyntax(documentEditor, namespaceSet, namespaceToClassnameSetMap);
            File.WriteAllText(doc.FilePath, syntaxTree.GetText().ToString()); // http://stackoverflow.com/questions/18295837/c-sharp-roslyn-api-reading-a-cs-file-updating-a-class-writing-back-to-cs-fi
            Console.WriteLine("Transformed   " + doc.FilePath);
        }

        public void transformXml(string filePath, HashSet<String> newdllSet, HashSet<String> olddllSet)
        {
            string xmlElementOutputPathName = "OutputPath";
            string xmlElementReferenceName = "Reference";
            string xmlElementHintPathName = "HintPath";

            filePath = @"C:\Users\Christopher Lupo\Documents\Visual Studio 2015\Projects\2017SpringTeam25\TransformClient2\Client - Copy.xml"; // magic
            // find the namsespace by calling Descendents() on the Root and drill down into the properties to find the namsespace you need
            XNamespace ns = XNamespace.Get("http://schemas.microsoft.com/developer/msbuild/2003"); // https://granadacoder.wordpress.com/2012/10/11/how-to-find-references-in-a-c-project-file-csproj-using-linq-xml/
            XDocument xdoc = XDocument.Load(filePath);

            string outputPath = (from outp in xdoc.Descendants(ns + xmlElementOutputPathName)
                                 select outp).First().Value;

            var references = from reference in xdoc.Descendants(ns + xmlElementReferenceName)
                             where reference.Element(ns + xmlElementHintPathName) != null
                             //where olddllSet.Contains(Path.GetFullPath((outputPath+Path.GetFileName(reference.Descendants(ns + xmlElementHintPathName).First().Value)))) == true
                             // Don't remove the line above
                             // that checks if the reference is part of the old sdk
                             // if the reference is not part of the old sdk then don't include it in the selection output
                             // because if it is included in the output then it will get removed
                             select reference;

            try
            {
                foreach (var r in references)
                {
                    // TOOD find the refernces that are part of the old sdk. Get a old_dll_files list from the database and compare
                    // also maybe do this work in the linq statement (cleaner)
                    // Don't remove the line below
                    //r.Remove();
                }
            }
            catch (NullReferenceException nre)
            {
                // null exception is thrown because the reference is remove from the list, so just ignore
            }


            string[] newDlls = { "C:\\newsdk.dll", "C:\\newsdk2.dll", "C:\\newsdk3.dll" }; // magic

            foreach (var dll in newDlls)
            {
                // https://www.youtube.com/playlist?list=PL6n9fhu94yhX-U0Ruy_4eIG8umikVmBrk
                XElement addedref = new XElement("Reference", new XAttribute("Include", "SDK, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL"),
                        new XElement("SpecificVersion", "False"),
                        new XElement("HintPath", outputPath + Path.GetFileName(dll)),
                        new XElement("Private", "False")
                    );
                xdoc.Descendants(ns + "ItemGroup").First().AddFirst(addedref);
            }

            xdoc.Save(@"..\\..\\Client.xml");
        }
    }
}
