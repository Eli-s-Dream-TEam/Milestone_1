using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace FlightSimulator.Helper
{
    class XMLParser
    {
        public static List<string> DeserializeFromXML()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = "xml";
            dlg.Filter = "XML Files | *.xml";
            dlg.Title = "Please provide the xml properties for your CSV file";
            string filename;

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                // Open document 
                filename = dlg.FileName;
            }

            else
            {
                return null;
            }

            List<string> elementList = new List<string>();
            var xml = XDocument.Load(filename);

            var query = from c in xml.Root.Descendants("output").Descendants("chunk") select c.Element("name").Value;

            foreach (string name in query)
            {
                elementList.Add(name);
            };

            return elementList;
        }

        //getting the sampling rate in Hz from the playback file.
        //public static int getSamplingRate(string file)
        //{
            


        //}
    }
}
