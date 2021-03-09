using Election.Models;
using Election.Services;
using ElectionModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Election.ViewModels.Views
{
    public class CategoryViewModel : BaseViewModel
    {
        public Guid Id { get; set; }
        public Guid ElectionId { get; set; }
        public int CategoryTypeId { get; set; }
        public int Sequence { get; set; }
        public bool Delete { get; set; }
            
        private string heading;
        public string Heading
        {
            get { return heading; }
            set
            {
                if (heading != value)
                {
                    heading = value;
                    OnPropertyChanged("Heading");
                    OnPropertyChanged("Display");
                }
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged("Title");
                    OnPropertyChanged("Display");
                }
            }
        }

        private string subTitle;
        public string SubTitle
        {
            get { return subTitle; }
            set
            {
                if (subTitle != value)
                {
                    subTitle = value;
                    OnPropertyChanged("SubTitle");
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

        private int judgePosition;
        public int JudgePosition
        {
            get { return judgePosition; }
            set
            {
                if (judgePosition != value)
                {
                    judgePosition = value;
                    OnPropertyChanged("JudgePosition");
                }
            }
        }

        private bool subTitleVisible;
        public bool SubTitleVisible
        {
            get { return subTitleVisible; }
            set
            {
                if (subTitleVisible != value)
                {
                    subTitleVisible = value;
                    OnPropertyChanged("SubTitleVisible");
                }
            }
        }

        private bool informationVisible;
        public bool InformationVisible
        {
            get { return informationVisible; }
            set
            {
                if (informationVisible != value)
                {
                    informationVisible = value;
                    OnPropertyChanged("InformationVisible");
                }
            }
        }

        private List<SelectItem> categoryTypes;
        public List<SelectItem> CategoryTypes
        {
            get { return categoryTypes; }
            set
            {
                if (categoryTypes != value)
                {
                    categoryTypes = value;
                    OnPropertyChanged("CategoryTypes");
                }
            }
        }

        private SelectItem selectedCategoryType;
        public SelectItem SelectedCategoryType
        {
            get { return selectedCategoryType; }
            set
            {
                if (selectedCategoryType != value)
                {
                    selectedCategoryType = value;
                    OnPropertyChanged("SelectedCategoryType");
                    this.CategoryTypeId = value.Id;
                }
            }
        }

        private List<TicketViewModel> tickets;
        public List<TicketViewModel> Tickets
        {
            get { return tickets; }
            set
            {
                if (tickets != value)
                {
                    tickets = value;
                    OnPropertyChanged("Tickets");
                }
            }
        }

        private ObservableCollection<TicketViewModel> filteredtickets;
        public ObservableCollection<TicketViewModel> FilteredTickets
        {
            get { return filteredtickets; }
            set
            {
                if (filteredtickets != value)
                {
                    filteredtickets = value;
                    OnPropertyChanged("FilteredTickets");
                }
            }
        }

        private TicketViewModel selectedTicket;
        public TicketViewModel SelectedTicket
        {
            get { return selectedTicket; }
            set
            {
                if (selectedTicket != value)
                {
                    selectedTicket = value;
                    OnPropertyChanged("SelectedTicket");
                    HasSelectedTicket = value != null;
                }
            }
        }

        private bool hasSelectedTicket;
        public bool HasSelectedTicket
        {
            get { return hasSelectedTicket; }
            set
            {
                if (hasSelectedTicket != value)
                {
                    hasSelectedTicket = value;
                    OnPropertyChanged("HasSelectedTicket");
                }
            }
        }

        public string Display
        {
            get 
            {
                switch ((CategoryTypeEnum)CategoryTypeId)
                {
                    case CategoryTypeEnum.measure:
                        return Heading;
                    case CategoryTypeEnum.federal:
                        return $"{Heading} - {Title}";
                    case CategoryTypeEnum.state:
                        return $"{Title}";
                    case CategoryTypeEnum.legislative:
                        return $"{Title}";
                    case CategoryTypeEnum.judicial:
                        return $"{Heading} - JP {JudgePosition}";
                    default:
                        return Resource.Category_CategoryTypeUnknownEnum;
                }
            }
        }

        public CategoryViewModel() : base()
        {
            CategoryTypes = Utils.CategoryTypes();
        }

        public void RefreshTickets()
        {
            int cnt = 0;
            FilteredTickets = new ObservableCollection<TicketViewModel>();
            foreach (TicketViewModel tvm in Tickets.Where(n => !n.Delete).OrderBy(n => n.Sequence))
            {
                tvm.Sequence = ++cnt;
                tvm.GetCategoryType = GetCategoryType;
                FilteredTickets.Add(tvm);
            }
        }

        public void RefreshSelectedCategory()
        {
            SelectedCategoryType = CategoryTypes.SingleOrDefault(n => n.Id == CategoryTypeId);
            SubTitleVisible = InformationVisible = true;
        }

        public int RemoveTicketViewModel(int selectedNdx)
        {
            if (SelectedCategoryType == null || DataService.ElectionId == Guid.Empty)
                return selectedNdx;

            TicketViewModel cvm = Tickets[selectedNdx];
            cvm.Delete = true;
            RefreshTickets();
            return selectedNdx;
        }

        public int AddTicketViewModel(int selectedNdx)
        {
            if (SelectedCategoryType == null || DataService.ElectionId == Guid.Empty)
                return selectedNdx;

            TicketViewModel cvm = new TicketViewModel()
            {
                Id = Guid.NewGuid(),
                ElectionId = DataService.ElectionId,
                CategoryId = Id
            };

            int nextNdx = selectedNdx <= 0 ? 1 :
                    selectedNdx == (Tickets.Count - 1) ? nextNdx = Tickets.Count :
                    nextNdx = selectedNdx + 1;

            cvm.Sequence = nextNdx;
            for (int n = nextNdx; n < Tickets.Count; n++)
            {
                Tickets[n].Sequence = n + 1;
            }
            Tickets.Add(cvm);
            RefreshTickets();
            return selectedNdx;
        }

        public int MoveUp(int selectedNdx)
        {
            TicketViewModel tmpA = (TicketViewModel)this.Tickets[selectedNdx - 1];
            TicketViewModel tmpB = (TicketViewModel)this.Tickets[selectedNdx];

            int tmpASequence = tmpA.Sequence;
            tmpA.Sequence = tmpB.Sequence;
            tmpB.Sequence = tmpASequence;

            this.Tickets[selectedNdx] = tmpA;
            this.Tickets[selectedNdx - 1] = tmpB;

            return selectedNdx - 1;
        }

        public int MoveDown(int selectedNdx)
        {
            TicketViewModel tmpA = (TicketViewModel)this.Tickets[selectedNdx];
            TicketViewModel tmpB = (TicketViewModel)this.Tickets[selectedNdx + 1];

            int tmpASequence = tmpA.Sequence;
            tmpA.Sequence = tmpB.Sequence;
            tmpB.Sequence = tmpASequence;

            this.Tickets[selectedNdx] = tmpB;
            this.Tickets[selectedNdx + 1] = tmpA;

            return selectedNdx + 1;
        }

        private CategoryTypeEnum GetCategoryType()
        {
            return (CategoryTypeEnum)CategoryTypeId;
        }
    }
}
