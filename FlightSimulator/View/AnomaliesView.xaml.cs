using System.Windows.Controls;
using FlightSimulator.ViewModel;
using FlightSimulator.Model;
using System.Data;
using System;
using System.Collections;

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


        private void dataGrid1_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            if (dg == null || dg.SelectedCells == null)
            {
                return;
            }

            DataGridCellInfo dgci = dg.SelectedCells[0];

            var cellContent = dgci.Column.GetCellContent(dgci.Item);

            if (cellContent != null)
            {
                DataObject item = (DataObject) cellContent.DataContext;
                int timestamp = item.Start;

                avm.Timestamp = timestamp;
            }
        }
    }
}
