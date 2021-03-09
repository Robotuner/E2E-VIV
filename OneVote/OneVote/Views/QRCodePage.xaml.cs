using OneVote.Models;
using OneVote.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

namespace OneVote.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRCodePage : ContentPage
    {
        public string QRText { get; set; }
        public Func<string, Task> OnSubmitBallot {get;set;}
        public QRCodePage()
        {
            InitializeComponent();
        }

        private string SSN { get; set; }


        public async void scanView_OnScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {                
                QRText = result.Text;
                (Guid electionId, string registration, int birthYear, Guid ballotId) = Utils.DisectQR(QRText, SSN);
                string msg = string.Format(@"{0}\n{1}\n{2}", electionId, registration, birthYear);
                scanView.IsScanning = electionId == Guid.Empty;
                if (!scanView.IsScanning)
                {
                    DataService.QRText = result.Text;
                    await DisplayAlert("Scanned result", msg, "OK");
                    await Navigation.PopModalAsync();
                    await OnSubmitBallot?.Invoke(QRText);
                }
                else
                {
                    DataService.QRText = null;
                    Debug.WriteLine("Repeating scann");
                }
            });
            await Task.CompletedTask;
        }


    }
}