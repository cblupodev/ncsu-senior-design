using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using NamespaceRefactorer;
using DBConnector;

namespace TransformClient2
{
    class ProjectTransform
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
            foreach (Document doc in proj.Documents)
            {
                if (isDocCSharp(doc))
                {
                    ProcessDocumentCSharp(doc); 
                }

                if (isDocVB(doc))
                {
                    ProcessDocumentVB(doc);
                }
            }
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

        void ProcessDocumentCSharp(Document doc)
        {
            var semanticModel = doc.GetSemanticModelAsync().Result;
            var syntaxTree = doc.GetSyntaxTreeAsync().Result;

            //do processing here

            FileTransform ft = new FileTransform(syntaxTree, semanticModel);

            // pass in the set not the connector, only query the database once
            if (ft.hasNameSpaceInDatabase(mappingConnector.GetAllNamespaces(sdkId)))
            {
                syntaxTree = ft.findOldUsingsAndReplaceOldSyntax(mappingConnector.GetAllNamespaces(sdkId));
                File.WriteAllText(doc.FilePath, syntaxTree.GetText().ToString()); // http://stackoverflow.com/questions/18295837/c-sharp-roslyn-api-reading-a-cs-file-updating-a-class-writing-back-to-cs-fi
            }        
        }
    }
}
