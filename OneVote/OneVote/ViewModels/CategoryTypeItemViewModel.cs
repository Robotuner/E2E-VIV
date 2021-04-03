using ElectionModels;
using OneVote.Models;

namespace OneVote.ViewModels
{
    public class CategoryTypeItemViewModel : BaseViewModel
    {
        private CategoryTypeItem categoryTypeItem;

        public CategoryTypeEnum Id
        {
            get { return categoryTypeItem.Id; }
        }

        public string Text
        {
            get { return categoryTypeItem.Text; }
        }

        public string Description
        {
            get { return categoryTypeItem.Description; }
        }

        private int selected;
        public int Selected
        {
            get { return selected; }
            set
            {
                if (selected != value)
                {
                    selected = value;
                    OnPropertyChanged("Selected");
                }
            }
        }

        private int total;
        public int Total
        {
            get { return total; }
            set
            {
                if (total != value)
                {
                    total = value;
                    OnPropertyChanged("Total");
                }
            }
        }


        public CategoryTypeItemViewModel(CategoryTypeItem cti) : base()
        {
            this.categoryTypeItem = cti;
        }
    }
}
