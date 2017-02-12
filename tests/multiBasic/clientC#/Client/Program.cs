using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDK.FirstSpace;
using SDK.SecondSpace;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Sample1.add(1, 2));
            Program2.Run();
            Program3.Run();
        }
    }

    class Program3
    {
        public static void Run()
        {
            Console.WriteLine(Sample4.add5(1, 2, 3, 4, 5));
        }
    }
}
