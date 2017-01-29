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
        private object customAttributeName = "ModelIdentifier";

        // param 0 = the path to the old sdk dll with the custom attributes
        // param 1 = the path to tne new sdk dll with the custom attributes
        // param 2 = the path to the old client code that you want to transform
        // http://stackoverflow.com/questions/4791140/how-do-i-start-a-program-with-arguments-when-debugging
        static void Main(string[] args)
        {
            FileTransform fileTransform = new FileTransform();
            fileTransform.run(args);
        }

        private void run(string[] args)
        {
            findCustomerAttributes(args[0], args[1]);
            loadVariables(args[2]);

            string[] mockOldUsings = { "FujitsuSDKOld" }; // magic

            findOldUsingsAndReplacIfCertainClassesFound(mockOldUsings);
        }

        // search for using statements with the old sdk. Then if they are found then look for classes that coresponded to the custom attributes
        // if classes are found then replace the old using statement with the new one
        // oldSDKUsings is a list of the old sdk using statements
        private void findOldUsingsAndReplacIfCertainClassesFound(IEnumerable<string> oldSDKUsings)
        {
            foreach (var usingDirective in root.Usings) // iterate over each using statement
            {
                var name = semanticModel.GetSymbolInfo(usingDirective.Name); // https://github.com/dotnet/roslyn/wiki/Getting-Started-C%23-Semantic-Analysis
                if (name.Symbol == null) // I don't know why I have to do this. I don't know why our namespace is diffrent than the System ones, magic
                {
                    // get the text for the using
                    IdentifierNameSyntax ins = (IdentifierNameSyntax)usingDirective.Name;
                    var valueText = ins.Identifier.ValueText;
                    foreach (var oldUse in oldSDKUsings) // iterate over the old usings, provided as the input
                    {
                        // if an old using is located in the file then seek for object creations that use classes that are tagged
                        if (valueText.Equals(oldUse))
                        {
                            IEnumerable<ObjectCreationExpressionSyntax> objectCreations = root.DescendantNodes().OfType<ObjectCreationExpressionSyntax>();
                            foreach (ObjectCreationExpressionSyntax item in objectCreations) // iterate over all object creations in the file
                            {
                                var semanticObjCreation = semanticModel.GetSymbolInfo(item.Type);
                                //semanticObcCreation
                                // if find a class that was tagged then replace the old using with the new one
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
            this.clientFilePath = clientFilePath;

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
        private List<Mapping> findCustomerAttributes(string oldDllPath, string newDllPath)
        {
            List<Mapping> mappings = new List<Mapping>();

            verifyFileExists(oldDllPath);
            verifyFileExists(newDllPath);
            var oldassem = Assembly.LoadFile(oldDllPath);
            var newassem = Assembly.LoadFile(newDllPath);

            var types = oldassem.GetTypes(); // the types will tell you if there are custom data attributes
            foreach(var type in types) // itereate over old dll to find custom attributes
            {
                foreach(var attr in type.CustomAttributes)
                {                   
                    if (attr.AttributeType.Name.Equals(customAttributeName))
                    {
                        Mapping mapping = new Mapping();
                        mapping.ModelIdentifierGUID = (string)attr.ConstructorArguments.First().Value;
                        mapping.ClassName = type.Name;
                        mapping.OldNamespace = type.Namespace;
                        mappings.Add(mapping);
                    }
                }
            }

            types = newassem.GetTypes();
            foreach(var type in types) // iterate new dll to find the custom attribute mappings
            {
                foreach(var attr in type.CustomAttributes)
                {
                    if (attr.AttributeType.Name.Equals(customAttributeName))
                    {
                        foreach (var mapping in mappings)
                        {
                            if (mapping.ModelIdentifierGUID.Equals((string)attr.ConstructorArguments.First().Value)) // if an existing GUID equals the guid then associate the namespace to the old mapping
                            {
                                mapping.NewNamespace = type.Namespace; // assoicate the model identifier to the new namespace
                            }
                        }
                    }
                }
            }

            return mappings;
        }

        private void verifyFileExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("file doesn't exist", filePath);
            }
        }
    }
}