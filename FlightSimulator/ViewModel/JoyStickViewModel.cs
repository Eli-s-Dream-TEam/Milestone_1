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
        private int default_top = 15;
        private int default_left = 15;
   
        private DataModel dm;
        public event PropertyChangedEventHandler PropertyChanged;

        public JoyStickViewModel(DataModel dm)
        {
            this.dm = dm;
            dm.PropertyChanged += delegate  (Object sender, PropertyChangedEventArgs e)
            {
                if(e.PropertyName == "Alieron")
                {
                    NotifyPropertyChanged("VM_Left");
                }

                if (e.PropertyName == "Elevator")
                {
                    NotifyPropertyChanged("VM_Top");
                }

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

        public int VM_Top
        {
            get
            {
               return (int)(VM_Elevator * 10) + this.default_top;
            }

           
        }


        public int VM_Left
        {   
            set
            {
                
            }
            get
            {
                return (int)(VM_Alieron * 10) + this.default_left;
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
