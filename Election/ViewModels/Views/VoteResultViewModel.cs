using System;

namespace Election.ViewModels.Views
{
    public class VoteResultViewModel : BaseViewModel
    {
        public Guid ElectionId { get; set; }
        public Guid CategoryId { get; set; }

        private int count;
        public int Count
        {
            get { return count; }
            set
            {
                if (count != value)
                {
                    count = value;
                    OnPropertyChanged("Count");
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
                }
            }
        }

        private string subTitle;
        public string Subtitle
        {
            get { return subTitle; }
            set
            {
                if (subTitle != value)
                {
                    subTitle = value;
                    OnPropertyChanged("Subtitle");
                }
            }
        }

        private string candidate;
        public string Candidate
        {
            get { return candidate; }
            set
            {
                if (candidate != value)
                {
                    candidate = value;
                    OnPropertyChanged("Candidate");
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

        private string judgePosition;
        public string JudgePosition
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

        private double percentOfTotal;
        public double PercentOfTotal
        {
            get { return percentOfTotal; }
            set
            {
                if (percentOfTotal != value)
                {
                    percentOfTotal = value;
                    OnPropertyChanged("PercentOfTotal");
                }
            }
        }

    }
}
