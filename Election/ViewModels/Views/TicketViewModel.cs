using Election.Models;
using ElectionModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Election.ViewModels.Views
{
    public class TicketViewModel : BaseViewModel
    {
        public Func<CategoryTypeEnum> GetCategoryType { get; set; }
        public Guid Id { get; set; }
        public Guid ElectionId { get; set; }
        public Guid CategoryId { get; set; }
        public int? PartyId { get; set; }
        public int Sequence { get; set; }
        public bool Delete { get; set; }
        public TicketTypeEnum TicketType { get; set; }

        private SelectItem selectedParty;
        public SelectItem SelectedParty
        {
            get { return selectedParty; }
            set
            {
                if (selectedParty != value)
                {
                    selectedParty = value;
                    OnPropertyChanged("SelectedParty");
                    if (value == null || selectedParty.Id == 0) PartyId = null;
                    else
                    {
                        PartyId = value.Id;
                    }
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

        private string information;
        public string Information
        {
            get { return information; }
            set
            {
                if (information != value)
                {
                    information = value;
                    OnPropertyChanged("Information");
                }
            }
        }

        private SelectItem selectedLegislativePosition;
        public SelectItem SelectedLegislativePosition
        {
            get { return selectedLegislativePosition; }
            set
            {
                if (selectedLegislativePosition != value)
                {
                    selectedLegislativePosition = value;
                    OnPropertyChanged("SelectedLegislativePosition");
                    TicketType = (TicketTypeEnum)value.Id;
                }
            }
        }

        private List<SelectItem> parties;
        public List<SelectItem> Parties
        {
            get { return parties; }
            set
            {
                if (parties != value)
                {
                    parties = value;
                    OnPropertyChanged("Parties");
                }
            }
        }

        private List<SelectItem> legislativePositions;
        public List<SelectItem> LegislativePositions
        {
            get { return legislativePositions; }
            set
            {
                if (legislativePositions != value)
                {
                    legislativePositions = value;
                    OnPropertyChanged("LegislativePositions");
                }
            }
        }

        private bool legislativePositionVisible;
        public bool LegislativePositionVisible
        {
            get { return legislativePositionVisible; }
            set
            {
                if (legislativePositionVisible != value)
                {
                    legislativePositionVisible = value;
                    OnPropertyChanged("LegislativePositionVisible");
                }
            }
        }

        public TicketViewModel() : base()
        {
            this.Parties = Utils.Parties();
            this.LegislativePositions = Utils.LegislativePositions();
        }

        public void LoadData()
        {
            if (this.Id != Guid.Empty)
            {
                SelectedParty = Parties.SingleOrDefault(n => n.Id == PartyId);
                SelectedLegislativePosition = LegislativePositions.SingleOrDefault(n => n.Id == (int)TicketType);

            }
        }
    }
}
