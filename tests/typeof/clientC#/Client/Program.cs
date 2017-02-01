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
            Console.WriteLine(typeof(Sample1));
            Console.WriteLine(Sample1.add(1, 2));
        }
    }
}
