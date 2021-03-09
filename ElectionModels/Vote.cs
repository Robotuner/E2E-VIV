using System;

namespace ElectionModels
{
    public interface IVote
    {
        Guid Id { get; set; }
        Guid ElectionId { get; set; }
        Guid BallotId { get; set; }
        Guid CategoryId { get; set; }
        int CategoryTypeId { get; set; }
        Guid? SelectionId { get; set; }
        int VoteStatus { get; set; }
        //DateTime CreateDate { get; }
        DateTime ApprovalDate { get; set; }
    }

    public class Vote : IVote
    {
        public Guid Id { get; set; }
        public Guid ElectionId { get; set; }
        public Guid BallotId { get; set; }
        public Guid CategoryId { get; set; }
        public int CategoryTypeId { get; set; }
        public Guid? SelectionId { get; set; }
        public int VoteStatus { get; set; }
        //public DateTime CreateDate { get; }
        public DateTime ApprovalDate { get; set; }
    }
}
