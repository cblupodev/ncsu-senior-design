using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDK
{

    [ModelIdentifier("00000000-0000-4000-8000-00000001")]
    public class Change1
    {
        public static int add(int a, int b)
        {
            return a + b;
        }

        [ModelIdentifier("00000000-0000-4000-8000-00000002")]
        public class Change2
        {
            public static int multiply(int a, int b)
            {
                return a * b;
            }
        }
    }
}
