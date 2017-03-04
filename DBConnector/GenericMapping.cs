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
    }
}
