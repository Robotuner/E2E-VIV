using OneVote.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneVote.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubmitAuthorizationPage : ContentPage
    {
        public Action<string> OnSubmitOKClicked { get; set; }


        public SubmitAuthorizationPage()
        {
            InitializeComponent();
        }

        private async void OKButtonClicked(object sender, EventArgs e)
        {
            string ssn = null;

            if (this.BindingContext is SubmitAuthorizationPageViewModel sapvm)
            {
                ssn = sapvm.SSN;  
            }
            await Navigation.PopModalAsync();
            OnSubmitOKClicked?.Invoke(ssn);
        }

        private async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void KeyboardButtonClicked(object sender, EventArgs e)
        {
            if (BindingContext is SubmitAuthorizationPageViewModel vm)
            {
                vm.Keyboard = vm.Keyboard == "Numeric" ? "Default" : "Numeric";                
            }
        }
    }
}