using Election.Services;
using Election.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Election.Pages
{
    /// <summary>
    /// Interaction logic for CreateQRCode.xaml
    /// </summary>
    public partial class CreateQRCode : UserControl
    {
        public CreateQRCode()
        {
            InitializeComponent();
        }

        private async void CreateQRCode_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is CreateQRCodeViewModel vm)
            {
                vm.Elections = await DataService.GetAllElections();
                vm.SelectedElection = vm.Elections.FirstOrDefault();
            }
        }
    }
}
