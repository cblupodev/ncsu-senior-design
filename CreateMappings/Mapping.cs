using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamespaceRefactorer
{
    public class Mapping
    {
        public string OldNamespace { get; set; }
        public string NewNamespace { get; set; }
        public string ModelIdentifierGUID { get; set; }
        public string ClassName { get; set; }

        public Mapping(string oldNameSpace, string ModelIdentifierGUID, string ClassName)
        {
            this.OldNamespace = oldNameSpace;
            this.ModelIdentifierGUID = ModelIdentifierGUID;
            this.ClassName = ClassName;
        }
    }
}
