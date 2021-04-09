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
                return this.dm.TrainFile;
            }
            set
            {
                this.dm.TrainFile = value;
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

        public void getFile(string file)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".csv";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
              
                // Open document 
                string filename = dlg.FileName;
                if (file == "train")
                {
                    this.dm.TrainFile = filename;
                }
                else
                {
                    this.dm.TestFile = filename;
                }
            }
        }

        public void start()
        {
            if (dm.TrainFile != null || dm.TestFile != null)
            {
                this.dm.Stop = false;
            }
        }
    }
}
