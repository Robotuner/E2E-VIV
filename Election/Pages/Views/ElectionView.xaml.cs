using Election.Services;
using Election.ViewModels.Views;
using ElectionModels;
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
                vm.SetSortButtonVisibility = this.SetSortButtonVisibility;
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

        private void SetSortButtonVisibility(bool value)
        {
            this.legislativeJudiciarySort.Visibility = value ? Visibility.Visible : Visibility.Hidden;
        }

        private void SortButton_Click(Object sender, RoutedEventArgs e)
        {
            if (DataContext is ElectionViewModel vm)
            {
                switch ((CategoryTypeEnum)vm.SelectedCategoryType.Id)
                {
                    case CategoryTypeEnum.legislative:
                        // sorts legislative by district and updates the sequence to reflect the sort.
                        vm.SortLegislative();
                        break;
                    case CategoryTypeEnum.judicial:
                        vm.SortJudicial();
                        break;                   
                }
            }

        }
        private async void Elections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is ElectionViewModel vm)
            {                
                var Election = await DataService.InitElection(vm.SelectedElection.Id);
                vm.InitializeElection(Election);
            }
        }
    }
}
