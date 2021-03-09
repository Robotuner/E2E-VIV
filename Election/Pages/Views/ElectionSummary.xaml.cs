using Election.ViewModels.Views;
using System.Windows;
using System.Windows.Controls;

namespace Election.Pages.Views
{
    /// <summary>
    /// Interaction logic for ElectionSummary.xaml
    /// </summary>
    public partial class ElectionSummary : UserControl
    {
        public ElectionSummary()
        {
            InitializeComponent();
        }

        private void CategoryTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.DataContext is ElectionSummaryViewModel vm)
            {
                vm.InitCategories();
            }
        }

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.DataContext is ElectionSummaryViewModel vm)
            {
                vm.InitContestResults();
            }
        }

        private async void Elections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.DataContext is ElectionSummaryViewModel vm)
            {
                await vm.InitElection();
            }
        }

        private async void ElectionSummary_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is ElectionSummaryViewModel vm)
            {
                await vm.OnLoaded();
            }
        }
    }
}
