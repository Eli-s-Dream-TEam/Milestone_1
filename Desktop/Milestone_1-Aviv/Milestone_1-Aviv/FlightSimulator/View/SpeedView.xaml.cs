using System.Windows.Controls;
using FlightSimulator.ViewModel;
using FlightSimulator.Model;

namespace FlightSimulator.View
{
    /// <summary>
    /// Interaction logic for SpeedView.xaml
    /// </summary>
    public partial class SpeedView : UserControl
    {
        private GaugeViewModel gvm;
        public SpeedView()
        {
            InitializeComponent();
            DataModel dm = DataModel.Instance;
            this.gvm = new GaugeViewModel(dm);
            this.DataContext = gvm;
        }
    }
}
