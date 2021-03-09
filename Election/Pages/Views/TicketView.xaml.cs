using Election.ViewModels.Views;
using System;
using System.Windows;
using System.Windows.Controls;
namespace Election.Pages.Views
{
    /// <summary>
    /// Interaction logic for TicketView.xaml
    /// </summary>
    public partial class TicketView : UserControl
    {
        public TicketView()
        {
            InitializeComponent();
        }

        private void TicketView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext is TicketViewModel vm)
            {
                vm.LoadData();
                if (vm.GetCategoryType == null)
                {
                    this.legslativePositions.Visibility = Visibility.Collapsed;
                }
                else
                {
                    this.legslativePositions.Visibility = vm.GetCategoryType.Invoke() == ElectionModels.CategoryTypeEnum.legislative ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }
    }
}
