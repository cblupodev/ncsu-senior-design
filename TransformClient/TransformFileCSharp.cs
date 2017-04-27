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
using EFSQLConnector;
using Microsoft.CodeAnalysis.Editing;
using CreateMappings;

namespace TransformClient
{
    public class TransformFileCSharp
    {

        private CompilationUnitSyntax root;
        private SemanticModel semanticModel;
        private SyntaxTree tree;
        private DocumentEditor documentEditor;
        private string clientFilePath;

        public TransformFileCSharp(DocumentEditor documentEditor)
        {
            this.documentEditor = documentEditor;
            this.root = (CompilationUnitSyntax)documentEditor.OriginalRoot;
            this.tree = root.SyntaxTree;
            this.semanticModel = documentEditor.SemanticModel;
        }

        public TransformFileCSharp(string fileName)
        {
            FileHelper.verifyFileExists(fileName);
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
        public SyntaxTree ReplaceSyntax()
        {
            ReplaceUsingStatements();
            ReplaceQualifiedNames();
            ReplaceIdentifierNames();
            return documentEditor.GetChangedRoot().SyntaxTree;
        }

        private void ReplaceUsingStatements()
        {
            HashSet<string> alreadyAddedUsingStatements = new HashSet<string>();
            IEnumerable<UsingDirectiveSyntax> usingDirectiveNodes = tree.GetRoot().DescendantNodes().OfType<UsingDirectiveSyntax>();
            foreach (UsingDirectiveSyntax oldUsingDirectiveNode in usingDirectiveNodes) // iterate over all qualified names in the file
            {
                var oldNamespace = oldUsingDirectiveNode.Name.GetText().ToString();
                List<namespace_map> namespaces = NSMappingSQLConnector.GetInstance().GetNamespaceMapsFromOldNamespace(TransformProject.sdkId, oldNamespace);
                if (namespaces != null)
                {
                    foreach (namespace_map nsMap in namespaces)
                    {
                        var newNamespace = nsMap.new_namespace;
                        if (!alreadyAddedUsingStatements.Contains(newNamespace))
                        {
                            alreadyAddedUsingStatements.Add(newNamespace);
                            NameSyntax newIdentifierNode = IdentifierName(newNamespace);
                            var newUsingDirectiveNode = oldUsingDirectiveNode.WithName(newIdentifierNode);
                            documentEditor.InsertAfter(oldUsingDirectiveNode, newUsingDirectiveNode);
                        }
                    }
                    documentEditor.RemoveNode(oldUsingDirectiveNode);
                }
            }
        }

        private void ReplaceQualifiedNames()
        {
            IEnumerable<QualifiedNameSyntax> qualifiedNames = tree.GetRoot().DescendantNodes().OfType<QualifiedNameSyntax>();
            foreach (QualifiedNameSyntax oldQualifiedNameNode in qualifiedNames) // iterate over all qualified names in the file
            {
                if (!(oldQualifiedNameNode.Parent is QualifiedNameSyntax))
                {
                    var qualifiedSymbolInfo = semanticModel.GetSymbolInfo(oldQualifiedNameNode);
                    string nsString = oldQualifiedNameNode.Left.WithoutTrivia().GetText().ToString();
                    if (qualifiedSymbolInfo.Symbol != null)
                    {
                        string className = qualifiedSymbolInfo.Symbol.Name.ToString();
                        sdk_map2 sdkMap = SDKMappingSQLConnector.GetInstance().GetSDKMapFromClassAndNamespace(TransformProject.sdkId, nsString, className);
                        if (sdkMap != null)
                        {
                            string newNamespace = sdkMap.namespace_map.new_namespace;
                            string newClassName = sdkMap.new_classname;
                            QualifiedNameSyntax newQualifiedNameNode = QualifiedName(IdentifierName(newNamespace), IdentifierName(newClassName)).WithTriviaFrom(oldQualifiedNameNode);
                            documentEditor.ReplaceNode(oldQualifiedNameNode, newQualifiedNameNode);
                        }
                    }
                }
            }
        }

        private void ReplaceIdentifierNames()
        {
            // https://duckduckgo.com/?q=nested+selection+linq&ia=qa
            IEnumerable<IdentifierNameSyntax> identifierNames = tree.GetRoot().DescendantNodes().OfType<IdentifierNameSyntax>();
            foreach (IdentifierNameSyntax oldNameNode in identifierNames) // iterate over all identifier names in the file
            {
                if (!(oldNameNode.Parent is QualifiedNameSyntax))
                {
                    var semanticObjCreation = semanticModel.GetSymbolInfo(oldNameNode);
                    var nodeTypeInfo = semanticModel.GetTypeInfo(oldNameNode);
                    if ((nodeTypeInfo.Type != null || oldNameNode.Parent is ObjectCreationExpressionSyntax) && semanticObjCreation.Symbol != null)
                    {
                        var oldNamespace = semanticObjCreation.Symbol.ContainingNamespace.Name;
                        String oldClassname = semanticObjCreation.Symbol.Name.ToString();
                        sdk_map2 sdkMap = SDKMappingSQLConnector.GetInstance().GetSDKMapFromClassAndNamespace(TransformProject.sdkId, oldNamespace, oldClassname);
                        if (sdkMap != null)
                        {
                            String newClassname = sdkMap.new_classname;
                            SyntaxToken oldNameToken = oldNameNode.DescendantTokens().First();
                            SyntaxToken name = Identifier(newClassname).WithTriviaFrom(oldNameToken);
                            IdentifierNameSyntax newNameNode = oldNameNode.WithIdentifier(name);
                            documentEditor.ReplaceNode(oldNameNode, newNameNode);
                        }
                    }
                }
            }
        }
    }
}