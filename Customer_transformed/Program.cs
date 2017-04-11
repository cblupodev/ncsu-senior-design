using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBConnector;
//test
using testAlias = /* test */ FujitsuSDKNew;
using newNamespace;


namespace Customer
{
    [ModelIdentifier("26A993C4-65C6-4168-B045-9FABB2A1526D")]
    class Program : newNamespace.NewClass
    {

        private testAlias.Sample a = new testAlias.Sample();
        //this is a comment to test keeping track of trivia
        private newNamespace.NewClass b = new newNamespace.NewClass();

        static void Main(string[] args)
        {
            FujitsuSDKNew.SampleNew s = new FujitsuSDKNew.SampleNew();
            // this is also a test comment
            newNamespace.NewClass t = new newNamespace.NewClass();

            NewClass cast = (NewClass)new Casting();
        }

        public NewClass testMethod(NewClass a)
        {
            Dictionary<String, newNamespace.NewClass> test = new Dictionary<string, NewClass>();
            NewClass b = new NewClass();
            return b;
        }
    }
}