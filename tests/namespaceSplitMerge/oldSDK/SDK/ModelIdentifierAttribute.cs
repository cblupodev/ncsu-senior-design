using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDK
{
    [AttributeUsage(AttributeTargets.All)]
    public class ModelIdentifierAttribute : Attribute
    {
        // Private fields.
        private string name;

        // This constructor defines two required parameters: name

        public ModelIdentifierAttribute(string name)
        {
            this.name = name;
        }

        // Define Name property.
        // This is a read-only attribute.
        public virtual string Name
        {
            get { return name; }
        }
    }

}
