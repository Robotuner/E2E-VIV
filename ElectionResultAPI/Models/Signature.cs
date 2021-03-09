using System;

namespace ElectionResultAPI.Models
{
    public interface ISignature
    {
        Guid Id { get; set; }
        Guid BallotId { get; set; }
        string Name { get; set; }
        bool Confirmed { get; set; }
        byte[] ImageArray { get; set; }
        DateTime CreateDate { get; }
    }

    public class Signature
    {
        public Guid Id { get; set; }
        public Guid BallotId { get; set; }
        public string Name { get; set; }
        public bool Confirmed { get; set; }
        public byte[] ImageArray { get; set; }
        public DateTime CreateDate { get; }
    }
}
