using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FlightSimulator.ViewModel;
using FlightSimulator.Model;

namespace FlightSimulator.View
{
    /// <summary>
    /// Interaction logic for JoyStick.xaml
    /// </summary>
    public partial class TimeController : UserControl
    {
        private TimeControllerViewModel vm;
        public TimeController()
        {
            InitializeComponent();
            DataModel dm = DataModel.Instance;
            this.vm = new TimeControllerViewModel(dm);
            DataContext = vm;
        }


    }
}