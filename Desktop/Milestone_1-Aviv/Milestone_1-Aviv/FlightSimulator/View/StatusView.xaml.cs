using System.Windows.Controls;
using FlightSimulator.ViewModel;
using FlightSimulator.Model;


namespace FlightSimulator.View
{
    /// <summary>
    /// Interaction logic for StatusView.xaml
    /// </summary>
    public partial class StatusView : UserControl
    {
        private StatusViewModel svm;
        public StatusView()
        {
            InitializeComponent();
            DataModel dm = DataModel.Instance;
            this.svm = new StatusViewModel(dm);
            this.DataContext = svm;
        }
    }
}
