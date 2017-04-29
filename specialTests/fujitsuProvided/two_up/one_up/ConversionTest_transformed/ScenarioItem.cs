using System.Xml.Linq;

namespace Fujitsu.Tools.SDKExplorer.Controller
{
    public class ScenarioItem
    {
        public enum ScenarioItemType
        {
            Group,
            Scenario,
            Document
        }

        public ScenarioItem Group { get; set; }
        public string Text { get; set; }
        public XElement Element { get; set; }
        public XNamespace NameSpace { get; set; }
        public string DocumentName { get; set; }
        public string Settings { get; set; }
        public ScenarioItemType Type { get; set; }
    }
}