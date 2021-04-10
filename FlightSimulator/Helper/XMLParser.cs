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
            string filename = @"public/playback_small.xml";

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
