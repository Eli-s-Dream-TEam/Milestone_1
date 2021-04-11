using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using FlightSimulator.Model;

namespace FlightSimulator.ViewModel
{
    class StatusViewModel : INotifyPropertyChanged
    {
        private DataModel model;
        public event PropertyChangedEventHandler PropertyChanged;

        public StatusViewModel(DataModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {

                switch (e.PropertyName)
                {
                    case "TestFile":
                        this.NotifyPropertyChanged("VM_TestStatus");
                        this.NotifyPropertyChanged("VM_TestForeground");
                        break;
                    case "TrainFile":
                        this.NotifyPropertyChanged("VM_TrainStatus");
                        this.NotifyPropertyChanged("VM_TrainForeground");
                        break;
                    case "Timestamp":
                    case "Pause":
                        this.NotifyPropertyChanged("VM_SimulatorStatus");
                        this.NotifyPropertyChanged("VM_SimulatorForeground");
                        break;
                    default:
                        break;
                }
            };
        }

        // Notify handler
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }

        }

        public string VM_SimulatorStatus
        {
            get
            {
                if (this.model.Pause || this.model.Timestamp <= 0)
                {
                    return "Pending";
                }
                return "Running";
            }
        }

        public SolidColorBrush VM_SimulatorForeground
        {
            get
            {
                if (this.model.Pause || this.model.Timestamp <= 0)
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BB86FC"));
                }
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#03DAC6"));
            }
        }

        public string VM_TrainStatus
        {
            get
            {
                if (this.model.TrainFile == null)
                {
                    return "Pending";
                }
                return "Active";
            }
        }

        public SolidColorBrush VM_TrainForeground
        {
            get
            {
                if (this.model.TrainFile == null)
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BB86FC"));
                }
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#03DAC6"));
            }
        }

        public string VM_TestStatus
        {
            get
            {
                if (this.model.TestFile == null)
                {
                    return "Pending";
                }
                return "Active";
            }
        }

        public SolidColorBrush VM_TestForeground
        {
            get
            {
                if (this.model.TestFile == null)
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BB86FC"));
                }
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#03DAC6"));
            }
        }
    }
}
