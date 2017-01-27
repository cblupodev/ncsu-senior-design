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

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            var debugpath = @"C:\Users\Christopher Lupo\documents\visual studio 2015\Projects\2017SpringTeam25\debug.txt";
            //File.AppendAllText(debugpath, "888" + Environment.NewLine);
            //var assem = Assembly.LoadFile(@"C:\Users\Christopher Lupo\Documents\Visual Studio 2015\Projects\2017SpringTeam25\FujitsuSDKOld\bin\Debug\FujitsuSDKOld.dll"); // the .dll file

            //var asdf = assem.GetTypes(); // the types will tell you if there are custom data attributes           

            //var path = @"C:\Users\Christopher Lupo\Documents\Visual Studio 2015\Projects\2017SpringTeam25\Customer\client_old.cs";
            //SyntaxTree tree;
            //using (var stream = File.OpenRead(path))
            //{
            //    tree = CSharpSyntaxTree.ParseText(SourceText.From(stream), path: path);
            //}

            //var root = (CompilationUnitSyntax)tree.GetRoot();
            //var firstMember = root.Members[0]; // TODO loop the members
            //var namespaceDecleration = (NamespaceDeclarationSyntax)firstMember;
            //int i = 0;
            //bool seminal = true;
            //while (true)
            //{
            //    Console.WriteLine("asdf");
            //    try
            //    {
            //        var asdf2 = (ClassDeclarationSyntax)namespaceDecleration.Members[i];
            //        var text = asdf2.Identifier.Text;
            //        Debug.WriteLine("{0}   {1}   {3}", i, namespaceDecleration.Members.Count, text);
            //    }
            //    catch (Exception)
            //    {
            //        //seminal = false;
            //    }
            //    i++;
            //}

        }
    }
}