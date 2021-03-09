using System;

namespace ElectionModels
{
    public class Ballot
    {
        public Guid Id { get; set; }
        public Guid ElectionId { get; set; }
        public int Nonce { get; set; }
        public string BallotChain { get; set; }       
    }
}
