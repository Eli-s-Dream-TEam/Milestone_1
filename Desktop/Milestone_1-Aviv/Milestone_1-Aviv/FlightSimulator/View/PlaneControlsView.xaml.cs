using System.Windows.Controls;
using FlightSimulator.ViewModel;
using FlightSimulator.Model;

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
        }
    }
}
