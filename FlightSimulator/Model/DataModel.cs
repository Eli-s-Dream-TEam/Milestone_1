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
using LiveCharts.Helpers;

namespace FlightSimulator.Model
{
    class DataModel : INotifyPropertyChanged
    {
        private List<string> attributeList;
        private Boolean stop = true;
        private Boolean pause = false;
        private double playbackMultiplier = 1.0;
        private int playbackSpeed = 100;
        private int timestamp = -1;
        private int prevTimeStamp = 0;
        private int maximumLength = 1000;
        private string ip = "127.0.0.1";
        private int in_port = 5006;
        private int out_port = 5004;
        private string trainFile;
        private string testFile;
        private float alieron;
        private float elevator;
        private float rudder;
        private float throttle;
        private float altitude;
        private float speed;
        private float direction;
        private float roll;
        private float yaw;
        private float pitch;
        private SocketModel in_socket;
        private SocketModel out_socket;
        public event PropertyChangedEventHandler PropertyChanged;
        private static DataModel instance = null;

        private SeriesCollection featUpdatingGraphSeries;
        private SeriesCollection mostCorrGraphSeries;
        private SeriesCollection regLineGraphSeries;
        private bool isGraphsResetted = false;

        private List<string> flightParamters;
        public string researchedParamater;
        private DataParser dp = new DataParser();
        
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
        this.attributeList = XMLParser.DeserializeFromXML();
      }


        // Properties
        public string TrainFile
        {
            get { return this.trainFile; }
            set
            {
                this.trainFile = value;
                this.stop = true;

                if (Timestamp == -1)
                {
                    this.connect();
                    this.dp.learnFlight(this.trainFile, this.attributeList);
                }
                NotifyPropertyChanged("TrainFile");

            }
        }

        public string TestFile
        {
            get { return this.testFile; }
            set
            {

                if (this.testFile != value)
                {

                    this.testFile = value;
                    this.stop = true;
                    this.dp.extractDataFromTestFlight(this.testFile);
                    NotifyPropertyChanged("TestFile");
                }
            }
        }

        public bool Stop
        {
            set
            {
                if (Stop != value && this.trainFile != null)
                {
                    string file = trainFile;
                    if (testFile != null)
                    {
                        file = testFile;
                    }
                    this.stop = value;
                    NotifyPropertyChanged("Stop");
                    start(file);
                }

            }

            get
            {
                return this.stop;
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

        public float Altitude
        {
            get { return this.altitude; }
            set
            {
                if (this.altitude != value)
                {
                    this.altitude = value;
                    NotifyPropertyChanged("Altitude");
                }
            }
        }

        public float Speed
        {
            get { return this.speed; }
            set
            {
                if (this.speed != value)
                {
                    this.speed = value;
                    NotifyPropertyChanged("Speed");
                }
            }
        }

        public float Direction
        {
            get { return this.direction; }
            set
            {
                if (this.direction != value)
                {
                    this.direction = value;
                    NotifyPropertyChanged("Direction");
                }
            }
        }
        public float Roll
        {
            get { return this.roll; }
            set
            {
                if (this.roll != value)
                {
                    this.roll = value;
                    NotifyPropertyChanged("Roll");
                }
            }
        }
        public float Yaw
        {
            get { return this.yaw; }
            set
            {
                if (this.yaw != value)
                {
                    this.yaw = value;
                    NotifyPropertyChanged("Yaw");
                }
            }
        }
        public float Pitch
        {
            get { return this.pitch; }
            set
            {
                if (this.pitch != value)
                {
                    this.pitch = value;
                    NotifyPropertyChanged("Pitch");
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
                if (this.featUpdatingGraphSeries != value)
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
                if (this.flightParamters != value)
                {
                    this.flightParamters = value;
                    NotifyPropertyChanged("FlightParamaters");
                }
            }
        }

        public string ResearchedParamater
        {
            get { return this.researchedParamater; }
            set
            {
                this.researchedParamater = value;
                updateGraphs(true);
                NotifyPropertyChanged("ResearchedParamater");
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
            this.Alieron = float.Parse(parsedLine[getPropertyIndex("aileron")]);
            this.Elevator = float.Parse(parsedLine[getPropertyIndex("elevator")]);
            this.Rudder = float.Parse(parsedLine[getPropertyIndex("rudder")]);
            this.Throttle = float.Parse(parsedLine[getPropertyIndex("throttle")]);
            this.Altitude = float.Parse(parsedLine[getPropertyIndex("altimeter_indicated-altitude-ft")]);
            this.Speed = float.Parse(parsedLine[getPropertyIndex("airspeed-indicator_indicated-speed-kt")]);
            this.Direction = float.Parse(parsedLine[getPropertyIndex("heading-deg")]);
            this.Roll = float.Parse(parsedLine[getPropertyIndex("attitude-indicator_indicated-roll-deg")]);
            this.Pitch = float.Parse(parsedLine[getPropertyIndex("attitude-indicator_indicated-pitch-deg")]);
            this.Yaw = float.Parse(parsedLine[getPropertyIndex("side-slip-deg")]);
        }

        public int getPropertyIndex(string property)
        {
            return attributeList.FindIndex(a => a.Equals(property));
        }


        public void connect()
        {
            try
            {
                out_socket.disconnect();
                out_socket.connect();
                this.Timestamp = 0;
            }

            catch (Exception e)
            {
                Console.Write("Error while starting connection");
            }
        }

        public void start(string file)
        {

            Console.WriteLine(file);

            this.Timestamp = 0;
            
            //reading the csv file.
            string[] lines = System.IO.File.ReadAllLines(file);
            this.FlightParamaters = this.attributeList;

            //check if test flight was loaded.
            if(this.dp.getIsTestFlightLoaded())
            {
                //updating correlated feautres in the test flight data.
                this.dp.integrateCorFeatures();
            }
            
            //the deafult paramter is the first one.
            this.researchedParamater = this.flightParamters[0];
            generateGraphs();
            

            this.MaximumLength = lines.Length;
            NotifyPropertyChanged("MaximumLength");
            new Thread(delegate ()
            {
                while (!stop)
                {
                    updateGraphs();
                    if (timestamp < this.MaximumLength && !pause)
                    {
                        isGraphsResetted = false;
                        this.parseLine(lines[this.Timestamp]);
                        out_socket.send(lines[this.Timestamp] + "\n");
                        this.Timestamp++;
                    }

                    
                    Thread.Sleep((int)(PlaybackSpeed / PlaybackMultiplier));
                }
            }).Start();
        }


        //create an empty line graph for the given paramter.
        private SeriesCollection generateOneParamaterLineGraph(string paramater)
        {
            return new SeriesCollection
                {
                    new LineSeries {
                        Values = new ChartValues<float> { },
                        PointGeometry = null,
                        Fill = System.Windows.Media.Brushes.Transparent,
                        Title = paramater }
                };
        }

        private void generateGraphs()
        {
            string feat = this.researchedParamater;
            string corFeat = dp.getFeatMostCorrFeature(feat);
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                //initiating a blank updating graph for the reaserched flight paramater.
                this.FeatUpdatingGraphSeries = generateOneParamaterLineGraph(feat);

                //initiating a blank updating graph for the most correletad feature flight paramter.
                this.MostCorrGraphSeries = generateOneParamaterLineGraph(corFeat);

                //extracting all data about regression line and displaying the graph as a whole.
                //first LineSeries is the regression line.
                //the second LineSeries is the all the points of the last 30 seconds.

                this.RegLineGraphSeries = new SeriesCollection
                {
                    //regression line.
                    new LineSeries {
                        Title = "Regression Line",
                        Values = new ChartValues<ObservablePoint>(){ },
                        PointGeometry = null,
                        Fill = System.Windows.Media.Brushes.Transparent},
            
                    //last 30 values.
                    new ScatterSeries {
                        Values = new ChartValues<ScatterPoint>() { },
                        PointGeometry = DefaultGeometries.Circle,
                        Title="Last Thirty Seconds Values" }

                };
            });

        }


        private void updateGraphs(bool isParamaterHaveChanged = false)
        {
            //end of time line, do nothing.
            if(this.timestamp == this.maximumLength) { return; }

            //check for reset button situation.
            if (timestamp == 0 && pause && !isGraphsResetted)
            {
                generateGraphs();
                isGraphsResetted = true;
            }

            string feat = this.researchedParamater;
            string corFeat = dp.getFeatMostCorrFeature(feat);

            if(isParamaterHaveChanged)
            {
                paramChangedGraphsUpdate(feat,corFeat);
                return;
            }
   
            //check for pause, if paused than no updated needed.
            if (!this.pause)
            {
                //check for time skip in the playback controller.
                if (this.timestamp - this.prevTimeStamp == 1 && this.timestamp != 0)
                {
                    addNextValueToFeatAndCorGraphs(feat, corFeat);

                    //in case the graph is not yet presented.
                    if (this.RegLineGraphSeries[0].Values.Count == 0)
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(() =>
                            this.RegLineGraphSeries[0].Values = dp.getRegLineValues(feat, corFeat, this.timestamp)
                        );
                    }

                    //removing points not in 30 seconds range.
                    removeOutOfRangePoints();

                    //adding the next(feat, corFeat) point value the the scatter point graph display.
                    addPointToRegLineGraph(feat, corFeat);
                }
                else
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        featAndCorGraphssTimeSkipUpdate(feat,corFeat);
                        regLineGraphUpdate();
                    });
                }
                //updating the prevtime variable to hold the last timestamp.
                this.prevTimeStamp = this.timestamp;
            } 
        }

        private void paramChangedGraphsUpdate(string feat, string corFeat)
        {
            //in case that the researched paramater have changed.
            System.Windows.Application.Current.Dispatcher.Invoke(() =>           
            {
                //generating the upper graphs:
                this.FeatUpdatingGraphSeries = generateOneParamaterLineGraph(feat);
                this.MostCorrGraphSeries = generateOneParamaterLineGraph(corFeat);

                //updating the graphs.
                featAndCorGraphssTimeSkipUpdate(feat, corFeat);
                this.RegLineGraphSeries[0].Values = dp.getRegLineValues(feat, corFeat, this.timestamp);
                regLineGraphUpdate();
            });
        }

        private void addNextValueToFeatAndCorGraphs(string feat, string corFeat)
        {
            this.FeatUpdatingGraphSeries[0].Values.Add(dp.getDataInTime(feat, this.timestamp));
            this.MostCorrGraphSeries[0].Values.Add(dp.getDataInTime(corFeat, this.timestamp));
        }

        private void addPointToRegLineGraph(string feat, string corFeat)
        {
            this.RegLineGraphSeries[1].Values.Add(
                        new ScatterPoint(dp.getDataInTime(feat, this.timestamp)
                                        , dp.getDataInTime(corFeat, this.timestamp))
                    );
        }

        private void featAndCorGraphssTimeSkipUpdate(string feat, string corFeat)
        {
            //getting the data until the current time stamp.
            var resParam = dp.getFeatureDataInRange(feat, this.timestamp);
            var corParam = dp.getFeatureDataInRange(corFeat, this.timestamp);
            this.FeatUpdatingGraphSeries[0].Values = resParam.ToList().AsChartValues();
            this.MostCorrGraphSeries[0].Values = corParam.ToList().AsChartValues();
        }

        //removing points that are not in the last 30 seconds.
        private void removeOutOfRangePoints()
        {
            if (timestamp > 200 && this.RegLineGraphSeries[1].Values.Count != 0)
            {
                this.RegLineGraphSeries[1].Values.RemoveAt(0);
            }
        }

        //had a problem with deleting and updating the reline graph. this is the solution.
        private void regLineGraphUpdate()
        {
            string feat = this.researchedParamater;
            string corFeat = dp.getFeatMostCorrFeature(feat);

            while (RegLineGraphSeries[1].Values.Count > 0)
            {
                RegLineGraphSeries[1].Values.RemoveAt(0);
            }
            NotifyPropertyChanged("RegLineGraphSeries");


            this.RegLineGraphSeries[1].Values.InsertRange(0, dp.getLast30SecRegLinePoints(feat, corFeat, this.timestamp));
        }


        // Notify handler
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

    }

}

