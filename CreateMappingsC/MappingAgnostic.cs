using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateMappings
{
    public class MappingAgnostic
    {
        public string Namespace { get; set; }
        public string ModelIdentifierGUID { get; set; }
        public string ClassName { get; set; }

        public MappingAgnostic(string Namespace, string ModelIdentifierGUID, string Classname)
        {
            this.Namespace = Namespace;
            this.ModelIdentifierGUID = ModelIdentifierGUID;
            this.ClassName = Classname;
        }

        public MappingAgnostic(string OldNamespace, string NewNamespace, string ModelIdentifierGUID, string OldClassName, string NewClassName)
        {
            this.Namespace = OldNamespace;
            this.ModelIdentifierGUID = ModelIdentifierGUID;
            this.ClassName = OldClassName;
        }

        // https://msdn.microsoft.com/en-us/library/ms173147(v=vs.90).aspx
        public bool Equals(MappingAgnostic m)
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
