using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulator.Model;

namespace FlightSimulator.ViewModel
{
    class TimeControllerViewModel
    {
        private DataModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        public TimeControllerViewModel(DataModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Timestamp")
                {
                    Console.WriteLine("timestamp: {0}", model.Timestamp);
                }
                NotifyPropertyChanged("VM_" + e.PropertyName);
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

        public int VM_Timestamp
        {
            get {
                return model.Timestamp;
            }
            set
            {
                model.Timestamp = value;
                NotifyPropertyChanged("VM_Timestamp");
            }
        }

        public int VM_MaximumLength
        {
            get { return model.MaximumLength; }
        }

    }
}
