using Election.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Election.Pages
{
    /// <summary>
    /// Interaction logic for WebCamControl.xaml
    /// </summary>
    public partial class WebCamControl : UserControl
    {
        public WebCamControl()
        {
            InitializeComponent();            
        }

        private async void webcam_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is WebcamControlViewModel vm)
            {
                await vm.InitializeWebCam();
            }
        }
    }
}

