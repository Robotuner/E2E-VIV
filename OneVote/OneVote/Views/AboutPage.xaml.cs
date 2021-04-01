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
                //string qrtext = "a13acd4a-d415-4b27-afe6-e2310ac71bc6|Test Voter|2003|17LrItgenFpBeX0bpz8eodcorgCnsNWNb3r/2j74WYpR2eYptVmAVhaNLG22bv1Q";
                //await vm.OnQRScanned(qrtext);
            }
        }

        private async void DisplayAlert(string title, string msg)
        {
            await DisplayAlert(title, msg, "OK");
        }
    }
}