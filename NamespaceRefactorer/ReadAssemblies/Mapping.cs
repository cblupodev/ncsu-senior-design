using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamespaceRefactorer
{
    class Mapping
    {
        public string OldNamespace { get; set; }
        public string NewNamespace { get; set; }
        public string ModelIdentifierGUID { get; set; }
        public string ClassName { get; set; }
    }
}
