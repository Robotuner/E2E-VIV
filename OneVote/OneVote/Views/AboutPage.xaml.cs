using OneVote.ViewModels;
using Xamarin.Forms;

namespace OneVote.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (this.BindingContext is AboutViewModel vm)
            {
                vm.DisplayAlert = this.DisplayAlert;
                vm.OnAppearing();
            }
        }

        private async void scanQRButton_Clicked(object sender, System.EventArgs e)
        {
            if (this.BindingContext is AboutViewModel vm)
            {
                QRCodePage qrPage = new QRCodePage();
                qrPage.OnSubmitBallot = vm.OnQRScanned;
                await Navigation.PushModalAsync(qrPage);
            }
        }

        private async void DisplayAlert(string title, string msg)
        {
            await DisplayAlert(title, msg, "OK");
        }
    }
}