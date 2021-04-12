using System.Windows.Controls;
using FlightSimulator.ViewModel;
using FlightSimulator.Model;

namespace FlightSimulator.View
{
    /// <summary>
    /// Interaction logic for AltitudeView.xaml
    /// </summary>
    public partial class AltitudeView : UserControl
    {
        private GaugeViewModel gvm;
        public AltitudeView()
        {
            InitializeComponent();
            DataModel dm = DataModel.Instance;
            this.gvm = new GaugeViewModel(dm);
            this.DataContext = gvm;
        }
    }
}
