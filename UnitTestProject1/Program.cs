using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("arguments:");
                Console.WriteLine("\tlist: lists test methods");
                Console.WriteLine("\t<test method names>: runs the selected tests");
                Console.WriteLine("input arguments");
                args = Console.ReadLine().Split(' ');
                if (args.Length == 0)
                {
                    return;
                }
                new Program().Run(args);
                Console.WriteLine("Press enter to exit...");
                Console.ReadLine();
            }
            else
            {
                new Program().Run(args);
            }
        }
        
        private string testDirectory = null;
        private string startDirectory;

        public void Run(string[] args)
        {
            startDirectory = Directory.GetCurrentDirectory();
            if ( args[0] == "list" )
            {
                var methods = GetTestMethods(this.GetType().Assembly);
                methods.Sort((a,b)=>a.Name.CompareTo(b.Name));
                foreach ( var method in methods )
                {
                    Console.WriteLine(method.Name);
                }
            }
            else
            {
                CreateTestDirectory();
                Directory.SetCurrentDirectory(testDirectory);
                var methods = GetTestMethods(this.GetType().Assembly);
                methods.Sort((a, b) => a.Name.CompareTo(b.Name));
                var passed = new List<string>();
                var failed = new List<string>();
                foreach ( var method in methods )
                {
                    for ( var i = 0; i < args.Length; i++ )
                    {
                        if ( Matches(method.Name, args[i]) )
                        {
                            Console.WriteLine("Running: " + method.Name);
                            var success = RunTest(method);
                            if ( success )
                            {
                                passed.Add(method.Name);
                                Console.WriteLine("Test Passed");
                            }
                            else
                            {
                                failed.Add(method.Name);
                                Console.WriteLine("Test Failed");
                            }
                            break;
                        }
                    }
                }
                Console.WriteLine("Tests Passed (" + passed.Count + "):");
                Console.WriteLine(String.Join("\n", passed));
                Console.WriteLine("Tests Failed (" + failed.Count + "):");
                Console.WriteLine(String.Join("\n", failed));
            }
        }

        private void CreateTestDirectory()
        {
            if (testDirectory != null)
            {
                return;
            }
            var cur = Directory.GetCurrentDirectory();
            while ( Directory.GetFiles(cur, "2017SpringTeam25.sln").Length <= 0 )
            {
                cur = Directory.GetParent(cur).FullName;
            }

            if (!Directory.Exists(Path.Combine(cur, "TestResults")))
            {
                Directory.CreateDirectory(Path.Combine(cur, "TestResults"));
            }
            var directoryName = "Deploy_" + Environment.UserName + " " + DateTime.Now.ToString("yyyy-MM-dd HH_mm_ss");
            if (Directory.Exists(Path.Combine(cur, "TestResults", directoryName)))
            {
                int num = 1;
                while (Directory.Exists(Path.Combine(cur, "TestResults", directoryName + num.ToString())))
                {
                    num++;
                }
                directoryName = directoryName + num.ToString();
            }
            var dir = Directory.CreateDirectory(Path.Combine(cur, "TestResults", directoryName));
            Directory.CreateDirectory(Path.Combine(cur, dir.FullName, "Results"));
            testDirectory = dir.FullName;
        }

        private List<MethodInfo> GetTestMethods(Assembly assem)
        {
            var ret = new List<MethodInfo>();
            foreach ( var type in assem.GetTypes() )
            {
                if ( type.GetCustomAttributes(typeof(TestClassAttribute), true).Count() > 0 )
                {
                    foreach ( var method in type.GetMethods() )
                    {
                        if ( method.GetCustomAttributes(typeof(TestMethodAttribute)).Count() > 0 ) {
                            ret.Add(method);
                        }
                    }
                }
            }
            return ret;
        }

        private bool Matches(string target, string pattern)
        {
            var split = pattern.Split('*');
            for ( var i = 0; i < split.Length; i++ )
            {
                var split2 = split[i].Split('.');
                for ( var j = 0; j < split2.Length; j++ )
                {
                    split2[j] = Regex.Escape(split2[j]);
                }
                split[i] = String.Join(".", split2);
            }
            pattern = "^" + String.Join(".*", split) + "$";
            return Regex.IsMatch(target, pattern, RegexOptions.IgnoreCase);
        }

        // taken from http://stackoverflow.com/questions/1066674/how-do-i-copy-a-folder-and-all-subfolders-and-files-in-net/1066811#1066811
        private static void CopyDirectory(string sourcePath, string destPath)
        {
            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }

            foreach (string file in Directory.GetFiles(sourcePath))
            {
                string dest = Path.Combine(destPath, Path.GetFileName(file));
                File.Copy(file, dest);
            }

            foreach (string folder in Directory.GetDirectories(sourcePath))
            {
                string dest = Path.Combine(destPath, Path.GetFileName(folder));
                CopyDirectory(folder, dest);
            }
        }

        private bool RunTest(MethodInfo method)
        {
            foreach ( var deploy in method.GetCustomAttributes<DeploymentItemAttribute>() ) {
                if ( ! Directory.Exists(deploy.OutputDirectory) )
                {
                    CopyDirectory(Path.Combine(startDirectory, deploy.Path), deploy.OutputDirectory);
                }
            }
            var curDir = Directory.GetCurrentDirectory();
            var tracer = new TextWriterTraceListener(Path.Combine(testDirectory, "Results", method.Name + ".txt"));
            Trace.Listeners.Add(tracer);
            bool passed = false;
            try
            {
                var instance = method.ReflectedType.GetConstructor(new Type[0]).Invoke(new object[0]);
                method.Invoke(instance, new object[0]);
                passed = true;
            }
            catch (TargetInvocationException e)
            {
                Console.WriteLine("Exception thrown: ");
                Console.WriteLine(e.InnerException.ToString());
                Trace.WriteLine("--- Test Results:");
                Trace.WriteLine("Exception thrown: ");
                Trace.WriteLine(e.InnerException.ToString());
                passed = false;
            }
            Trace.WriteLine("Test " + (passed ? "Passed" : "Failed"));
            Trace.Listeners.Remove(tracer);
            tracer.Close();
            Directory.SetCurrentDirectory(curDir);
            return passed;
        }
    }
}
