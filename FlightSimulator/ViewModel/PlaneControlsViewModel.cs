using System;
using System.ComponentModel;
using FlightSimulator.Model;
using LiveCharts;
using LiveCharts.Defaults;
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
                    Values = new ChartValues<ObservablePoint> {},
                    PointGeometry = null,
                    Fill = System.Windows.Media.Brushes.Transparent
                },
                new LineSeries
                {
                    Title = "Pitch",
                    Values = new ChartValues<ObservablePoint> {},
                    PointGeometry = null,
                    Fill = System.Windows.Media.Brushes.Transparent
                },
                new LineSeries
                {
                    Title = "Roll",
                    Values = new ChartValues<ObservablePoint> {},
                    PointGeometry = null,
                    Fill = System.Windows.Media.Brushes.Transparent
                }
            };

            
            Formatter = value => value.ToString("N");

            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Yaw" && this.model.Timestamp % TICK_DELAY == 0)
                {
                    SeriesCollection[0].Values.Add(new ObservablePoint(this.model.Timestamp, this.model.Yaw));
                    SeriesCollection[1].Values.Add(new ObservablePoint(this.model.Timestamp, this.model.Pitch));
                    SeriesCollection[2].Values.Add(new ObservablePoint(this.model.Timestamp, this.model.Roll));
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                }

                // Flight reset
                if (model.Timestamp == 0)
                {
                    SeriesCollection[0].Values.Clear();
                    SeriesCollection[1].Values.Clear();
                    SeriesCollection[2].Values.Clear();
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
