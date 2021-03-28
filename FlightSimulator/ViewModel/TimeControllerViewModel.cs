using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulator.Model;

namespace FlightSimulator.ViewModel
{
    class TimeControllerViewModel : INotifyPropertyChanged
    {
        private DataModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        public TimeControllerViewModel(DataModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
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
            get { return model.Timestamp; }
            set { model.Timestamp = value; }
        }

        public int VM_MaximumLength
        {
            get { return model.MaximumLength; }
            set { model.MaximumLength = value; }
        }

    }
}
