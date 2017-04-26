using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    class TextLogger : TextWriter
    {

        private string currentValue = "";
        private TextWriter target;

        public TextLogger(TextWriter target)
        {
            this.target = target;
        }

        public string GetValue()
        {
            return currentValue;
        }

        public string GetAndClearValue()
        {
            var val = GetValue();
            ClearValue();
            return val;
        }

        public void ClearValue()
        {
            currentValue = "";
        }

        public TextWriter GetTarget()
        {
            return target;
        }

        public override Encoding Encoding
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void Write(char value)
        {
            currentValue += value;
        }
    }
}
