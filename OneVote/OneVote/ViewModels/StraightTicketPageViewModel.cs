using AutoMapper;
using ElectionModels;
using ElectionModels.Misc;
using OneVote.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace OneVote.ViewModels
{
    public class StraightTicketPageViewModel : BaseViewModel
    {
        public ICommand PartyTappedCommand { get; set; }
        private IMapper mapper { get; set; }
        public ObservableCollection<PartyViewModel> Parties { get; set; }

        public StraightTicketPageViewModel() : base()
        {
            Parties = new ObservableCollection<PartyViewModel>();
            mapper = OneVote.Models.Utils.CreateMapper();

        }

        public void InitParties()
        {
            Parties.Clear();
            foreach(Party party in DataService.Election.PartyList)
            {
                PartyViewModel pvm = mapper.Map<Party, PartyViewModel>(party);
                pvm.SelectedParty = PartySelected;
                Parties.Add(pvm);
            }
        }
        public void UpdateTickets()
        {
            PartyViewModel selectedPvm = Parties.FirstOrDefault(n => n.Selected);
            if (selectedPvm == null)
                return;
            foreach(CategoryViewModel cat in DataService.CategoryList)
            {
                int count = cat.Tickets.Count(n => n.Party == selectedPvm.Description);
                if (count == 1)
                {
                    foreach (TicketViewModel ticket in cat.Tickets)
                    {
                        ticket.Selected = ticket.Party == selectedPvm.Description ? true : false;
                    }
                }
            }
        }

        private void PartySelected(PartyViewModel selectedParty)
        {
            if (selectedParty.Selected)
            {
                foreach(PartyViewModel pvm in Parties)
                {
                    if (pvm != selectedParty && pvm.Selected)
                    {
                        pvm.Selected = false;
                    }
                }
            }
        }
    }
}
