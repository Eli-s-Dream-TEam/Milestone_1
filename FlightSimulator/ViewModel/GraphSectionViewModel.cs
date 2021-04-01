using FlightSimulator.Model;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModel
{
    class GraphSectionViewModel : INotifyPropertyChanged
    {
        private DataModel model;
        public event PropertyChangedEventHandler PropertyChanged;

        public GraphSectionViewModel(DataModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        private void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        //properties
        public SeriesCollection VM_FeatUpdatingGraphSeries {
            get { return model.FeatUpdatingGraphSeries; }
            set { model.FeatUpdatingGraphSeries = value; } 
        }

        public SeriesCollection VM_MostCorrGraphSeries {
            get { return model.MostCorrGraphSeries; }
            set { model.MostCorrGraphSeries = value; }
        }
        public SeriesCollection VM_RegLineGraphSeries {
            get { return model.RegLineGraphSeries; }
            set { model.RegLineGraphSeries = value; }
        }

        public List<string> VM_FlightParamaters
        { 
            get { return model.FlightParamaters; } 
            set { model.FlightParamaters = value; } 
        }

        public int VM_Timestamp
        {
            get { return model.Timestamp; }
            set { model.Timestamp = value; }
        }


        //methods
        internal void changeResearchedParam(string newResearchedParam)
        {
            this.model.ResearchedParamater = newResearchedParam;
        }

    }
}
