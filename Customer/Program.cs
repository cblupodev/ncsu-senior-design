using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//test
using testAlias = /* test */ FujitsuSDKOld;
using oldNamespace;


namespace Customer
{
    class Program : oldNamespace.OldClass
    {

        private testAlias.Sample a = new testAlias.Sample();
        //this is a comment to test keeping track of trivia
        private oldNamespace.OldClass b = new oldNamespace.OldClass();

        static void Main(string[] args)
        {
            FujitsuSDKOld.Sample s = new FujitsuSDKOld.Sample();
            // this is also a test comment
            oldNamespace.OldClass t = new oldNamespace.OldClass();

            OldClass cast = (OldClass)new Casting();
        }

        public OldClass testMethod(OldClass a)
        {
            Dictionary<String, oldNamespace.OldClass> test = new Dictionary<string, OldClass>();
            OldClass b = new OldClass();
            return b;
        }
    }
}