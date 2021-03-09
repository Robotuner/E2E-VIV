namespace ElectionModels
{
    // when user initially submits a ballot the selection is 
    // by default a valid vote.  If the user resubmits ballot
    // at a later date, this original choice is rejected.  We are not deleting
    // any records from the db, only changing the vote status.
    public enum VoteStatusEnum
    {
        isValidVote,
        choiceRejected
    }

    public static class VoteStatusEnumExtension
    {
        public static string ToDisplay(this VoteStatusEnum status)
        {
            switch (status)
            {
                case VoteStatusEnum.isValidVote: return "Is Valid";
                case VoteStatusEnum.choiceRejected: return "Rejected Vote";
                default:
                    return"Rejected Vote";
            }
        }
    }
}
