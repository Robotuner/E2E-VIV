using ElectionModels;
using OneVote.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OneVote.ViewModels
{
    public class ReviewBallotChoicesViewModel : BaseViewModel
    {
        public Action<bool> SetApprovedState { get; set; }
        private readonly List<Vote> AllVotes;

        private List<CategoryType> categoryTypes;
        public List<CategoryType> CategoryTypes
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

        private ObservableCollection<CategoryViewModel> categoryList;
        public ObservableCollection<CategoryViewModel> CategoryList
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
                }
            }
        }

        private ObservableCollection<Vote> filteredVotes;
        public ObservableCollection<Vote> FilteredVotes
        {
            get { return filteredVotes; }
            set
            {
                if (filteredVotes != value)
                {
                    filteredVotes = value;
                    OnPropertyChanged("FilteredVotes");
                }
            }
        }

        private bool noChoices;
        public bool NoChoices
        {
            get { return noChoices; }
            set
            {
                if (noChoices != value)
                {
                    noChoices = value;
                    OnPropertyChanged("NoChoices");
                }
            }
        }

        public ReviewBallotChoicesViewModel()
        {
            AllVotes = DataService.Validate(null);
            CategoryList = new ObservableCollection<CategoryViewModel>();
        }

        public void Init()
        {
            CategoryTypes = DataService.CategoryTypes;
            for(int ndx = 0; ndx<CategoryTypes.Count; ndx++)
            {
                SelectedCategoryType = CategoryTypes[ndx];
                if (CategoryList.Count > 0)
                {
                    break;
                }
            }
        }

        public void RefreshCategories()
        {
            var ans = DataService.CategoryList.Where(n => n.Category.CategoryTypeId == (CategoryTypeEnum)SelectedCategoryType.Id);
            CategoryList.Clear();
            foreach (CategoryViewModel cat in ans.OrderBy(n => n.Sequence).ToList())
            {
                if (cat.Selection != null)
                {
                    this.CategoryList.Add(cat);
                }
            }
            NoChoices = CategoryList.Count == 0;
        }
    }
}
