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
        static Sample1 item;

        static void Main(string[] args)
        {
            Console.WriteLine(Sample1.add(1, 2));
            item = new Sample1();
            Console.WriteLine(item.mul(3, 15));
            Sample1 item2 = new Sample1();
            Console.WriteLine(item2.mul(4, 2));
        }
    }
}
