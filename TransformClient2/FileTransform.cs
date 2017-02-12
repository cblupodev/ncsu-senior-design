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
using NamespaceRefactorer;
using DBConnector;

namespace NamespaceRefactorer
{
    public class FileTransform
    {

        private CompilationUnitSyntax root;
        private SemanticModel semanticModel;
        private SyntaxTree tree;
        private string clientFilePath;

        public FileTransform(SyntaxTree tree, SemanticModel semanticModel)
        {
            this.tree = tree;
            this.root = (CompilationUnitSyntax)tree.GetRoot();
            this.semanticModel = semanticModel;
        }

        public FileTransform(string fileName)
        {
            Helper.verifyFileExists(fileName);
            SyntaxTree tree;
            this.clientFilePath = fileName;

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

        // search for using statements with the old sdk. Then if they are found then look for classes that coresponded to the custom attributes
        // if classes are found then replace the old using statement with the new one
        // oldSDKUsings is a list of the old sdk using statements
        public SyntaxTree findOldUsingsAndReplaceOldSyntax(HashSet<string> namespaceSet)
        {
            List<UsingDirectiveSyntax> oldUsings = findMatchingUsings(namespaceSet);
            if (oldUsings.Any()) // continue if there are usings in the database
            {
                foreach (var usingDirective in oldUsings)
                {
                    replaceObjectCreations(usingDirective);
                    replaceCastings(usingDirective);
                    replaceUsingStatements(usingDirective);
                    replaceFullyQualifiedNames(UsingDirective);
                    replaceAliasing(UsingDirective);
                    replaceClassExtensions(UsingDirective);
                    // etc.
                }
            }

            return tree;
        }

        internal bool hasNameSpaceInDatabase(HashSet<string> namespaceSet)
        {
            foreach (var usingDirective in root.Usings) // iterate over each using statement
            {
                var name = semanticModel.GetSymbolInfo(usingDirective.Name); // https://github.com/dotnet/roslyn/wiki/Getting-Started-C%23-Semantic-Analysis
                //if (name.Symbol == null) // I don't know why I have to do this. I don't know why our namespace is diffrent than the System ones, magic
                //{
                    // get the text for the using
                    IdentifierNameSyntax ins = (IdentifierNameSyntax)usingDirective.Name;
                    var valueText = ins.Identifier.ValueText;

                    return namespaceSet.Contains(valueText);
                //}
            }

            return false;
        }

        private void replaceClassExtensions(Func<NameEqualsSyntax, NameSyntax, UsingDirectiveSyntax> usingDirective)
        {
            throw new NotImplementedException();
        }

        private void replaceAliasing(Func<NameEqualsSyntax, NameSyntax, UsingDirectiveSyntax> usingDirective)
        {
            throw new NotImplementedException();
        }

        private void replaceFullyQualifiedNames(Func<NameEqualsSyntax, NameSyntax, UsingDirectiveSyntax> usingDirective)
        {
            throw new NotImplementedException();
        }

        private void replaceUsingStatements(UsingDirectiveSyntax usingDirective)
        {
            throw new NotImplementedException();
        }

        private void replaceCastings(UsingDirectiveSyntax usingDirective)
        {
            throw new NotImplementedException();
        }

        private void replaceObjectCreations(UsingDirectiveSyntax usingDirective)
        {

            // https://duckduckgo.com/?q=nested+selection+linq&ia=qa
            IEnumerable<ObjectCreationExpressionSyntax> objectCreations = root.DescendantNodes().OfType<ObjectCreationExpressionSyntax>();
            foreach (ObjectCreationExpressionSyntax item in objectCreations) // iterate over all object creations in the file
            {
                var semanticObjCreation = semanticModel.GetSymbolInfo(item.Type);
                // semanticObcCreation
                // if find a class that was tagged then replace the old using with the new one
                var descendentTokens = item.DescendantTokens().OfType<SyntaxToken>(); // TODO use the semantic model instead of this way
                if (descendentTokens.ElementAt(1).Value.Equals("Sample")) // [1] gets the identifier syntax, magic
                {
                    replaceOldUsingWithNew(usingDirective);
                }
            }
        }

         //return a list of using directives that exist in the database
        private List<UsingDirectiveSyntax> findMatchingUsings(HashSet<string> namespaceSet)
        {
           List<UsingDirectiveSyntax> rtn = new List<UsingDirectiveSyntax>();
            foreach (var usingDirective in root.Usings) // iterate over each using statement
            {
                var name = semanticModel.GetSymbolInfo(usingDirective.Name); // https://github.com/dotnet/roslyn/wiki/Getting-Started-C%23-Semantic-Analysis
                if (name.Symbol == null) // I don't know why I have to do this. I don't know why our namespace is diffrent than the System ones, magic
                {
                    // get the text for the using
                    IdentifierNameSyntax ins = (IdentifierNameSyntax)usingDirective.Name;
                    var valueText = ins.Identifier.ValueText;

                    if (namespaceSet.Contains(valueText))
                    {
                        rtn.Add(usingDirective);
                    }
                }
            }
            return rtn;
        }

    private void replaceOldUsingWithNew(UsingDirectiveSyntax usingDirective)
        {
            NameSyntax mockName2 = IdentifierName("FujitsuSDKNew"); // Magic

            var oldUsing = usingDirective;
            var newUsing = oldUsing.WithName(mockName2);
            root = root.ReplaceNode(oldUsing, newUsing);
        }
    }
}