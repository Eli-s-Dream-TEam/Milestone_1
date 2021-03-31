﻿using LiveCharts;
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
        private string filePath;
        private string[] data;

        public DataParser(string filePath)
        {
            this.data = System.IO.File.ReadAllLines(filePath);
            this.filePath = filePath;
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

        public List<string> getFeatures()
        {
            return new List<string> { "alieron", "elevator", "rudder", "throttle" };
        }

        private void extractData()
        {
            TextFieldParser parser = new TextFieldParser(this.filePath);
            parser.SetDelimiters(",");
            string[] row;
            int numOfTimeStamps = this.data.Length;

            
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

            int numOfTimeStamps = this.data.Length;
            int start = this.data.Length - 30;

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