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

    class FlightParamater
    {
        private string name;
        private string displayName;
        private float[] data;
        private string corrFeature;

        public FlightParamater(string name, float[] data)
        {
            this.name = name;
            this.displayName = name;
            this.data = data;
        }

        public FlightParamater(string name, string displayString, float[] data)
        {
            this.name = name;
            this.displayName = displayString;
            this.data = data;
        }

        public string getName() { return this.name; }
        public string getDisplayName() { return this.displayName; }
        public float[] getData() { return data; }
        public string getCorrFeature() { return this.corrFeature; }
        public void setCorrFeature(string name) { this.corrFeature = name; }


    }

    class DataParser : ICSVDataParser
    {
        

        private string csvFilePath;
        private string[] csvRows;
        private List<string> flightParamatersNames;
        private List<FlightParamater> data;

        public DataParser(string filePath, List<string> flightParamaters)
        {
            this.csvRows = System.IO.File.ReadAllLines(filePath);
            this.csvFilePath = filePath;
            this.flightParamatersNames = flightParamaters;
            this.data = new List<FlightParamater>();
            extractData();
            calcCorrFeatures();
        }

        public float getDataInTime(string feat, int timeStamp)
        {
            var featData = getFeatureData(feat);
            return featData[timeStamp];
        }

        public string getFeatMostCorrFeature(string feat)
        {
            int index = this.flightParamatersNames.FindIndex(a => a.Equals(feat));
            return this.data[index].getCorrFeature();
        }

        public float[] getFeatureData(string feat)
        {
            int index = this.flightParamatersNames.FindIndex(a => a.Equals(feat));
            return this.data[index].getData();
        }

        public float[] getFeatureDataInRange(string name, int endingTimeStamp)
        {
            float[] paramData = getFeatureData(name);
            Array.Resize(ref paramData, endingTimeStamp);
            return paramData;
        }

        public List<string> getFeatures()
        {
            return this.flightParamatersNames;
        }


        private void extractData()
        {
            TextFieldParser parser;
            int numOfTimeStamps = this.csvRows.Length;
            int appearnces;
            int paramIndex;

            //creating the Flight Paramater data list.
            foreach (string name in this.flightParamatersNames)
            {
                parser = new TextFieldParser(this.csvFilePath);
                parser.SetDelimiters(",");

                string[] row;
                float[] paramData = new float[numOfTimeStamps];

                
                //dealing with multiple name paramaters.
                appearnces = getNumberOfAppearnces(name);

                paramIndex = this.flightParamatersNames.FindIndex(a => a.Equals(name));
                //updating the index to get the correct one. (multiple paramter case).
                if (appearnces > 0) { paramIndex += appearnces; } 

                for (int i = 0; i < numOfTimeStamps; i++)
                {
                    row = parser.ReadFields();
                    paramData[i] = float.Parse(row[paramIndex]);
                }

                
                FlightParamater fp;
                if (appearnces == 0)
                {
                    fp = new FlightParamater(name, paramData);
                } else
                {
                    fp = new FlightParamater(name, name + (appearnces + 1).ToString(), paramData);
                }
                
                this.data.Add(fp);
            }

    }

        private int getNumberOfAppearnces(string name)
        {
            int numOfAppearnce = 0;
            foreach (var param in data)
            {
                if(name == param.getName())
                {
                    numOfAppearnce++;
                }
            }
            return numOfAppearnce;
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

        private void calcCorrFeatures()
        {
            //vector<string> atts = ts.gettAttributes();
            int len = this.csvRows.Length;

            for (int i = 0; i < this.flightParamatersNames.Count; i++)
            {
                string f1 = this.flightParamatersNames[i];
                float max = 0;
                int jmax = 0;
                float p = 0;
                for (int j = i + 1; j < this.flightParamatersNames.Count; j++)
                {
                    p = Math.Abs(Calculation.pearson(getFeatureData(f1), getFeatureData(flightParamatersNames[j]), len));
                    if (p > max)
                    {
                        max = p;
                        jmax = j;
                    }
                }
                string f2 = this.flightParamatersNames[jmax];
                this.data[i].setCorrFeature(f2);

            }
        }

    }
}
