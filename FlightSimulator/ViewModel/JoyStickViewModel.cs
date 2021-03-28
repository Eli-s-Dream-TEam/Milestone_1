using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulator.Model;

namespace FlightSimulator.ViewModel
{
    class JoyStickViewModel : INotifyPropertyChanged
    {
        private DataModel dm;
        public event PropertyChangedEventHandler PropertyChanged;

        public JoyStickViewModel(DataModel dm)
        {
            this.dm = dm;
            dm.PropertyChanged += delegate  (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public float VM_Alieron
        {
            get
            {
                return this.dm.Alieron;
            }
            set
            {
                this.dm.Alieron = value;
            }
        }

        public float VM_Elevator
        {
            get
            {
                return this.dm.Elevator;
            }
            set
            {
                this.dm.Elevator = value;
            }
        }

        public float VM_Rudder
        {
            get
            {
                return this.dm.Rudder;
            }
            set
            {
                this.dm.Rudder = value;
            }
        }

        public float VM_Throttle
        {
            get
            {
                return this.dm.Throttle;
            }
            set
            {
                this.dm.Throttle = value;
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
