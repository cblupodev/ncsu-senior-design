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
            Console.WriteLine(Sample1.add(1, 2));
            Console.WriteLine(SDK.Sample1.Item.mul(3, 5));
        }
    }
}
