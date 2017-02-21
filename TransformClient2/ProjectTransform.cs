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
            XMLTransform xmlTransform = new XMLTransform();
            // Don't remove the line below, cblupo
            //xmlTransform.transformXml(proj.FilePath, newdllSet, olddllSet);
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
    }
}
