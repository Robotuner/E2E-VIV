using OneVote.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneVote.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReviewBallotChoices : ContentPage
    {
        public ReviewBallotChoices()
        {
            ReviewBallotChoicesViewModel vm = new ReviewBallotChoicesViewModel();

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (this.BindingContext is ReviewBallotChoicesViewModel vm)
            {
                vm.Init();
            }
        }

        private void CategoryTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.BindingContext is ReviewBallotChoicesViewModel vm)
            {
                vm.RefreshCategories();
            }
        }

        private async void ApproveBallotClicked(object sender, EventArgs e)
        {
            if (this.BindingContext is ReviewBallotChoicesViewModel vm)
            {
                bool result = await DisplayAlert(Resource.ReviewBallotAlertTitle, Resource.ReviewBallotAlertMsg, "OK", "Cancel");
                vm.SetApprovedState?.Invoke(result);
                await Navigation.PopModalAsync();
            }
        }

        private async void CloseBallotClicked(object sender, EventArgs e)
        {
            if (this.BindingContext is ReviewBallotChoicesViewModel vm)
            {
                vm.SetApprovedState?.Invoke(false);
                await Navigation.PopModalAsync();
            }
        }
    }
}