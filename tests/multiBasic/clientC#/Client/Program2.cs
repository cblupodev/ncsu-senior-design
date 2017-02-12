using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDK.FirstSpace;
using SDK.SecondSpace;

namespace Client
{
    class Program2
    {
        public static void Run()
        {
            Console.WriteLine(Sample2.add3(1, 2, 3));
            Run2();
        }

        public static void Run2()
        {
            Console.WriteLine(Sample3.add4(1, 2, 3, 4));
        }
    }
}
