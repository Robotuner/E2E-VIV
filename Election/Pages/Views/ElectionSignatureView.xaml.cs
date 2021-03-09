using Election.ViewModels.Views;
using System.Windows.Controls;

namespace Election.Pages.Views
{
    /// <summary>
    /// Interaction logic for ElectionSignatureView.xaml
    /// </summary>
    public partial class ElectionSignatureView : UserControl
    {
        public ElectionSignatureView()
        {
            InitializeComponent();
        }

        private async void Elections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.DataContext is ElectionSignatureViewModel vm)
            {
                await vm.InitElection();
            }
        }

        private async void ElectionSignature_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.DataContext is ElectionSignatureViewModel vm)
            {
                await vm.OnLoaded();
            }
        }

    }
}
