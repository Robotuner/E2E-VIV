using OneVote.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneVote.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StraightTicketPage : ContentPage
    {
        public StraightTicketPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (this.BindingContext is StraightTicketPageViewModel vm)
            {
                vm.InitParties();
            }
        }

        private async void Close_Clicked(object sender, EventArgs e)
        {
            if (this.BindingContext is StraightTicketPageViewModel vm)
            {
                vm.UpdateTickets();
                await Navigation.PopModalAsync();
            }
        }
    }
}