using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Helper
{
    class DataParser : ICSVDataParser
    {

        private int[] alieron = new int[1000];
        private int[] elevator = new int[1000];
        private int[] rudder = new int[1000];
        private int[] throttle = new int[1000];

        public DataParser()
        {
            generateData();
        }

        public int getDataInTime(string feat, int timeStamp)
        {
            switch(feat)
            {
                case "alieron": { return alieron[timeStamp];}
                case "elevator": { return elevator[timeStamp];}
                case "rudder": { return rudder[timeStamp];}
                case "throttle": { return throttle[timeStamp];}
                default: return -1;
            }
        }

        public string getFeatMostCorrFeature(string feat)
        {
            switch (feat)
            {
                case "alieron": { return "elevator"; }
                case "elevator": { return "rudder"; }
                case "rudder": { return "throttle"; }
                case "throttle": { return "alieron"; }
                default: return "";

            }
        }

        public int[] getFeatureData(string feat)
        {
            switch (feat)
            {
                case "alieron": { return alieron; }
                case "elevator": { return elevator; }
                case "rudder": { return rudder; }
                case "throttle": { return throttle; }
                default: return null;
            }
        }

        public List<string> getFeatures()
        {
            return new List<string> { "alieron", "elevator", "rudder", "throttle" };
        }

        private void generateData()
        {
            Random r = new Random();
            for (int i = 0; i < 1000; i++)
            {
                alieron[i] = r.Next(10);
                elevator[i] = r.Next(10);
                rudder[i] = r.Next(10);
                throttle[i] = r.Next(10);
            }
        }

    }
}
