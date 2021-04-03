using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulator.Model;


namespace FlightSimulator.ViewModel
{
    class GaugeViewModel : INotifyPropertyChanged
    {
        private DataModel model;
        public event PropertyChangedEventHandler PropertyChanged;

        public GaugeViewModel(DataModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

        }
        public float VM_Speed
        {
            get
            {
                return this.model.Speed;
            }
            set
            {
                this.model.Speed = value;

            }
        }

        public float VM_Altitude
        {
            get
            {
                return this.model.Altitude;
            }
            set
            {
                this.model.Altitude = value;

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
