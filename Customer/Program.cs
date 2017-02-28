using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBConnector;
using FujitsuSDKOld;
using asdf;


namespace Customer
{
    [ModelIdentifier("26A993C4-65C6-4168-B045-9FABB2A1526D")]
    class Program
    {

        private FujitsuSDKOld.Sample a = new FujitsuSDKOld.Sample();
        //this is a comment to test keeping track of trivia
        private asdf.fudgeold2 b = new asdf.fudgeold2();

        static void Main(string[] args)
        {
            FujitsuSDKOld.Sample s = new FujitsuSDKOld.Sample();
            // this is also a test comment
            asdf.fudgeold2 t = new asdf.fudgeold2();
        }
    }
}
