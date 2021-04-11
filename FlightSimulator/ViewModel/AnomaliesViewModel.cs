using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using FlightSimulator.Model;


namespace FlightSimulator.ViewModel
{
    class AnomaliesViewModel : INotifyPropertyChanged
    {
        private DataModel model;
        private DLLModel dll_model;
        public event PropertyChangedEventHandler PropertyChanged;


        public AnomaliesViewModel(DataModel model)
        {
            this.model = model;
            this.dll_model = new DLLModel();
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

            dll_model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

        }

        public void HandleDLLUpload()
        {

            // Bail if test/train file non existent.
            /*
            if (this.model.TestFile == null || this.model.TrainFile == null)
            {
                MessageBox.Show("Please upload test and train files", "Error");
                return;
            }
            */

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".dll";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                this.dll_model.handleDLLFileUpload(filename, this.model.TrainFile, this.model.TestFile);
            }
        }

        public List<Tuple<int, string>> VM_Anomalies
        {
            // Returns list of only anomaly names 
            get { return this.dll_model.Anomalies; }
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
