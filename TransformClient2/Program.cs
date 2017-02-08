using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace TransformClient2
{
    class Program
    {
        //public static void Main(string[] args)
        //{
        //    (new Program()).Run(args);
        //}

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
                ProcessDocument(doc);
            }
        }

        void ProcessDocument(Document doc)
        {
            var semanticModel = doc.GetSemanticModelAsync().Result;
            var syntaxTree = doc.GetSyntaxTreeAsync().Result;

            //do processing here

            File.WriteAllText(doc.FilePath, syntaxTree.GetText().ToString());
        }
    }
}
