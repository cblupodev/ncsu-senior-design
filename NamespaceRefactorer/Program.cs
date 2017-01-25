using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NamespaceRefactorer
{
    class Program
    {
        static void Main(string[] args)
        {
            var assem = Assembly.LoadFile("C:\\Users\\Christopher Lupo\\Documents\\Visual Studio 2015\\Projects\\2017SpringTeam25\\FujitsuSDKOld\\bin\\Debug\\FujitsuSDKOld.dll"); // the .dll file
            var console = assem.GetType("System.Console");
            var att = console.Attributes;
        }
    }
}
