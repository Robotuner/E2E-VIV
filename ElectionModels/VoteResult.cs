using System;

namespace ElectionModels
{
    public class VoteResult
    {
        public Guid Id { get; set; }
        public Guid ElectionId { get; set; }
        public Guid CategoryId { get; set; }
        public int Count { get; set; }
    }
}
