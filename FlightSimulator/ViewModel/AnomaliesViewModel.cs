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
        private List<string> descriptions;
        public event PropertyChangedEventHandler PropertyChanged;


        public AnomaliesViewModel(DataModel model)
        {
            this.model = model;
            this.dll_model = new DLLModel();

            dll_model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Anomalies")
                {
                    this.descriptions = this.dll_model.Anomalies.Select((x) => x.Item2).ToList();
                    NotifyPropertyChanged("VM_Descriptions");
                }
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

            this.model.Pause = true;


            // Abbreviate test & train
            string test = this.model.TestFile;
            string train = this.model.TrainFile;

            if (!File.Exists(test) || !File.Exists(train))
            {
                MessageBox.Show("Train/Test File doesn't exist!", "Error");
                return;
            }

            // Reset descriptions
            if (this.descriptions != null)
            {
                this.descriptions.Clear();
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

            this.model.Pause = false;

        }


        public void Select(int index)
        {

            // Assert Anomalies list
            if (this.dll_model.Anomalies == null)
            {
                return;
            }

            // Assert index in bound
            if (index >= this.dll_model.Anomalies.Count)
            {
                return;
            }

            // Get Item1 (aka timestamp)
            int requestedTimestamp = this.dll_model.Anomalies[index].Item1;

            // Assert timestamp in bounds
            if (requestedTimestamp <= 0 || requestedTimestamp > this.model.Timestamp)
            {
                return;
            }

            // Set timestamp
            this.model.Timestamp = requestedTimestamp;
        }

        public List<string> VM_Descriptions { get { return this.descriptions;  } }


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
