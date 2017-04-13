﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDK.PreSpaceA
{
    [ModelIdentifierAttribute("00000000-0000-4000-8000-000000000001")]
    public class Sample1
    {
        public static int add(int a, int b)
        {
            return a + b;
        }
    }
    
    [ModelIdentifierAttribute("00000000-0000-4000-8000-000000000002")]
    public class Sample2
    {
        public static int multiply(int a, int b)
        {
            return a * b;
        }
    }
}

namespace SDK.PreSpaceB
{
    [ModelIdentifierAttribute("00000000-0000-4000-8000-000000000003")]
    public class Sample3
    {
        public static int divied(int a, int b)
        {
            return a / b;
        }
    }
}