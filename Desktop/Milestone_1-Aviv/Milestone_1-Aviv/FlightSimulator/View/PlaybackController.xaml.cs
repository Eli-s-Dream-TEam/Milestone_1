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
    /// Interaction logic for PlaybackController.xaml
    /// </summary>
    public partial class PlaybackController : UserControl
    {
        private PlaybackControllerViewModel vm;
        public PlaybackController()
        {
            InitializeComponent();
            DataModel dm = DataModel.Instance;
            this.vm = new PlaybackControllerViewModel(dm);
            DataContext = vm;
        }

        private void HandleSkipStart(object sender, MouseButtonEventArgs e)
        {
            this.vm.ResetVideo();
        }

        private void HandleReduceSpeed(object sender, MouseButtonEventArgs e)
        {
            this.vm.ReduceSpeed();
        }

        private void HandleResume(object sender, MouseButtonEventArgs e)
        {
            this.vm.ResumeVideo();
        }

        private void HandlePause(object sender, MouseButtonEventArgs e)
        {
            this.vm.PauseVideo();
        }

        private void HandleStop(object sender, MouseButtonEventArgs e)
        {
            this.vm.ResetVideo();
        }

        private void HandleIncreaseSpeed(object sender, MouseButtonEventArgs e)
        {
            this.vm.IncreaseSpeed();
        }

        private void HandleSkipEnd(object sender, MouseButtonEventArgs e)
        {
            this.vm.FinishVideo();
        }
    }
}
