using ElectionModels;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace OneVote.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        private double ticketTemplateHeightWithoutParty;
        private double ticketTemplateHeight;
        public Category Category { get; set; }

        public Guid Id { get; set; }
        public int Sequence { get; set; }
        public CategoryTypeEnum CategoryType = CategoryTypeEnum.undefined;

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
                }
            }
        }

        private bool includeSenator;
        public bool IncludeSenator
        {
            get { return includeSenator; }
            set
            {
                if (includeSenator != value)
                {
                    includeSenator = value;
                    OnPropertyChanged("IncludeSenator");
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

        private TicketViewModel selection;
        public TicketViewModel Selection
        {
            get { return selection; }
            set
            {
                if (selection != value)
                {
                    selection = value;
                    OnPropertyChanged("Selection");
                }
            }
        }

        private double ticketViewHeight;
        public double TicketViewHeight
        {
            get { return ticketViewHeight; }
            set
            {
                if (ticketViewHeight != value)
                {
                    ticketViewHeight = value;
                    OnPropertyChanged("TicketViewHeight");
                }
            }
        }

        private ObservableCollection<TicketViewModel> tickets;
        public ObservableCollection<TicketViewModel> Tickets
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

        public CategoryViewModel()
        {

        }

        public void SetTicketViewHeight(CategoryTypeEnum ctype)
        {
            ticketTemplateHeightWithoutParty = this.IsAccessibility ? 40.0 : 38.0;
            ticketTemplateHeight = this.IsAccessibility ? 96.0 : 70.0;

            this.CategoryType = ctype;
            double height =  (ctype == CategoryTypeEnum.legislative || ctype == CategoryTypeEnum.state || ctype == CategoryTypeEnum.federal) ?
                ticketTemplateHeight:ticketTemplateHeightWithoutParty;
            this.TicketViewHeight = Tickets.Count * height;
        }

        public void TickedSelected(TicketViewModel ticket)
        {
            try
            {
                if (ticket != null)
                {
                    if (ticket.Selected)
                    {
                        Selection = ticket;
                        foreach (TicketViewModel tvm in Tickets.Where(n => n.Id != ticket.Id))
                        {
                            tvm.Selected = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public override string ToString()
        {
            return Heading;
        }
        public TicketViewModel GetSelectedTicket()
        {
            return Selection;
        }

    }
}
