﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDK
{

    [ModelIdentifierAttribute("00000000-0000-4000-8000-000000000001")]
    public class Change1
    {
        public int add(int a, int b)
        {
            return a + b;
        }
    }
}
