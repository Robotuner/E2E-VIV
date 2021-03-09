using AutoMapper;
using Election.Models;
using Election.Services;
using ElectionModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Election.ViewModels.Views
{
    public class ElectionSummaryViewModel : ElectionBaseViewModel
    { 
        private List<VoteResult> AllResults { get; set; }
   
        private List<Category> AllCategories { get; set; }
        public ICommand SummaryCommand { get; set; }
          
        public ObservableCollection<CategoryType> CategoryTypes { get; set; }
        public ObservableCollection<VoteResultViewModel> RaceResults { get; set; }
        public ObservableCollection<CategoryViewModel> Categories { get; set; }

        private CategoryType selectedCategoryType;
        public CategoryType SelectedCategoryType
        {
            get { return selectedCategoryType; }
            set
            {
                if (selectedCategoryType != value)
                {
                    selectedCategoryType = value;
                    OnPropertyChanged("SelectedCategoryType");
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
                }
            }
        }

        public ElectionSummaryViewModel() : base()
        {
            CategoryTypes = new ObservableCollection<CategoryType>();
            Categories = new ObservableCollection<CategoryViewModel>();
            RaceResults = new ObservableCollection<VoteResultViewModel>();
        }

        public override async Task OnLoaded()
        {
            await base.OnLoaded();
            CategoryTypes.Clear();
            foreach (CategoryType ct in await DataService.InitCategoryTypes())
            {
                CategoryTypes.Add(ct);
            }
            

            if (SelectedElection != null)
            {
                AllResults = await DataService.GetElectionSummary(SelectedElection.Id);
                await this.InitElection();
            }

            SelectedCategoryType = CategoryTypes.FirstOrDefault();
        }

        public override async Task InitElection()
        {
            await base.InitElection();
            if (this.SelectedElection != null)
            {
                this.AllResults = await DataService.GetElectionSummary(this.SelectedElection.Id);
                this.AllCategories = this.Election.CategoryList;
            }
        }

        public void InitCategories()
        {
            if (this.SelectedCategoryType != null)
            {
                Categories.Clear();
                foreach(Category category in this.AllCategories.Where(n => n.CategoryTypeId == (CategoryTypeEnum)this.SelectedCategoryType.Id).OrderBy(n => n.Sequence))
                {
                    CategoryViewModel cvm = mapper.Map<Category, CategoryViewModel>(category);
                    this.Categories.Add(cvm);
                }
                this.SelectedCategory = this.Categories.First();
            }
        }

        private string SetTitle(CategoryViewModel cvm)
        {
            switch ((CategoryTypeEnum)cvm.CategoryTypeId)
            {
                case CategoryTypeEnum.measure:
                    return $"{cvm.Title}";
                case CategoryTypeEnum.federal:
                    return $"{cvm.Heading}-{cvm.Title}"; 
                case CategoryTypeEnum.state:
                case CategoryTypeEnum.legislative:
                case CategoryTypeEnum.judicial:
                    return $"{cvm.Heading}";
                    //return $"{cvm.Title}";    
            }
            return $"{cvm.Title}";
        }

        public void InitContestResults()
        {
            if (this.SelectedCategory != null)
            {
                this.RaceResults.Clear();
                foreach(TicketViewModel ticket in this.SelectedCategory.Tickets)
                {
                    VoteResultViewModel result = mapper.Map<VoteResult, VoteResultViewModel>(this.AllResults.SingleOrDefault(n => n.Id == ticket.Id));
                    if (result != null)
                    {
                        result.Title = this.SetTitle(this.SelectedCategory);
                        result.Subtitle = this.SelectedCategory.SubTitle;
                        result.Candidate = ticket.Description;
                        result.Party = this.Election.PartyList.SingleOrDefault(n => n.Id == ticket.PartyId)?.Description;
                        result.JudgePosition = SelectedCategory.JudgePosition > 0 ? $"JP-{SelectedCategory.JudgePosition}" : null;
                        this.RaceResults.Add(result);
                    }
                }
                int totalCount = this.RaceResults.Sum(n => n.Count);
                foreach(VoteResultViewModel vrvm in this.RaceResults)
                {
                    vrvm.PercentOfTotal = Math.Round(((vrvm.Count * 1.0) / totalCount) * 100.0, 2);
                }
            }
        }

    }
}
