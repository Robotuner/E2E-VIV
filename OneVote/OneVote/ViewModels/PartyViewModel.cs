using OneVote.Models;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace OneVote.ViewModels
{
    public class PartyViewModel : BaseViewModel
    {
        public Action<PartyViewModel> SelectedParty { get; set; }
        public ICommand PartyTapped { get; set; }

        public int Id { get; set; }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private bool selected;
        public bool Selected
        {
            get { return selected; }
            set
            {
                if (selected != value)
                {
                    selected = value;
                    OnPropertyChanged("Selected");
                    if (value)
                    {
                        SelectedParty?.Invoke(this);
                    }
                    this.Icon = value ? FontAwesome.CheckCircle : FontAwesome.TimesCircle;
                }
            }
        }

        private string icon;
        public string Icon
        {
            get { return icon; }
            set
            {
                if (icon != value)
                {
                    icon = value;
                    OnPropertyChanged("Icon");
                }
            }
        }

        public PartyViewModel() : base()
        {
            this.Icon = this.Selected ? FontAwesome.CheckCircle : FontAwesome.TimesCircle;
            PartyTapped = new Command(() =>
            {
                Selected = !Selected;
            });
        }
    }
}
