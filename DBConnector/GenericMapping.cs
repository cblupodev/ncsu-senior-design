using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnector
{
    public class GenericMapping
    {
        public string Namespace { get; set; }
        public string ModelIdentifierGUID { get; set; }
        public string ClassName { get; set; }
        public string dllPath { get; set; }

        public int sdkId { get; set; }

        public GenericMapping(string Namespace, string ModelIdentifierGUID, string Classname, string dllPath, int sdkId)
        {
            this.Namespace = Namespace;
            this.ModelIdentifierGUID = ModelIdentifierGUID;
            this.ClassName = Classname;
            this.dllPath = dllPath;
            this.sdkId = sdkId;
        }

        // https://msdn.microsoft.com/en-us/library/ms173147(v=vs.90).aspx
        public bool Equals(GenericMapping m)
        {
            // If parameter is null return false:
            if ((object)m == null)
            {
                return false;
            }

            // Return true if the fields match:
            if (
                this.Namespace.Equals(m.Namespace) &&
                this.ModelIdentifierGUID.Equals(m.ModelIdentifierGUID) &&
                this.ClassName.Equals(m.ClassName)
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
