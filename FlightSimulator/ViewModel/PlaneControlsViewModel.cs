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
        public event PropertyChangedEventHandler PropertyChanged;
        

        public PlaneControlsViewModel(DataModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };


            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

            
        }

        public SeriesCollection VM_PlaneControlsGraphSeries
        {
            get { return model.PlaneControlsGraphSeries; }
            set { model.PlaneControlsGraphSeries = value; }
        }

        // Notify handler
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public string[] VM_PlaneControls { 
            get { return this.model.PlaneControls; } 
            set { this.model.PlaneControls = value; } 
        }

        
    }
}
