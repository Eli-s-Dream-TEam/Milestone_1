using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using FlightSimulator.Helper;
using FlightSimulator.Model;

namespace FlightSimulator.ViewModel
{
    public class DataObject
    {
        public int Start { get; set;  }
        public int End { get; set; }
        public string Description { get; set; }

    }
    class AnomaliesViewModel : INotifyPropertyChanged
    {
        private DataModel model;
        private ObservableCollection<DataObject> datalist;
        private DLLModel dll_model;
        public event PropertyChangedEventHandler PropertyChanged;


        public AnomaliesViewModel(DataModel model)
        {
            this.model = model;
            this.dll_model = new DLLModel();

            this.datalist = new ObservableCollection<DataObject>();

            dll_model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Anomalies")
                {
                    datalist.Clear();

                    foreach (var entry in this.dll_model.Anomalies)
                    {
                        datalist.Add(new DataObject() { Start = entry.Item1.Item1, End = entry.Item1.Item2, Description = entry.Item2 });
                    }

                    NotifyPropertyChanged("VM_DataList");
                }
            };

        }

        public ObservableCollection<DataObject> VM_DataList {
            get { return this.datalist; }
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


        public int Timestamp
        {
            set
            {
                // Assert timestamp in bounds
                if (value <= 0 ||value > this.model.MaximumLength - 1)
                {
                    return;
                }

                // Set timestamp
                this.model.Timestamp = value;
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
