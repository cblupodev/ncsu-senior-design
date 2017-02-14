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
            var abc = new LinkedList<Sample1>();
            abc.AddLast(new Sample1());
            Console.WriteLine(Sample1.add(1, 2));
            Console.WriteLine(abc.Count);
        }
    }
}
