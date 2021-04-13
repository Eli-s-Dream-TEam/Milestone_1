using System.Windows.Controls;
using FlightSimulator.ViewModel;
using FlightSimulator.Model;
using LiveCharts;
using System;

namespace FlightSimulator.View
{
    /// <summary>
    /// Interaction logic for PlaneControlsView.xaml
    /// </summary>
    public partial class PlaneControlsView : UserControl
    {
        private PlaneControlsViewModel cvm;
        public PlaneControlsView()
        {
            InitializeComponent();
            DataModel dm = DataModel.Instance;
            this.cvm = new PlaneControlsViewModel(dm);
            DataContext = cvm;

            Func<double, string> format = (x) => String.Format("{0:0.00}", x);
            planecontrols.LabelFormatter = format;

        }
    }
}
