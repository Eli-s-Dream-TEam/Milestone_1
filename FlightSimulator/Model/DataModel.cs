using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.ComponentModel;
using LiveCharts;
using FlightSimulator.Helper;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace FlightSimulator.Model
{
    class DataModel : INotifyPropertyChanged
    {
        private Boolean stop = false;
        private Boolean pause = false;
        private double playbackMultiplier = 1.0;
        private int playbackSpeed = 100;
        private int timestamp = 0;
        private int maximumLength = 1000;
        private string ip = "127.0.0.1";
        private int in_port = 5006;
        private int out_port = 5004;
        private string file;
        private float alieron;
        private float elevator;
        private float rudder;
        private float throttle;
        private SocketModel in_socket;
        private SocketModel out_socket;
        public event PropertyChangedEventHandler PropertyChanged;
        private static DataModel instance = null;

        private SeriesCollection featUpdatingGraphSeries;
        private SeriesCollection mostCorrGraphSeries;
        private SeriesCollection regLineGraphSeries;

        private List<string> flightParamters;

        private string researchedParamater;

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //for testing purposes.
        private DataParser dp;
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@



        /**
         * Implementing Singleton design pattern so we can reference the same DataModel 
         * Object across our views.
         **/
        public static DataModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataModel();
                }

                return instance;
            }
        }

      private DataModel()
      {
        this.in_socket = new SocketModel(ip, in_port);
        this.out_socket = new SocketModel(ip, out_port);
      }

   
        // Properties
        public string File {
           get { return this.file; }
           set
            {
                if (this.file != value)
                {
                    this.file = value;
                    this.start();
                    NotifyPropertyChanged("File");
                }
            }
        }

        public float Alieron
        {
           get { return this.alieron; }
           set
            {
                if (this.alieron != value)
                {
                    this.alieron = value;
                    NotifyPropertyChanged("Alieron");
                }
            }
        }

        public float Elevator
        {
            get { return this.elevator; }
            set
            {
                if (this.elevator != value)
                {
                    this.elevator = value;
                    NotifyPropertyChanged("Elevator");
                }
            }
        }


        public float Rudder
        {
            get { return this.rudder; }
            set
            {
                if (this.rudder != value)
                {
                    this.rudder = value;
                    NotifyPropertyChanged("Rudder");
                }
            }
        }

        public float Throttle
        {
            get { return this.throttle; }
            set
            {
                if (this.throttle != value)
                {
                    this.throttle = value;
                    NotifyPropertyChanged("Throttle");
                }
            }
        }

        public int Timestamp
        {
            get { return this.timestamp; }
            set
            {
                if (this.timestamp != value)
                {
                    this.timestamp = value;
                    NotifyPropertyChanged("Timestamp");
                }
            }
        }

        public int MaximumLength
        {
            get { return this.maximumLength; }
            set
            {
                if (this.maximumLength != value)
                {
                    this.maximumLength = value;
                    NotifyPropertyChanged("MaximumLength");
                }
            }
        }

        public Boolean Pause
        {
            get { return this.pause; }
            set
            {
                if (this.pause != value)
                {
                    this.pause = value;
                    NotifyPropertyChanged("Pause");
                }
            }
        }

        public double PlaybackMultiplier
        {
            get { return this.playbackMultiplier; }
            set
            {
                if (this.playbackMultiplier != value)
                {
                    this.playbackMultiplier = value;
                    NotifyPropertyChanged("PlaybackMultiplier");
                }
            }
        }

        public int PlaybackSpeed
        {
            get { return this.playbackSpeed; }
            set
            {
                if (this.playbackSpeed != value)
                {
                    this.playbackSpeed = value;
                    NotifyPropertyChanged("PlaybackSpeed");
                }
            }
        }

        public SeriesCollection FeatUpdatingGraphSeries
        {
            get { return this.featUpdatingGraphSeries; }
            set 
            { 
                if(this.featUpdatingGraphSeries!=value)
                {
                    this.featUpdatingGraphSeries = value;
                    NotifyPropertyChanged("FeatUpdatingGraphSeries");

                }
            }
        }

        public SeriesCollection MostCorrGraphSeries
        {
            get { return this.mostCorrGraphSeries; }
            set
            {
                if (this.mostCorrGraphSeries != value)
                {
                    this.mostCorrGraphSeries = value;
                    NotifyPropertyChanged("MostCorrGraphSeries");

                }
            }
        }

        public SeriesCollection RegLineGraphSeries
        {
            get { return this.regLineGraphSeries; }
            set
            {
                if (this.regLineGraphSeries != value)
                {
                    this.regLineGraphSeries = value;
                    NotifyPropertyChanged("RegLineGraphSeries");

                }
            }
        }

        public List<string> FlightParamaters
        {
            get { return this.flightParamters; }
            set
            {
                if (this.flightParamters!= value)
                {
                    this.flightParamters= value;
                    NotifyPropertyChanged("FlightParamaters");

                }
            }
        }



        //Methods

        // parse the line from the csv, update needed properties
        public void parseLine(string line)
        {
            if (line == null)
            {
                return;
            }

            string[] parsedLine = line.Split(',');
            this.Alieron = float.Parse(parsedLine[0]);
            this.Elevator = float.Parse(parsedLine[1]);
            this.Rudder = float.Parse(parsedLine[2]);
            this.Throttle = float.Parse(parsedLine[6]);


        }

        public void start()
        {
            try
            {
                

                out_socket.disconnect();
                out_socket.connect();
                this.Timestamp = 0;
                
                //reading the csv file.
                string[] lines = System.IO.File.ReadAllLines(this.file);


                //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                this.dp = new DataParser(this.file);
                //keep the FlightPar. property, chnage the extracting data part.
                //extracting flight paramaters from csv file.
                this.FlightParamaters = dp.getFeatures();
                //the deafult paramter is the first one.
                this.researchedParamater = this.flightParamters[0];
                generateGraphs();
                //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

                this.MaximumLength = lines.Length;
                NotifyPropertyChanged("MaximumLength");
                new Thread(delegate ()
                {
                    while (!stop)
                    {
                        if (timestamp < this.MaximumLength && !pause)
                        {
                            updateGraphs();
                            this.parseLine(lines[this.Timestamp]);
                            out_socket.send(lines[this.Timestamp] + "\n");
                            this.Timestamp++;
                        }

                        Thread.Sleep((int)(PlaybackSpeed / PlaybackMultiplier));
                    }
                }).Start();
            }
            catch (Exception e)
            {
                Console.Write("Error while starting connection");
            }

        }

        private void generateGraphs()
        {
            //initiating a blank updating graph for the reaserched flight paramater.
            this.FeatUpdatingGraphSeries = new SeriesCollection 
            { 
                new LineSeries { Values = new ChartValues<float> { }, PointGeometry = null, Fill = System.Windows.Media.Brushes.Transparent }
            };

            //initiating a blank updating graph for the most correletad feature flight paramter.
            this.MostCorrGraphSeries = new SeriesCollection 
            { 
                new LineSeries { Values = new ChartValues<float> { }, PointGeometry = null, Fill = System.Windows.Media.Brushes.Transparent}
            };

            //extracting all data about regression line and displaying the graph as a whole.
            //first LineSeries is the regression line.
            //the second LineSeries is the all the points of the last 30 seconds.

            var cv2 = new ChartValues<ScatterPoint>();
            cv2.AddRange(dp.getLast30SecRegLinePoints(this.researchedParamater, dp.getFeatMostCorrFeature(this.researchedParamater)));

            this.RegLineGraphSeries = new SeriesCollection
            {
                //regression line.
                new LineSeries {
                    Values = new ChartValues<float> { 5,4,3},
                    PointGeometry = null,
                    Fill = System.Windows.Media.Brushes.Transparent},
            
                //last 30 values.
                new ScatterSeries { Values = cv2, PointGeometry = DefaultGeometries.Circle }
                
            };

        }

        private void updateGraphs()
        {
            string corFeat = dp.getFeatMostCorrFeature(this.researchedParamater);
            this.FeatUpdatingGraphSeries[0].Values.Add(dp.getDataInTime(this.researchedParamater, this.timestamp));
            this.MostCorrGraphSeries[0].Values.Add(dp.getDataInTime(corFeat, this.timestamp));
            //this.regLineGraphSeries[0].Values.Add(dp.getDataInTime(this.researchedParamater, this.timestamp));
        }


        // Notify handler
        public void NotifyPropertyChanged(string propName)
        {
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

    }

}
