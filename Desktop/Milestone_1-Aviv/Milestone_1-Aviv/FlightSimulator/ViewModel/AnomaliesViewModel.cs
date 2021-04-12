using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using FlightSimulator.Model;
using FlightSimulator.Helper;
using System.IO;

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

            if (this.model.TestFile == null || this.model.TrainFile == null)
            {
                MessageBox.Show("Please upload test and train files", "Error");
                return;
            }


            // Abbreviate test & train
            string test = this.model.TestFile;
            string train = this.model.TrainFile;

            if (!File.Exists(test) || !File.Exists(train))
            {
                MessageBox.Show("Train/Test File doesn't exist!", "Error");
                return;
            }


            // Convert test & train with headers (using xml file)

            // Get headers from XML, parse into line seperated by `,`
            List<string> headers = XMLParser.DeserializeFromXML();
            string headersLine = string.Join(",", headers);

            // Add to List<string>, append all of the csv into that list
            List<string> testLines = new List<string>() { headersLine };
            List<string> trainLines = new List<string>() { headersLine };
            testLines.AddRange(System.IO.File.ReadAllLines(test));
            trainLines.AddRange(System.IO.File.ReadAllLines(train));

            // Get current directory and intiialize files
            string path = Directory.GetCurrentDirectory();
            string testPath = path + @"\temptest.csv";
            string trainPath = path + @"\temptrain.csv";
            File.WriteAllLines(testPath, testLines);
            File.WriteAllLines(trainPath, trainLines);

            // Get DLL File path
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".dll";

            Nullable<bool> result = dlg.ShowDialog();

            // DLL file selected
            if (result == true)
            {
                string filename = dlg.FileName;
                this.dll_model.handleDLLFileUpload(filename, testPath, trainPath);
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
