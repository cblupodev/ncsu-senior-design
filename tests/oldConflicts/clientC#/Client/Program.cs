using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDK;

namespace Client
{
    class Sample1
    {
      public static int minus(int a, int b) {
        return a - b;
      }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(SDK.Sample1.add(1, 2));
            Console.WriteLine(Sample1.minus(3, 2));
        }
    }
}
