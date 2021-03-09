using ElectionModels;
using OneVote.Models;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace OneVote.ViewModels
{
    public class TicketViewModel : BaseViewModel
    {
        public Ticket Ticket { get; set; }
        public Action<TicketViewModel> SelectedTicket { get; set; }
        //public Func<TicketViewModel> GetSelectedTicket { get; set; }
        public ICommand VoteTapped { get; set; }
        public Guid Id { get; set; }

        private Guid categoryId;
        public Guid CategoryId
        {
            get { return categoryId; }
            set
            {
                if (categoryId != value)
                {
                    categoryId = value;
                    OnPropertyChanged("CategoryId");
                }
            }
        }

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

        private string party;
        public string Party
        {
            get { return party; }
            set
            {
                if (party != value)
                {
                    party = value;
                    OnPropertyChanged("Party");
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
                        SelectedTicket?.Invoke(this);
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

        public TicketViewModel() : base()
        {
            this.Icon = this.Selected ? FontAwesome.CheckCircle : FontAwesome.TimesCircle;
            VoteTapped = new Command(() =>
            {
                Selected = !Selected;
            });
        }
    }
}
