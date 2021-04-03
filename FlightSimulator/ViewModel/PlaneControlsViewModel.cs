using System;
using System.ComponentModel;
using FlightSimulator.Model;
using LiveCharts;
using LiveCharts.Wpf;

namespace FlightSimulator.ViewModel
{
    class PlaneControlsViewModel : INotifyPropertyChanged
    {
        private DataModel model;
        private int TICK_DELAY = 20;
        public event PropertyChangedEventHandler PropertyChanged;
        

        public PlaneControlsViewModel(DataModel model)
        {
            this.model = model;
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Yaw",
                    Values = new ChartValues<float> {}
                },
                new LineSeries
                {
                    Title = "Pitch",
                    Values = new ChartValues<float> {}
                },
                new LineSeries
                {
                    Title = "Roll",
                    Values = new ChartValues<float> {}
                }
            };

            Formatter = value => value.ToString("N");

            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Yaw" && this.model.Timestamp % TICK_DELAY == 0)
                {
                    SeriesCollection[0].Values.Add(this.model.Yaw);
                    SeriesCollection[1].Values.Add(this.model.Pitch);
                    SeriesCollection[2].Values.Add(this.model.Roll);
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                }
            };
        }

        public SeriesCollection SeriesCollection { get; set; }

        public Func<float, string> Formatter { get; set; }
        public float VM_Timestamp
        {
            get
            {
                return this.model.Timestamp;
            }
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
