﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDK
{

    [ModelIdentifier("00000000-0000-4000-8000-00000001")]
    public class Change1
    {
        public static Change1 Item = new Change1();

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
