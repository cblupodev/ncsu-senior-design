using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDK.PreSpaceA;
using SDK.PreSpaceB;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Sample1.add(1, 2));
            Console.WriteLine(Sample2.multiply(3, 5));
            Console.WriteLine(Sample3.divied(10, 2));
        }
    }
}
