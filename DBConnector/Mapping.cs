using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnector
{
    public class Mapping
    {
        public string OldNamespace { get; set; }
        public string NewNamespace { get; set; }
        public string ModelIdentifierGUID { get; set; }
        public string OldClassName { get; set; }
        public string NewClassName { get; set; }
        public string OldDllPath { get; set; }
        public string NewDllPath { get; set; }


        public Mapping(string OldNamespace, string ModelIdentifierGUID, string OldClassName)
        {
            this.OldNamespace = OldNamespace;
            this.ModelIdentifierGUID = ModelIdentifierGUID;
            this.OldClassName = OldClassName;
        }

        public Mapping(string OldNamespace, string NewNamespace, string ModelIdentifierGUID, string OldClassName, string NewClassName, string OldDllPath, string NewDllPath)
        {
            this.OldNamespace = OldNamespace;
            this.NewNamespace = NewNamespace;
            this.ModelIdentifierGUID = ModelIdentifierGUID;
            this.OldClassName = OldClassName;
            this.NewClassName = NewClassName;
            this.OldDllPath = OldDllPath;
            this.NewDllPath = NewDllPath;
        }
    }
}
