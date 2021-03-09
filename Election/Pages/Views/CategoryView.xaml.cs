using Election.ViewModels.Views;
using ElectionModels;
using System.Windows;
using System.Windows.Controls;

namespace Election.Pages.Views
{
    /// <summary>
    /// Interaction logic for CategoryView.xaml
    /// </summary>
    public partial class CategoryView : UserControl
    {
        public CategoryView()
        {
            InitializeComponent();
        }

        private void CategoryView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ResetVisibility();
            if (DataContext is CategoryViewModel vm)
            {
                vm.RefreshTickets();
            }
        }

        private void ResetVisibility()
        {
            if (DataContext is CategoryViewModel vm)
            {
                vm.RefreshSelectedCategory();
                judicial1.Visibility = Visibility.Collapsed;
                judicial2.Visibility = Visibility.Collapsed;
                subtitle1.Visibility = Visibility.Visible;
                subtitle2.Visibility = Visibility.Visible;
                info1.Visibility = Visibility.Visible;
                info2.Visibility = Visibility.Visible;
                title1.Visibility = Visibility.Visible;
                title2.Visibility = Visibility.Visible;
                switch ((CategoryTypeEnum)vm.CategoryTypeId)
                {
                    case CategoryTypeEnum.measure:
                        break;
                    case CategoryTypeEnum.federal:
                        subtitle1.Visibility = Visibility.Collapsed;
                        subtitle2.Visibility = Visibility.Collapsed;
                        info1.Visibility = Visibility.Collapsed;
                        info2.Visibility = Visibility.Collapsed;
                        break;
                    case CategoryTypeEnum.state:
                    case CategoryTypeEnum.legislative:
                        subtitle1.Visibility = Visibility.Collapsed;
                        subtitle2.Visibility = Visibility.Collapsed;
                        info1.Visibility = Visibility.Collapsed;
                        info2.Visibility = Visibility.Collapsed;
                        break;
                    case CategoryTypeEnum.judicial:
                        subtitle1.Visibility = Visibility.Collapsed;
                        subtitle2.Visibility = Visibility.Collapsed;
                        info1.Visibility = Visibility.Collapsed;
                        info2.Visibility = Visibility.Collapsed;
                        title1.Visibility = Visibility.Collapsed;
                        title2.Visibility = Visibility.Collapsed;
                        judicial1.Visibility = Visibility.Visible;
                        judicial2.Visibility = Visibility.Visible;
                        break;
                }
            }
        }
    }
}
