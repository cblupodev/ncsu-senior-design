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
    class Program
    {
        static void Main(string[] args)
        {
            var debugpath = @"C:\Users\Christopher Lupo\documents\visual studio 2015\Projects\2017SpringTeam25\debug.txt";
            var assem = Assembly.LoadFile(@"C:\Users\Christopher Lupo\Documents\Visual Studio 2015\Projects\2017SpringTeam25\FujitsuSDKOld\bin\Debug\FujitsuSDKOld.dll"); // the .dll file

            var asdf = assem.GetTypes(); // the types will tell you if there are custom data attributes           

            var path = @"C:\Users\Christopher Lupo\Documents\Visual Studio 2015\Projects\2017SpringTeam25\Customer\client_old.cs";
            SyntaxTree tree;
        
            using (var stream = File.OpenRead(path))
            {
                tree = CSharpSyntaxTree.ParseText(SourceText.From(stream), path: path);
            }

            var root = (CompilationUnitSyntax)tree.GetRoot();

            // https://github.com/dotnet/roslyn/wiki/Getting-Started-C%23-Semantic-Analysis
            var compilation = CSharpCompilation.Create("client_old").AddReferences(
                                                    MetadataReference.CreateFromFile(
                                                        typeof(object).Assembly.Location))
                                                    .AddSyntaxTrees(tree);

            var semanticModel = compilation.GetSemanticModel(tree);

            IEnumerable<ObjectCreationExpressionSyntax> objectCreations = root.DescendantNodes().OfType<ObjectCreationExpressionSyntax>();
            foreach (var item in objectCreations)
            {
                
                var descendentTokens = item.DescendantTokens().OfType<SyntaxToken>(); // TODO use the semantic model instead of this way
                if (descendentTokens.ElementAt(1).Value.Equals("Sample")) // [1] gets the identifier syntax
                {
                    // Get usings
                    foreach (var usingDirective in root.Usings)
                    {
                        var name = semanticModel.GetSymbolInfo(usingDirective.Name);
                        if (name.Symbol == null) // I don't know why I have to do this. I don't know why our namespace is diffrent than the System ones
                        {
                            IdentifierNameSyntax ins = (IdentifierNameSyntax)usingDirective.Name;
                            var valueText = ins.Identifier.ValueText;
                            if (valueText.Equals("FujitsuSDKOld"))
                            {
                                // TODO replace the using
                                NameSyntax name2 = IdentifierName("FujitsuSDKNew");

                                var oldUsing = usingDirective;
                                var newUsing = oldUsing.WithName(name2);
                                root = root.ReplaceNode(oldUsing, newUsing);

                                File.WriteAllText(path, root.ToFullString()); // http://stackoverflow.com/questions/18295837/c-sharp-roslyn-api-reading-a-cs-file-updating-a-class-writing-back-to-cs-fi
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
            }

        }
    }
}