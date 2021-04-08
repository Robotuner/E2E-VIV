using AutoMapper;
using Election.Models;
using Election.Services;
using ElectionModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Election.ViewModels.Views
{
    public class ElectionViewModel : BaseViewModel
    {
        public ICommand SaveElectionCommand { get; set; }
        public ICommand CreateElectionCommand { get; set; }
        public Guid Id { get; set; }
        public bool Delete { get; set; }
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged("Date");
                }
            }
        }
        public string Version { get; set; }
        public Action<bool> SetSortButtonVisibility { get; set; }

        private DateTime startDateLocal;
        public DateTime StartDateLocal
        {
            get { return startDateLocal; }
            set
            {
                if (startDateLocal != value)
                {
                    startDateLocal = value;
                    OnPropertyChanged("StartDateLocal");
                }
            }
        }

        private DateTime endDateLocal;
        public DateTime EndDateLocal
        {
            get { return endDateLocal; }
            set
            {
                if (endDateLocal != value)
                {
                    endDateLocal = value;
                    OnPropertyChanged("EndDateLocal");
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

        private bool hasLoaded;
        public bool HasLoaded
        {
            get { return hasLoaded; }
            set
            {
                if (hasLoaded != value)
                {
                    hasLoaded = value;
                    OnPropertyChanged("HasLoaded");
                }
            }
        }

        private bool allowUpdates;
        public bool AllowUpdates
        {
            get { return allowUpdates; }
            set
            {
                if (allowUpdates != value)
                {
                    allowUpdates = value;
                    OnPropertyChanged("AllowUpdates");
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
                    FilterCategoryList();
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

        private List<CategoryViewModel> categoryList;
        public List<CategoryViewModel> CategoryList
        {
            get { return categoryList; }
            set
            {
                if (categoryList != value)
                {
                    categoryList = value;
                    OnPropertyChanged("CategoryList");
                }
            }
        }

        private CategoryViewModel selectedCategory;
        public CategoryViewModel SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                if (selectedCategory != value)
                {
                    selectedCategory = value;
                    OnPropertyChanged("SelectedCategory");
                    HasSelectedCategory = value != null;
                    IsLegislatureOrJudiciary = HasSelectedCategory && (value.CategoryTypeId == CategoryTypeEnum.legislative ||
                        (value.CategoryTypeId == CategoryTypeEnum.judicial)); 
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

        private bool hasSelectedCategory;
        public bool HasSelectedCategory
        {
            get { return hasSelectedCategory; }
            set
            {
                if (hasSelectedCategory != value)
                {
                    hasSelectedCategory = value;
                    OnPropertyChanged("HasSelectedCategory");
                }
            }
        }

        private bool isLegislatureOrJudiciary;
        public bool IsLegislatureOrJudiciary
        {
            get { return isLegislatureOrJudiciary; }
            set
            {
                if (isLegislatureOrJudiciary != value)
                {
                    isLegislatureOrJudiciary = value;
                    OnPropertyChanged("IsLegislatureOrJudiciary");                    
                }
                SetSortButtonVisibility?.Invoke(value);
            }
        }

        private ObservableCollection<CategoryViewModel> filteredCategoryList;
        public ObservableCollection<CategoryViewModel> FilteredCategoryList
        {
            get { return filteredCategoryList; }
            set
            {
                if (filteredCategoryList != value)
                {
                    filteredCategoryList = value;
                    OnPropertyChanged("FilteredCategoryList");
                }
            }
        }

        public ElectionViewModel() : base()
        {
            SaveElectionCommand = new Command(OnSaveElection);
            CreateElectionCommand = new Command(OnCreateElection);
        }

        private void OnCreateElection(object p)
        {
            DataService.Election = new ElectionModels.Election()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Today.AddYears(1),
                StartDateLocal = DateTime.Today.AddYears(1).AddHours(8),
                EndDateLocal = DateTime.Today.AddYears(1).AddHours(20)
            };            
            DataService.Election.CategoryList = new List<Category>();
            InitializeElection(DataService.Election);
        }

        private async Task OnSaveElection(object p)
        {
            IMapper mapper = Utils.CreateMapper();
            List<string> errors = new List<string>();
            if (string.IsNullOrEmpty(Description)) errors.Add("Description is required.");
            if (errors.Count > 0)
            {
                string msg = string.Join("\n", errors.ToList());
                MessageBox.Show("Errors", msg);
                return;
            }

            List<CategoryViewModel> cvmList = CategoryList.Where(n => n.Delete).ToList();
            ElectionModels.Election election = mapper.Map<ElectionViewModel, ElectionModels.Election>(this);

            List<Category> catlist = election.CategoryList.Where(n => n.Delete).ToList();
            //List<Party> plist = mapper.Map<List<PartyViewModel>, List<Party>>(Partys.ToList());
            election.PartyList = DataService.Partys;
            ElectionModels.Election result = await DataService.PostElection(election);  
            if (result != null)
            {
                RemoveDeletedRecords();
            }            
        }
       
        public void FilterCategoryList()
        {
            if (SelectedCategoryType != null)
            {
                List<CategoryViewModel> result = null;
                CategoryTypeEnum ctenum = (CategoryTypeEnum)SelectedCategoryType.Id;
                switch (ctenum)
                {
                    case CategoryTypeEnum.measure: 
                        result = CategoryList.Where(n => n.CategoryTypeId == ctenum &&
                        n.SubcategoryTypeId == CategorySubTypeEnum.undefined && !n.Delete)?.OrderBy(n => n.Sequence)?.ToList();
                        break;
                    case CategoryTypeEnum.federal:
                        result = CategoryList.Where(n => n.CategoryTypeId == ctenum && !n.Delete)?.OrderBy(n => n.Sequence)?.ToList();
                        break;
                    case CategoryTypeEnum.state:
                        result = CategoryList.Where(n => n.CategoryTypeId == ctenum &&
                        n.SubcategoryTypeId == CategorySubTypeEnum.undefined && !n.Delete)?.OrderBy(n => n.Sequence)?.ToList();
                        break;
                    case CategoryTypeEnum.legislative:
                        result = CategoryList.Where(n => n.CategoryTypeId == ctenum &&
                        n.SubcategoryTypeId == CategorySubTypeEnum.undefined && !n.Delete)?.OrderBy(n => n.Sequence)?.ToList();
                        break;
                    case CategoryTypeEnum.judicial:
                        result = CategoryList.Where(n => n.CategoryTypeId == ctenum &&
                        !n.Delete)?.OrderBy(n => n.Sequence)?.ToList();
                        break;
                }
                FilteredCategoryList = new ObservableCollection<CategoryViewModel>(result);
                SelectedCategory = FilteredCategoryList.FirstOrDefault();
                ResetIndex();
            }
        }

        public void SortLegislative()
        {
            // sorts legislative to by district and updates sequence to match.
            List<CategoryViewModel> cvmList = FilteredCategoryList.OrderBy(n => n.District)?.ToList();
            int cnt = 0;
            foreach(CategoryViewModel cvm in cvmList)
            {
                cvm.Sequence = ++cnt;
            }
            FilteredCategoryList = new ObservableCollection<CategoryViewModel>(cvmList);
            SelectedCategory = FilteredCategoryList.FirstOrDefault();
        }

        public void SortJudicial()
        {
            // sorts judicial by CategoryTypeId, then subcategorytypeId, then by judgePosition
            List<CategoryViewModel> cvmList = FilteredCategoryList?.ToList();
            List<CategoryViewModel> resultList = new List<CategoryViewModel>();
            // first get the supreme court judges
            foreach (CategoryViewModel cvm in cvmList.Where(n => n.CategoryTypeId == CategoryTypeEnum.judicial &&
                         n.SubcategoryTypeId == CategorySubTypeEnum.supremeCourt).OrderBy(n => n.JudgePosition))
            {
                resultList.Add(cvm);
            }
            // now add the Court of appeals judges
            foreach (CategoryViewModel cvm in cvmList.Where(n => n.CategoryTypeId == CategoryTypeEnum.judicial &&
                         n.SubcategoryTypeId == CategorySubTypeEnum.courtOfAppeals).OrderBy(n => n.JudgePosition))
            {
                resultList.Add(cvm);
            }
            // now add the Superior courts ordered by Heading
            foreach (CategoryViewModel cvm in cvmList.Where(n => n.CategoryTypeId == CategoryTypeEnum.judicial &&
                         n.SubcategoryTypeId == CategorySubTypeEnum.superiorCourt).OrderBy(n => n.Heading).OrderBy(n => n.Heading))
            {
                resultList.Add(cvm);
            }
            int cnt = 0;
            foreach (CategoryViewModel cvm in resultList)
            {
                cvm.Sequence = ++cnt;
            }
            FilteredCategoryList = new ObservableCollection<CategoryViewModel>(resultList);
            SelectedCategory = FilteredCategoryList.FirstOrDefault();
        }

        public async Task LoadData()
        {
            var allElections = await DataService.GetAllElections();
            if (allElections != null)
            {
                Elections = new ObservableCollection<SelectGuidItem>(allElections);
                if (Elections != null && Elections.Count > 0)
                {
                    SelectedElection = Elections.First();
                }
            }
        }

        private void RemoveDeletedRecords()
        {
            List<CategoryViewModel> catDelList = new List<CategoryViewModel>();
            
            foreach (CategoryViewModel cat in CategoryList)
            {
                List<TicketViewModel> ticketDelList = new List<TicketViewModel>();
                if (cat.Delete) catDelList.Add(cat);
                foreach(TicketViewModel ticket in cat.Tickets)
                {
                    if (ticket.Delete) ticketDelList.Add(ticket);
                }
                foreach(TicketViewModel tvm in ticketDelList)
                {
                    cat.Tickets.Remove(tvm);
                }
            }
            foreach(CategoryViewModel cvm in catDelList)
            {
                catDelList.Remove(cvm);
            }
        }

        public void InitializeElection(ElectionModels.Election election)
        {
            if (election != null)
            {
                IMapper mapper = Utils.CreateMapper();
                mapper.Map<ElectionModels.Election, ElectionViewModel>(election, this);
                CategoryTypes = Utils.CategoryTypes();
                SelectedCategoryType = CategoryTypes.SingleOrDefault(n => n.Id == (int)CategoryTypeEnum.measure);
                HasLoaded = true;
            }
        }

        public int RemoveCategoryViewModel(int selectedNdx)
        {
            if (SelectedCategoryType == null || DataService.ElectionId == Guid.Empty)
                return selectedNdx;

            CategoryViewModel cvm = FilteredCategoryList[selectedNdx];
            cvm.Delete = true;
            FilterCategoryList();
            ResetIndex();
            return selectedNdx;
        }

        public int AddCategoryViewModel(int selectedNdx)
        {
            if (SelectedCategoryType == null || DataService.ElectionId == Guid.Empty)
                return selectedNdx;

            CategoryViewModel cvm = new CategoryViewModel()
            {
                Id = Guid.NewGuid(),
                ElectionId = DataService.ElectionId,
                CategoryTypeId = (CategoryTypeEnum)SelectedCategoryType.Id,
                Tickets = new List<TicketViewModel>()
            };

            int nextNdx = selectedNdx <= 0 ? 1 :
                    selectedNdx == (FilteredCategoryList.Count - 1) ? nextNdx = FilteredCategoryList.Count :
                    nextNdx = selectedNdx + 1;

            cvm.Sequence = nextNdx;
            for (int n = nextNdx; n < FilteredCategoryList.Count; n++)
            {
                FilteredCategoryList[n].Sequence = n + 1;
            }

            CategoryList.Add(cvm);
            FilterCategoryList();

            return selectedNdx;
        }

        public int MoveUp(int selectedNdx)
        {
            CategoryViewModel tmpA = (CategoryViewModel)this.FilteredCategoryList[selectedNdx - 1];
            CategoryViewModel tmpB = (CategoryViewModel)this.FilteredCategoryList[selectedNdx];

            int tmpASequence = tmpA.Sequence;
            tmpA.Sequence = tmpB.Sequence;
            tmpB.Sequence = tmpASequence;

            this.FilteredCategoryList[selectedNdx] = tmpA;
            this.FilteredCategoryList[selectedNdx - 1] = tmpB;

            return selectedNdx - 1;
        }

        public int MoveDown(int selectedNdx)
        {
            CategoryViewModel tmpA = (CategoryViewModel)this.FilteredCategoryList[selectedNdx];
            CategoryViewModel tmpB = (CategoryViewModel)this.FilteredCategoryList[selectedNdx + 1];

            int tmpASequence = tmpA.Sequence;
            tmpA.Sequence = tmpB.Sequence;
            tmpB.Sequence = tmpASequence;
    
            this.FilteredCategoryList[selectedNdx] = tmpB;
            this.FilteredCategoryList[selectedNdx + 1] = tmpA;

            return selectedNdx + 1;
        }

        private void ResetIndex()
        {
            int cnt = 0;
            foreach(CategoryViewModel cvm in FilteredCategoryList)
            {
                cvm.Sequence = ++cnt;
            }
        }
    }
}
