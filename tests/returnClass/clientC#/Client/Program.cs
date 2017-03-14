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
            Console.WriteLine(GetSDKClass().mul(3, 5));
        }

        static Sample1 GetSDKClass()
        {
            return new Sample1();
        }
    }
}
