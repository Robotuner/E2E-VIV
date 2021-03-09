using ElectionModels;
using OneVote.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace OneVote.Views
{
    [QueryProperty(nameof(CatType), nameof(CatType))]
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            //base.OnAppearing();
            if (this.BindingContext is ItemDetailViewModel vm)
            {
            }
        }

        public string CatType
        {
            set
            {
                if (this.BindingContext is ItemDetailViewModel vm)
                {
                    vm.LoadItemId((CategoryTypeEnum)Enum.Parse(typeof(CategoryTypeEnum), value));
                }
            }
        }

    }
}