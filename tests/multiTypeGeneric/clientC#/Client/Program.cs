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
            var map = new Dictionary<Sample1, Sample1>();
            var itemA = new Sample1();
            var itemB = new Sample1();
            map.Add(itemA, itemB);
            Console.WriteLine(map.Count);
            Console.WriteLine(map[itemA].Equals(itemB)?1:0);
        }
    }
}
