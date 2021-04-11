using System;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading;

namespace FlightSimulator.Model
{
    class DLLModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private List<Tuple<int, string>> anomalies;
       public DLLModel() { }

        public void handleDLLFileUpload(string dll, string train, string test)
        {
            // Local train & test with headers for now
            string ttrain = "C:/Users/grano/Desktop/Milestone_1/FlightSimulator/public/reg_flight.csv";
            string ttest = "C:/Users/grano/Desktop/Milestone_1/FlightSimulator/public/anomaly_flight.csv";
            //RegDll Metohds

            // Loading the dll
            Console.WriteLine("Before loading file");


            new Thread(delegate ()
            {
                try
                {
                    var assembly = Assembly.LoadFile(dll);
                    Console.WriteLine("Heyo");
                    Type[] types = assembly.GetTypes();

                    foreach (Type t in types)
                    {
                        Console.WriteLine(t.ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("test");
                    Console.WriteLine(e.ToString());
                }
                Console.WriteLine("After loading file");
            }).Start();
            
            //

            // Load anomalies into this.anomalies (pairs of <int,string>)

            // Uncomment next line to notify when you entered anomalies
            // NotifiyPropertyChanged("Anomalies")
        }

        public List<Tuple<int, string>> Anomalies
        {
            get { return this.anomalies; }
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
