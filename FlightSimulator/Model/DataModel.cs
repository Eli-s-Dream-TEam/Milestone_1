using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.ComponentModel;

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
                string[] lines = System.IO.File.ReadAllLines(this.file);
                this.MaximumLength = lines.Length;
                NotifyPropertyChanged("MaximumLength");
                new Thread(delegate ()
                {
                    while (!stop)
                    {
                        if (timestamp < this.MaximumLength && !pause)
                        {
                          
                            this.parseLine(lines[this.Timestamp]);
                            out_socket.send(lines[this.Timestamp] + "\n");
                            this.Timestamp++;
                        }

                        Thread.Sleep((int) (PlaybackSpeed / PlaybackMultiplier));
                    }
                }).Start();
            } 
            catch (Exception e)
            {
                Console.Write("Error while starting connection");
            }
            
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
