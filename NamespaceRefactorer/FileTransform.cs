using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;


namespace NamespaceRefactorer
{
    class FileTransform
    {

        private CompilationUnitSyntax root;
        private SemanticModel semanticModel;
        private string clientFilePath;
        private IEnumerable<string> oldSDKUsings;

        // param 1 = the path to the old sdk dll with the custom attributes
        // param 2 = the path to the old client code that you want to transform
        // http://stackoverflow.com/questions/4791140/how-do-i-start-a-program-with-arguments-when-debugging
        static void Main(string[] args)
        {
            FileTransform fileTransform = new FileTransform();
            fileTransform.run(args);
        }

        private void run(string[] args)
        {
            findCustomerAttributes(args[0]);
            loadVariables(args[1]);
            findOldUsingsAndReplacIfCertainClassesFound();
        }

        // search for using statements with the old sdk. Then if they are found then look for classes that coresponded to the custom attributes
        // if classes are found then replace the old using statement with the new one
        private void findOldUsingsAndReplacIfCertainClassesFound()
        {
            foreach (var usingDirective in root.Usings)
            {
                var name = semanticModel.GetSymbolInfo(usingDirective.Name);
                if (name.Symbol == null) // I don't know why I have to do this. I don't know why our namespace is diffrent than the System ones, magic
                {
                    IdentifierNameSyntax ins = (IdentifierNameSyntax)usingDirective.Name;
                    var valueText = ins.Identifier.ValueText;
                    foreach (var oldUse in oldSDKUsings)
                    {
                        if (valueText.Equals(oldUse))
                        {
                            IEnumerable<ObjectCreationExpressionSyntax> objectCreations = root.DescendantNodes().OfType<ObjectCreationExpressionSyntax>();
                            foreach (var item in objectCreations)
                            {

                                var descendentTokens = item.DescendantTokens().OfType<SyntaxToken>(); // TODO use the semantic model instead of this way
                                if (descendentTokens.ElementAt(1).Value.Equals("Sample")) // [1] gets the identifier syntax, magic
                                {
                                    replaceOldUsingWithNew(usingDirective);
                                }
                            }
                        } 
                    }
                }
                try
                {
                    var systemSymbol = (INamespaceSymbol)name.Symbol;
                    Debug.WriteLine(systemSymbol.Name);
                }
                catch (NullReferenceException)
                {
                }
            }
        }

        private void replaceOldUsingWithNew(UsingDirectiveSyntax usingDirective)
        {
            NameSyntax name2 = IdentifierName("FujitsuSDKNew"); // Magic

            var oldUsing = usingDirective;
            var newUsing = oldUsing.WithName(name2);
            root = root.ReplaceNode(oldUsing, newUsing);

            File.WriteAllText(clientFilePath, root.ToFullString()); // http://stackoverflow.com/questions/18295837/c-sharp-roslyn-api-reading-a-cs-file-updating-a-class-writing-back-to-cs-fi
        }

        // initialize the important roslyn variables
        private void loadVariables(string clientFilePath)
        {
            verifyFileExists(clientFilePath);
            SyntaxTree tree;

            using (var stream = File.OpenRead(clientFilePath))
            {
                tree = CSharpSyntaxTree.ParseText(SourceText.From(stream), path: clientFilePath);
            }

            root = (CompilationUnitSyntax)tree.GetRoot();

            // https://github.com/dotnet/roslyn/wiki/Getting-Started-C%23-Semantic-Analysis
            var compilation = CSharpCompilation.Create("client_old").AddReferences(
                                                    MetadataReference.CreateFromFile(
                                                        typeof(object).Assembly.Location))
                                                    .AddSyntaxTrees(tree);

            semanticModel = compilation.GetSemanticModel(tree);
        }

        // return array of custom atribute strings that exist in the file
        private string[] findCustomerAttributes(string dllPath)
        {
            verifyFileExists(dllPath);
            var assem = Assembly.LoadFile(@"C:\Users\Christopher Lupo\Documents\Visual Studio 2015\Projects\2017SpringTeam25\FujitsuSDKOld\bin\Debug\FujitsuSDKOld.dll"); // the .dll file

            var types = assem.GetTypes(); // the types will tell you if there are custom data attributes           

            // TODO return array of custom atributes that exist in the file
            return null;
        }

        private void verifyFileExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("file doesn't exist: {0}", filePath);
            }
        }
    }
}