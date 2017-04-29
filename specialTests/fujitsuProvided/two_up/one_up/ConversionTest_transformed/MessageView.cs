using System.Runtime.Serialization;

namespace Fujitsu.Tools.SDKExplorer.Controller
{
    [DataContract]
    public class MessageView
    {
        [DataMember]
        public string Raw { get; set; }

        [DataMember]
        public string Pretty { get; set; }

        [DataMember]
        public string Source { get; set; }
    }
}