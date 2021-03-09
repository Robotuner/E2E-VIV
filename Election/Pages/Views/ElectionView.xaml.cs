using Election.Services;
using Election.ViewModels.Views;
using Newtonsoft.Json;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Election.Pages
{
    /// <summary>
    /// Interaction logic for ElectionView.xaml
    /// </summary>
    public partial class ElectionView : UserControl
    {
        public ElectionView()
        {
            InitializeComponent();
        }

        protected override async void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            if (this.DataContext is ElectionViewModel vm)
            {
                DataService.Election = null;
                await vm.LoadData();
            }
        }

        private void AddButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is ElectionViewModel vm)
            {
                int nextNdx = this.catListBox.Items == null || this.catListBox.Items.Count == 0 ? 0 : this.catListBox.SelectedIndex;
                this.catListBox.SelectedIndex = vm.AddCategoryViewModel(nextNdx) + 1;
            }
        }

        private void RemoveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.catListBox.SelectedIndex < 0)
                return;

            if (DataContext is ElectionViewModel vm)
            {
                this.catListBox.SelectedIndex = vm.RemoveCategoryViewModel(this.catListBox.SelectedIndex);
            }
        }

        private void UpButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.catListBox.SelectedIndex < 0)
                return;

            int selectedNdx = this.catListBox.SelectedIndex;
            if (selectedNdx == 0)
                return;

            if (DataContext is ElectionViewModel vm)
            {
                this.catListBox.SelectedIndex = vm.MoveUp(selectedNdx);
            }
        }

        private void DnButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.catListBox.SelectedIndex < 0)
                return;

            int selectedNdx = this.catListBox.SelectedIndex;
            if (selectedNdx == this.catListBox.Items.Count - 1)
                return;

            if (DataContext is ElectionViewModel vm)
            {
                this.catListBox.SelectedIndex = vm.MoveDown(selectedNdx);
            }
        }

        private async void Elections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is ElectionViewModel vm)
            {                
                var Election = await DataService.InitElection(vm.SelectedElection.Id);
                //var json = JsonConvert.SerializeObject(Election);
 
                vm.InitializeElection(Election);
            }
        }
    }
}
