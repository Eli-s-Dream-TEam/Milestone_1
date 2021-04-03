using System.Windows.Controls;
using FlightSimulator.ViewModel;
using FlightSimulator.Model;

namespace FlightSimulator.View
{
    /// <summary>
    /// Interaction logic for GaugeView.xaml
    /// </summary>
    public partial class GaugeView : UserControl
    {
        private GaugeViewModel gvm;

        public GaugeView()
        {
            InitializeComponent();
            DataModel dm = DataModel.Instance;
            this.gvm = new GaugeViewModel(dm);
            this.DataContext = gvm;
        }

    }
}
