using Election.ViewModels;
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

namespace Election.Pages
{
    /// <summary>
    /// Interaction logic for WebCamPage.xaml
    /// </summary>
    public partial class WebCamPage : UserControl
    {
        public WebCamPage()
        {
            InitializeComponent();
        }

        private void WebCamPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is WebCamPageViewModel vm)
            {
                vm.GetImageControl = GetImageControl;
                vm.OnLoaded(null);
            }
        }

        private Image GetImageControl()
        {
            return this.imageControl;
        }
    }
}
