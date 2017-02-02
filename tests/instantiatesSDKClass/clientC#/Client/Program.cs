using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDK;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new Sample1();
            Console.WriteLine(obj.add(1, 2));
        }
    }
}
