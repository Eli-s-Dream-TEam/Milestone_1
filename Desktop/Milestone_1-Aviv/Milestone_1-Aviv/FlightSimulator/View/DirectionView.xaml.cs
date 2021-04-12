using System.Windows.Controls;
using FlightSimulator.ViewModel;
using FlightSimulator.Model;

namespace FlightSimulator.View
{
    /// <summary>
    /// Interaction logic for DirectionView.xaml
    /// </summary>
    public partial class DirectionView : UserControl
    {
        private DirectionViewModel dvm;
        public DirectionView()
        {
            InitializeComponent();
            DataModel dm = DataModel.Instance;
            this.dvm = new DirectionViewModel(dm);
            DataContext = dvm;
        }
    }
}
