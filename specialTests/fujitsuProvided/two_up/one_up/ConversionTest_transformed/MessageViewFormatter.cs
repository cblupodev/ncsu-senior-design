using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Xsl;
using Fujitsu.Infrastructure.Core;

namespace Fujitsu.Tools.SDKExplorer.Controller
{
    internal class MessageViewFormatter<T> where T : class
    {
        private readonly object m_blob;
        private readonly string m_Source;

        public MessageViewFormatter(object blob)
        {
            m_Source = "";
            m_blob = blob;
        }

        public MessageViewFormatter(object blob, string source)
        {
            m_Source = source;
            m_blob = blob;
        }

        public MessageView FormatMessage()
        {
            var messageView = new MessageView {Source = m_Source};
            if (typeof(T) != typeof(string))
            {
                using (var memoryStream = new MemoryStream())
                using (var reader = new StreamReader(memoryStream))
                {
                    var serializer = ProcessKnownTypeResolver.CreateSerializer(typeof(T));
                    serializer.WriteObject(memoryStream, m_blob);
                    memoryStream.Position = 0;
                    messageView.Raw = reader.ReadToEnd();
                }
            }
            else
            {
                messageView.Raw = m_blob as string;
            }
            var transform = new XslCompiledTransform();
            var xReader = XmlReader.Create(new StringReader(""/*Fujitsu.Tools.SDKExplorer.Controller.Properties.Resources.defaultssxslt.ToString()*/));
            transform.Load(xReader);
            var xContent = XmlReader.Create(new StringReader(messageView.Raw));
            var outText = new StringBuilder();
            var xOutput = XmlWriter.Create(outText);
            transform.Transform(xContent, xOutput);
            var regex = new Regex("z:Id</SPAN><SPAN class=\"m\">=\"</SPAN><B>[0-9]{0,3}</B><SPAN class=\"m\">\"</SPAN>");
            messageView.Pretty = regex.Replace(outText.ToString(), "</SPAN>");
            return messageView;
        }
    }
}