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
using Microsoft.CodeAnalysis.Editing;

namespace NamespaceRefactorer
{
    public class FileTransform
    {

        private CompilationUnitSyntax root;
        private SemanticModel semanticModel;
        private SyntaxTree tree;
        private DocumentEditor documentEditor;
        private string clientFilePath;

        public FileTransform(DocumentEditor documentEditor)
        {
            this.documentEditor = documentEditor;
            this.root = (CompilationUnitSyntax)documentEditor.OriginalRoot;
            this.tree = root.SyntaxTree;
            this.semanticModel = documentEditor.SemanticModel;
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
            var compilation = CSharpCompilation.Create("client_old").AddReferences( //magic
                                                    MetadataReference.CreateFromFile(
                                                        typeof(object).Assembly.Location))
                                                    .AddSyntaxTrees(tree);
            semanticModel = compilation.GetSemanticModel(tree);
        }

        // search for using statements with the old sdk. Then if they are found then look for classes that coresponded to the custom attributes
        // if classes are found then replace the old using statement with the new one
        // oldSDKUsings is a list of the old sdk using statements
        public SyntaxTree findOldUsingsAndReplaceOldSyntax(DocumentEditor documentEditor, HashSet<string> namespaceSet, Dictionary<String, HashSet<String>> namespaceToClassnameSetMap)
        {
            List<UsingDirectiveSyntax> oldUsings = findMatchingUsings(namespaceSet);
            List<ObjectCreationExpressionSyntax> oldObjectCreations = findMatchingClassnames(namespaceToClassnameSetMap);
            //if (oldUsings.Any()) // continue if there are usings in the database
            //{
            //    replaceUsingStatements(oldUsings);
            //}
            replaceObjectCreations();
            //replaceCastings(usingDirective);
            //replaceFullyQualifiedNames(UsingDirective);
            //replaceAliasing(UsingDirective);
            //replaceClassExtensions(UsingDirective);
            // etc.

            return documentEditor.GetChangedRoot().SyntaxTree;
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

        private void replaceUsingStatements(List<UsingDirectiveSyntax> oldUsings)
        {
            foreach (var usingDirective in oldUsings)
            {
                Dictionary<String, String> oldToNewMap = SDKMappingSQLConnector.GetInstance().GetOldToNewNamespaceMap(ProjectTransform.sdkId);
                var oldUsingName = usingDirective.Name.GetText().ToString();
                var newUsingName = oldToNewMap[oldUsingName];
                NameSyntax mockName2 = IdentifierName(newUsingName);

                var oldUsing = usingDirective;
                var newUsing = oldUsing.WithName(mockName2);
                documentEditor.ReplaceNode(oldUsing, newUsing);
            }
        }

        private void replaceCastings(UsingDirectiveSyntax usingDirective)
        {
            throw new NotImplementedException();
        }

        private void replaceObjectCreations()
        {

            // https://duckduckgo.com/?q=nested+selection+linq&ia=qa
            IEnumerable<ObjectCreationExpressionSyntax> objectCreations = tree.GetRoot().DescendantNodes().OfType<ObjectCreationExpressionSyntax>();
            foreach (ObjectCreationExpressionSyntax item in objectCreations) // iterate over all object creations in the file
            {
                var semanticObjCreation = semanticModel.GetSymbolInfo(item.Type);

                // semanticObcCreation
                // if find a class that was tagged then replace the old using with the new one
                //var descendentTokens = item.DescendantTokens().OfType<SyntaxToken>(); // TODO use the semantic model instead of this way
                //if (descendentTokens.ElementAt(1).Value.Equals("Sample")) // [1] gets the identifier syntax, magic
                //{
                //    replaceOldUsingWithNew(usingDirective);
                //}
            }
        }

        //return a list of using directives that exist in the database
        private List<UsingDirectiveSyntax> findMatchingUsings(HashSet<string> namespaceSet)
        {
            List<UsingDirectiveSyntax> rtn = new List<UsingDirectiveSyntax>();
            foreach (var usingDirective in root.Usings) // iterate over each using statement
            {
                var valueText = usingDirective.Name.GetText().ToString();
                if (namespaceSet.Contains(valueText))
                {
                    rtn.Add(usingDirective);
                }
            }
            return rtn;
        }

        //return a list of using classnames that exist in the database
        private List<ObjectCreationExpressionSyntax> findMatchingClassnames(Dictionary<String, HashSet<String>> namespaceToClassnameSetMap)
        {
            List<ObjectCreationExpressionSyntax> rtn = new List<ObjectCreationExpressionSyntax>();
            // https://duckduckgo.com/?q=nested+selection+linq&ia=qa
            IEnumerable<ObjectCreationExpressionSyntax> objectCreations = tree.GetRoot().DescendantNodes().OfType<ObjectCreationExpressionSyntax>();
            foreach (ObjectCreationExpressionSyntax item in objectCreations) // iterate over all object creations in the file
            {
                var semanticObjCreation = semanticModel.GetSymbolInfo(item.Type);
                var oldNamespace = semanticObjCreation.Symbol.ContainingNamespace.ToString();
                if (namespaceToClassnameSetMap.ContainsKey(oldNamespace))
                {
                    if (namespaceToClassnameSetMap[oldNamespace].Contains(semanticObjCreation.Symbol.Name))
                    {
                        rtn.Add(item);
                    }
                }
            }
            return rtn;
        }
    }
}