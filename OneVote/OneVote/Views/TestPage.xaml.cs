using OneVote.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneVote.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {

        public TestPage() 
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {

            }
        }

    }
}