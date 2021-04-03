using ElectionModels;

namespace OneVote.ViewModels
{
    public class VRViewModel : BaseViewModel
    {
        private VRecord vrecord { get; set; }

        public string Heading
        {
            get { return vrecord.Heading; }
        }

        public new string Title
        {
            get { return vrecord.Title; }
        }

        public int JudgePosition
        {
            get { return vrecord.JudgePosition; }
        }

        public CategoryTypeEnum CategoryType
        {
            get { return vrecord.CategoryType; }
        }

        public string Candidate
        {
            get { return vrecord.Candidate; }
        }

        public string Party
        {
            get { return vrecord.Party; }
        }

        public bool IsJudge
        {
            get
            {
                return JudgePosition > 0;
            }
        }

        public bool HasParty
        {
            get
            {
                return !string.IsNullOrEmpty(Party);
            }
        }
        public VRViewModel(VRecord vrecord)
        {
            this.vrecord = vrecord;
        }
    }
}
