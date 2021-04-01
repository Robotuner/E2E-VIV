using ElectionModels;
using OneVote.Models;
using OneVote.Services;
using OneVote.ViewModels;
using Plugin.Permissions;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OneVote.Views
{
    public partial class ItemsPage : ContentPage
    {
        public ItemsPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<ItemsViewModel>(this, MessagingEvents.BadSSN, (s) =>
            {
                DisplayAlert("Error", "Probably bad SSN or Account Authorization Code", "OK");
            });
        }


        protected override void OnAppearing()
        {
            try
            {
                if (this.BindingContext is ItemsViewModel vm)
                {
                    vm.SubmittalConfirmation = SubmittalConfirmation;
                    vm.ErrorMessage = ErrorMessage;
                    vm.ErrorMessage2 = ErrorMessage2;
  
                    base.OnAppearing();
                    vm.OnAppearing();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void ReviewBallot_Clicked(object sender, System.EventArgs e)
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                var status = await CrossPermissions.Current.RequestPermissionAsync<PhonePermission>();
                if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    status = await CrossPermissions.Current.RequestPermissionAsync<PhonePermission>();
                }
            }
            //string deviceId = DependencyService.Get<IPhoneDevice>().GetIdentifier();
            //string ans = Utils.GetUniqueId();
            //if (ans == null)
            //{
            //    Utils.SetUniqueId(Guid.NewGuid());
            //}
            if (this.BindingContext is ItemsViewModel vm)
            {
                if (vm.isSubmitting)
                    return;

                if (vm.BallotIsFilled())
                {
                    ReviewBallotChoices rbc = new ReviewBallotChoices();
                    (rbc.BindingContext as ReviewBallotChoicesViewModel).SetApprovedState = vm.SetApprovedState;
                    await Navigation.PushModalAsync(rbc);
                }
                vm.isSubmitting = false;
            }
        }

        private async void SubmitBallot_Clicked(object sender, System.EventArgs e)
        {
            if (this.BindingContext is ItemsViewModel vm)
            {
                if (string.IsNullOrEmpty(vm.SSN))
                {
                    SubmitAuthorizationPage sap = new SubmitAuthorizationPage();
                    sap.OnSubmitOKClicked = SubmitBallotClickOK;
                    await Navigation.PushModalAsync(sap);

                }
                else
                {
                    await vm.OnSubmitBallot();
                }
            }
        }

        private async void SubmitBallotClickOK(string ssn)
        {
            if (string.IsNullOrEmpty(ssn))
                return;

            if (this.BindingContext is ItemsViewModel vm)
            {
                vm.SSN = ssn;
                await vm.OnSubmitBallot();
            }
        }

        private async void SubmittalConfirmation(Signature signature)
        {
            string msg = null;
            if (signature != null && signature.Id != Guid.Empty)
            {
                msg = string.Format(Resource.ItemsPageSubmittalConfirmation,signature.Name, signature.Id);

                if (Models.Utils.BallotHasBeenSubmitted(ballotId: signature.BallotId, save: true) && !DataService.Election.AllowUpdates)
                {
                    DataService.ClearVotes();
                }
            }
            else
            {
                msg = string.Format(Resource.ItemsPageSubmittalProblem);
            }

            await DisplayAlert(Resource.ItemsPageDisplayAlertTitle, msg, "OK");
        }

        private async void ErrorMessage(string msg)
        {
            await DisplayAlert(Resource.ItemsPageDisplayAlertTitle, msg, "OK");
        }

        private async void ErrorMessage2(string msg)
        {
            if (this.BindingContext is ItemsViewModel vm)
            {
                bool retry = await DisplayAlert(Resource.ItemsPageDisplayAlertTitle, msg, "Retry", "Cancel");
                if (retry)
                {
                    await vm.OnSubmitBallot();
                }
            }
        }

        private async void StraightTicket_Clicked(object sender, EventArgs e)
        {
            StraightTicketPage stp = new StraightTicketPage();
            await Navigation.PushModalAsync(stp);
        }
    }
}