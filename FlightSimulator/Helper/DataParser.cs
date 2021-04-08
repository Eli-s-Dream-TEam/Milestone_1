using LiveCharts;
using LiveCharts.Defaults;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlightSimulator.Helper
{
    class DataParser : ICSVDataParser
    {

        private float[] alieron;
        private float[] elevator;
        private float[] rudder;
        private float[] throttle;
        private string csvFilePath;
        private string[] csvRows;
        private List<string> flightParamaters;

        public DataParser(string filePath, List<string> flightParamaters)
        {
            this.csvRows = System.IO.File.ReadAllLines(filePath);
            this.csvFilePath = filePath;
            this.flightParamaters = flightParamaters;
            extractData();
        }

        public float getDataInTime(string feat, int timeStamp)
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

        public float[] getFeatureData(string feat)
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

        public float[] getFeatureDataInRange(string name, int endingTimeStamp)
        {
            float[] paramData = getFeatureData(name);
            Array.Resize(ref paramData, endingTimeStamp);
            return paramData;

        }

        public List<string> getFeatures()
        {
            return new List<string> { "alieron", "elevator", "rudder", "throttle" };
        }

        private void extractData()
        {
            TextFieldParser parser = new TextFieldParser(this.csvFilePath);
            parser.SetDelimiters(",");
            string[] row;
            int numOfTimeStamps = this.csvRows.Length;

            
            this.alieron = new float[numOfTimeStamps];
            this.elevator = new float[numOfTimeStamps];
            this.rudder = new float[numOfTimeStamps];
            this.throttle = new float[numOfTimeStamps];

            for (int i = 0; i < numOfTimeStamps; i++)
            {
                row = parser.ReadFields();
                this.alieron[i] = float.Parse(row[0]);
                this.elevator[i] = float.Parse(row[1]);
                this.rudder[i] = float.Parse(row[2]);
                this.throttle[i] = float.Parse(row[3]);
            }
        }

        public ChartValues<ScatterPoint> getLast30SecRegLinePoints(string reaserchedFeat, string correlataedFeat)
        {

            int numOfTimeStamps = this.csvRows.Length;
            int start = this.csvRows.Length - 30;

            float[] resFeat = getFeatureData(reaserchedFeat);
            float[] corFeat = getFeatureData(correlataedFeat);


            var cv = new ChartValues<ScatterPoint>();

            for (int i = start; i < numOfTimeStamps; i++)
            {
                cv.Add(new ScatterPoint(resFeat[i], corFeat[i]));
            }
            return cv;
        }

    }
}
