using System;

namespace ElectionResultAPI.Models
{
    public interface IVote
    {
        Guid Id { get; set; }
        Guid ElectionId { get; set; }
        Guid BallotId { get; set; }
        int CategoryTypeId { get; set; }
        Guid? SelectionId { get; set; }
    }

    public class Vote
    {
        public Guid Id { get; set; }
        public Guid ElectionId { get; set; }
        public Guid BallotId { get; set; }
        public int CategoryTypeId { get; set; }
        public Guid? SelectionId { get; set; }
    }
}
