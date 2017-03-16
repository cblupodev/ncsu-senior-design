using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDK.NewSpaceOne
{

    [ModelIdentifier("00000000-0000-4000-8000-000000000001")]
    public class Sample1
    {
        public static int add(int a, int b)
        {
            return a + b;
        }
    }

    [ModelIdentifier("00000000-0000-4000-8000-000000000002")]
    public class Sample2
    {
        public static int add3(int a, int b, int c)
        {
            return a + b + c;
        }
    }
}
