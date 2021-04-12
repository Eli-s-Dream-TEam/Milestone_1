using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

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

            renameDuplicates(ref elementList);

            return elementList;
        }

        //assuming same name paramaters are one after the other, renaming duplicates as 'name + "j"' when j is the number of times the elemnt is shown until that point.
        private static void renameDuplicates(ref List<string> list)
        {
            List<(int, string)> duplicates = new List<(int, string)>() { };
            int appearnces = 1;
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].Equals(list[i - 1]))
                {
                    appearnces++;
                    var t = (i, appearnces.ToString());
                    duplicates.Add(t);
                }
                else
                {
                    appearnces = 1;
                }
            }
            foreach (var duplicateParam in duplicates)
            {
                list[duplicateParam.Item1] = list[duplicateParam.Item1] + duplicateParam.Item2;
            }
        }
    }
}
