using System.Windows.Controls;
using FlightSimulator.ViewModel;
using FlightSimulator.Model;

namespace FlightSimulator.View
{

    public partial class AnomaliesView : UserControl
    {
        private AnomaliesViewModel avm;
        public AnomaliesView()
        {
            InitializeComponent();
            DataModel dm = DataModel.Instance;
            this.avm = new AnomaliesViewModel(dm);
            this.DataContext = avm;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.avm.HandleDLLUpload();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem lbi = e.Source as ListBoxItem;

            if (lbi != null)
            {
                // Do the time skip here from `lbi`
            }
        }
    }
}
