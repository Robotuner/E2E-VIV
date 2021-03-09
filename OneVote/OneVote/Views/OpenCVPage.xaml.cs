using OneVote.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace OneVote.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenCVPage : ContentPage
    {
        public OpenCVPage()
        {
            InitializeComponent();
        }

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            if (this.BindingContext is OpenCVPageViewModel vm)
            {
                await vm.StartWebCam();
            }
        }
    }
}