using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulator.Model;

namespace FlightSimulator.ViewModel
{
    class DataViewModel : INotifyPropertyChanged
    {
        private DataModel dm;
        public event PropertyChangedEventHandler PropertyChanged;

        public DataViewModel(DataModel dataModel)
        {
            this.dm = dataModel;
            dm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        // Properties
        public string VM_FILE
        {
            get
            {
                return this.dm.File;
            }
            set
            {
                this.dm.File = value;
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

        public void getFile()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".csv";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                this.dm.File = filename;
            }
        }
    }
}
