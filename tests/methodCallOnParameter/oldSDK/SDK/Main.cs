﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDK
{

    [ModelIdentifierAttribute("00000000-0000-4000-8000-000000000001")]
    public class Sample1
    {
        public static Sample1 Item = new Sample1();

        public static int add(int a, int b)
        {
            return a + b;
        }
        
        public int mul(int a, int b)
        {
            return a * b;
        }
    }
}
