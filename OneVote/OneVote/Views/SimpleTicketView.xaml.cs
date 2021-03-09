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
    public partial class SimpleTicketView : ContentView
    {
        public SimpleTicketView()
        {
            InitializeComponent();
            //this.BindingContextChanged += SimpleTicketView_BindingContextChanged;            
        }

        private void Selection_Clicked(object sender, EventArgs e)
        {
            if (this.BindingContext is TicketViewModel vm)
            {
                vm.Selected = !vm.Selected;
            }
        }

        //private void SimpleTicketView_BindingContextChanged(object sender, EventArgs e)
        //{
        //    if (this.BindingContext is TicketViewModel tvm)
        //    {
        //        tvm.InitSelectedTicket();
        //    }
        //}
    }
}