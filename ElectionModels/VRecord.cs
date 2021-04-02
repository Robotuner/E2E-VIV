namespace ElectionModels
{
    public class VRecord
    {
        public string Heading { get; set; }
        public string Title { get; set; }
        public int JudgePosition { get; set; }
        public CategoryTypeEnum CategoryType { get; set; }
        public string Candidate { get; set; }
        public string Party { get; set; }

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

    }
}
