using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Symbols;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System;
using static Microsoft.CodeAnalysis.VisualBasic.SyntaxFactory;
using EFSQLConnector;
using Microsoft.CodeAnalysis.Editing;

namespace TransformClient
{
    public class TransformFileVBasic
    {

        private CompilationUnitSyntax root;
        private SemanticModel semanticModel;
        private SyntaxTree tree;
        private DocumentEditor documentEditor;
        private string clientFilePath;

        public TransformFileVBasic(DocumentEditor documentEditor)
        {
            this.documentEditor = documentEditor;
            this.root = (CompilationUnitSyntax)documentEditor.OriginalRoot;
            this.tree = root.SyntaxTree;
            this.semanticModel = documentEditor.SemanticModel;
        }

        public TransformFileVBasic(string fileName)
        {
            Helper.verifyFileExists(fileName);
            SyntaxTree tree;
            this.clientFilePath = fileName;

            using (var stream = File.OpenRead(clientFilePath))
            {
                tree = VisualBasicSyntaxTree.ParseText(SourceText.From(stream), path: clientFilePath);
            }

            root = (CompilationUnitSyntax)tree.GetRoot();

            // https://github.com/dotnet/roslyn/wiki/Getting-Started-C%23-Semantic-Analysis
            var compilation = VisualBasicCompilation.Create("client_old").AddReferences( //magic
                                                    MetadataReference.CreateFromFile(
                                                        typeof(object).Assembly.Location))
                                                    .AddSyntaxTrees(tree);
            semanticModel = compilation.GetSemanticModel(tree);
        }

        // search for using statements with the old sdk. Then if they are found then look for classes that coresponded to the custom attributes
        // if classes are found then replace the old using statement with the new one
        // oldSDKUsings is a list of the old sdk using statements
        public SyntaxTree replaceSyntax()
        {
            replaceUsingStatements();
            replaceQualifiedNames();
            replaceIdentifierNames();
            return documentEditor.GetChangedRoot().SyntaxTree;
        }

        private void replaceUsingStatements()
        {
            IEnumerable<ImportsStatementSyntax> usingDirectiveNodes = tree.GetRoot().DescendantNodes().OfType<ImportsStatementSyntax>();
            foreach (ImportsStatementSyntax oldUsingDirectiveNode in usingDirectiveNodes) // iterate over all qualified names in the file
            {
                // todo could be problems if this import statement isn't simple, however even if an alias is used in the import it's still simple
                SimpleImportsClauseSyntax oldUsingDirectiveSimpleNode = oldUsingDirectiveNode.DescendantNodes().OfType<SimpleImportsClauseSyntax>().First();
                var usingDirectiveSymbolInfo = semanticModel.GetSymbolInfo(oldUsingDirectiveNode);
                var oldNamespace = oldUsingDirectiveSimpleNode.Name.GetText().ToString();
                List<namespace_map> namespaces = NSMappingSQLConnector.GetInstance().GetNamespaceMapsFromOldNamespace(TransformProject.sdkId, oldNamespace);
                if (namespaces != null)
                {
                    ImportsStatementSyntax previousUsingDirectiveNode = oldUsingDirectiveNode;
                    int i = 1;
                    foreach (namespace_map nsMap in namespaces)
                    {
                        var newNamespace = nsMap.new_namespace;
                        NameSyntax newIdentifierNode = IdentifierName(newNamespace);
                        var newUsingDirectiveNode = oldUsingDirectiveSimpleNode.WithName(newIdentifierNode);
                        if (i == namespaces.Count)
                        {
                            documentEditor.ReplaceNode(oldUsingDirectiveNode, newUsingDirectiveNode);
                        }
                        else
                        {
                            documentEditor.InsertAfter(oldUsingDirectiveNode, newUsingDirectiveNode);
                        }
                        i++;
                    }
                }
            }
        }

        private void replaceQualifiedNames()
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

        private void replaceIdentifierNames()
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