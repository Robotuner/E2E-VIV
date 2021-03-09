using OneVote.Models;
using OneVote.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OneVote.Views
{
    public partial class NewItemPage : ContentPage
    {
        public CategoryTypeItem Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}