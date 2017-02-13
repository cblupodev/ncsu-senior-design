using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NamespaceRefactorer
{
    class XMLTransform
    {
        static void Main(string[] args)
        {
            string fileName = @"C:\Users\Christopher Lupo\Documents\Visual Studio 2015\Projects\2017SpringTeam25\XMLMod\Client.xml";
               // find the namsespace by calling Descendents() on the Root and drill down into the properties to find the namsespace you need
            XNamespace ns = XNamespace.Get("http://schemas.microsoft.com/developer/msbuild/2003"); // https://granadacoder.wordpress.com/2012/10/11/how-to-find-references-in-a-c-project-file-csproj-using-linq-xml/
            XDocument xdoc = XDocument.Load(fileName);

            var outputpathlinq = from outp in xdoc.Descendants(ns + "OutputPath")
                             select outp;
            string outputpath = outputpathlinq.First().Value;

            var references = from reference in xdoc.Descendants(ns + "Reference")
                             where reference.Element(ns + "HintPath") != null
                             select reference;

            try
            {
                foreach (var r in references)
                {
                    // TOOD find the refernces that are part of the old sdk. Get a old_dll_files list from the database and compare
                    // also maybe do this work in the linq statement (cleaner)
                    // TODO remove them
                    r.Remove();
                }
            }
            catch (NullReferenceException nre)
            {
                // null exception is thrown because the reference is remove from the list, so just ignore
            }

            // TODO make sure you do something like outputfilepath + basename(newSDKAbsoluteFilePath), before you add to xml

            // https://www.youtube.com/playlist?list=PL6n9fhu94yhX-U0Ruy_4eIG8umikVmBrk
            XElement addedref = new XElement("Reference", new XAttribute("Include", "SDK, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL"),
                    new XElement("SpecificVersion", "False"),
                    new XElement("HintPath", outputpath + "SDK2.dll"),
                    new XElement("Private", "False")
                );

            xdoc.Descendants(ns + "ItemGroup").First().AddFirst(addedref);

            xdoc.Save(fileName);
        }
    }
}
