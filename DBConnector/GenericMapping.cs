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
        public string DllPath { get; set; }
        public int SdkId { get; set; }
        public string NewDllFullName { get; set; }

        public GenericMapping(string Namespace, string ModelIdentifierGUID, string Classname, string dllPath, int sdkId)
        {
            this.Namespace = Namespace;
            this.ModelIdentifierGUID = ModelIdentifierGUID;
            this.ClassName = Classname;
            this.DllPath = dllPath;
            this.SdkId = sdkId;
        }

        public GenericMapping(string Namespace, string ModelIdentifierGUID, string Classname, string DllPath, string NewDllFullName, int SdkId)
        {
            this.Namespace = Namespace;
            this.ModelIdentifierGUID = ModelIdentifierGUID;
            this.ClassName = Classname;
            this.DllPath = DllPath;
            this.NewDllFullName = NewDllFullName;
            this.SdkId = SdkId;
        }
    }
}
