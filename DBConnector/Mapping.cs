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

        // https://msdn.microsoft.com/en-us/library/ms173147(v=vs.90).aspx
        public bool Equals(Mapping m)
        {
            // If parameter is null return false:
            if ((object)m == null)
            {
                return false;
            }

            // Return true if the fields match:
            if (
                this.OldNamespace.Equals(m.OldNamespace) &&
                this.NewNamespace.Equals(m.NewNamespace) &&
                this.ModelIdentifierGUID.Equals(m.ModelIdentifierGUID) &&
                this.OldClassName.Equals(m.OldClassName) &&
                this.NewClassName.Equals(m.NewClassName) &&
                this.OldDllPath.Equals(m.OldDllPath) &&
                this.NewDllPath.Equals(m.NewDllPath)
                )
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.ModelIdentifierGUID.GetHashCode(); // http://stackoverflow.com/questions/9317582/correct-way-to-override-equals-and-gethashcode
        }
    }
}
