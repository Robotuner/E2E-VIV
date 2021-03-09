using Election.ViewModels.Views;
using System.Windows;
using System.Windows.Controls;

namespace Election.Pages.Views
{
    /// <summary>
    /// Interaction logic for TicketsView.xaml
    /// </summary>
    public partial class TicketsView : UserControl
    {
        public TicketsView()
        {
            InitializeComponent();
        }

        private void TicketsView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is CategoryViewModel vm)
            {
                this.ticketlist.SelectedIndex = vm.AddTicketViewModel(this.ticketlist.SelectedIndex) + 1;
            }
        }

        private void RemoveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.ticketlist.SelectedIndex < 0)
                return;

            if (DataContext is CategoryViewModel vm)
            {
                this.ticketlist.SelectedIndex = vm.RemoveTicketViewModel(this.ticketlist.SelectedIndex);
            }
        }

        private void UpButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.ticketlist.SelectedIndex < 0)
                return;

            int selectedNdx = this.ticketlist.SelectedIndex;
            if (selectedNdx == 0)
                return;

            if (DataContext is CategoryViewModel vm)
            {
                this.ticketlist.SelectedIndex = vm.MoveUp(selectedNdx);
            }
        }

        private void DnButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.ticketlist.SelectedIndex < 0)
                return;

            int selectedNdx = this.ticketlist.SelectedIndex;
            if (selectedNdx == this.ticketlist.Items.Count - 1)
                return;

            if (DataContext is CategoryViewModel vm)
            {
                this.ticketlist.SelectedIndex = vm.MoveDown(selectedNdx);
            }
        }
    }
}
