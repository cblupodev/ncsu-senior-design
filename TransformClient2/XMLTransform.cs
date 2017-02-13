using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NamespaceRefactorer
{
    public class XMLTransform
    {

        private string xmlElementOutputPathName = "OutputPath";
        private string xmlElementReferenceName = "Reference";
        private string xmlElementHintPathName = "HintPath";

        public void transformXml(string filePath, HashSet<String> newdllSet, HashSet<String> olddllSet)
        {
            filePath = @"C:\Users\Christopher Lupo\Documents\Visual Studio 2015\Projects\2017SpringTeam25\TransformClient2\Client - Copy.xml"; // magic
            // find the namsespace by calling Descendents() on the Root and drill down into the properties to find the namsespace you need
            XNamespace ns = XNamespace.Get("http://schemas.microsoft.com/developer/msbuild/2003"); // https://granadacoder.wordpress.com/2012/10/11/how-to-find-references-in-a-c-project-file-csproj-using-linq-xml/
            XDocument xdoc = XDocument.Load(filePath);

            string outputPath = (from outp in xdoc.Descendants(ns + xmlElementOutputPathName)
                                 select outp).First().Value;

            var references = from reference in xdoc.Descendants(ns + xmlElementReferenceName)
                             where reference.Element(ns + xmlElementHintPathName) != null
                             //where olddllSet.Contains(Path.GetFullPath((outputPath+Path.GetFileName(reference.Descendants(ns + xmlElementHintPathName).First().Value)))) == true
                                // Don't remove the line above
                                // that checks if the reference is part of the old sdk
                                // if the reference is not part of the old sdk then don't include it in the selection output
                                // because if it is included in the output then it will get removed
                             select reference;

            try
            {
                foreach (var r in references)
                {
                    // TOOD find the refernces that are part of the old sdk. Get a old_dll_files list from the database and compare
                       // also maybe do this work in the linq statement (cleaner)
                    // Don't remove the line below
                    //r.Remove();
                }
            }
            catch (NullReferenceException nre)
            {
                // null exception is thrown because the reference is remove from the list, so just ignore
            }


            string[] newDlls = { "C:\\newsdk.dll", "C:\\newsdk2.dll", "C:\\newsdk3.dll" }; // magic

            foreach (var dll in newDlls)
            {
                // https://www.youtube.com/playlist?list=PL6n9fhu94yhX-U0Ruy_4eIG8umikVmBrk
                XElement addedref = new XElement("Reference", new XAttribute("Include", "SDK, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL"),
                        new XElement("SpecificVersion", "False"),
                        new XElement("HintPath", outputPath + Path.GetFileName(dll)),
                        new XElement("Private", "False")
                    );
                xdoc.Descendants(ns + "ItemGroup").First().AddFirst(addedref); 
            }

            xdoc.Save(@"..\\..\\Client.xml");
        }
    }
}