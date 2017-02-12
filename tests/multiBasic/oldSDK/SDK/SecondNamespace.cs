using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDK.SecondSpace
{

    [ModelIdentifier("00000000-0000-4000-8000-00000003")]
    public class Sample3
    {
        public static int add4(int a, int b, int c, int d)
        {
            return a + b + c + d;
        }
    }

    [ModelIdentifier("00000000-0000-4000-8000-00000004")]
    public class Sample4
    {
        public static int add5(int a, int b, int c, int d, int e)
        {
            return a + b + c + d + e;
        }
    }
}
