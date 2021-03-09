using AutoMapper;
using Election.Models;
using Election.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Election.ViewModels.Views
{
    public class ElectionBaseViewModel : BaseViewModel
    {
        protected IMapper mapper = Utils.CreateMapper();
        protected ElectionModels.Election Election { get; set; }

        private ObservableCollection<SelectGuidItem> elections;
        public ObservableCollection<SelectGuidItem> Elections
        {
            get { return elections; }
            set
            {
                if (elections != value)
                {
                    elections = value;
                    OnPropertyChanged("Elections");
                }
            }
        }

        private SelectGuidItem selectedElection;
        public SelectGuidItem SelectedElection
        {
            get { return selectedElection; }
            set
            {
                if (selectedElection != value)
                {
                    selectedElection = value;
                    OnPropertyChanged("SelectedElection");
                }
            }
        }

        public ElectionBaseViewModel() : base()
        {
            mapper = Utils.CreateMapper();
        }
        public virtual async Task OnLoaded()
        {
            var allElections = await DataService.GetAllElections();
            if (allElections != null)
            {
                this.Elections = new ObservableCollection<SelectGuidItem>(allElections);
                if (Elections != null && Elections.Count > 0)
                {
                    var ans = Elections.FirstOrDefault();
                    SelectedElection = ans;
                }
            }
        }

        public virtual async Task InitElection()
        {
            if (this.SelectedElection != null)
            {
                this.Election = await DataService.InitElection(this.SelectedElection.Id);
            }
        }
    }
}
