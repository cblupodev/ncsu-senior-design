﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDK.NewSpaceTwo
{

    [ModelIdentifierAttribute("00000000-0000-4000-8000-000000000003")]
    public class Change3
    {
        public static int add4(int a, int b, int c, int d)
        {
            return a + b + c + d;
        }
    }

    [ModelIdentifierAttribute("00000000-0000-4000-8000-000000000004")]
    public class Sample4
    {
        public static int add5(int a, int b, int c, int d, int e)
        {
            return a + b + c + d + e;
        }
    }
}
