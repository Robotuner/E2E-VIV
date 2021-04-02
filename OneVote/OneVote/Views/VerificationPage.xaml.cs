using OneVote.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneVote.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerificationPage : ContentPage
    {
        public VerificationPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            if (BindingContext is VerificationPageViewModel vm)
            {
                vm.INav = Navigation;
                await vm.Init();
            }
        }

    }
}