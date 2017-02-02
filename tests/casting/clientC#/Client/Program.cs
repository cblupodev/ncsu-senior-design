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
            var obj = (Sample1)new Sample1();
            Console.WriteLine(obj.GetType());
            Console.WriteLine(Sample1.add(1, 2));
        }
    }
}
