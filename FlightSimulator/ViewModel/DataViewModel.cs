using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulator.Model;
using System.IO;
using System.Windows;

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
        public string VM_TrainFILE
        {
            get
            {
                return this.dm.TrainFile;
            }
            set
            {
                this.dm.TrainFile = value;
                NotifyPropertyChanged("VM_TrainFILE");
            }
        }

        public string VM_TestFILE
        {
            get
            {
                return this.dm.TestFile;
            }
            set
            {
                this.dm.TestFile = value;
                NotifyPropertyChanged("VM_TestFILE");
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
                // Get file name
                string filename = dlg.FileName;

                // Read first line
                string firstLine = File.ReadLines(filename).First();

                // Split line by csv delimiter
                string[] splitLine = firstLine.Split(',');

                float f; // ignore

                // Check if first line is strings - if it is, wrong flight format.
                if (!float.TryParse(splitLine[0], out f))
                {
                    MessageBox.Show("We add the headers automatically for you, please upload only the raw data", "Error");
                    return;
                }
                
                // Open document 
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
