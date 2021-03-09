using System;
using System.Collections.Generic;

namespace ElectionModels
{
    public interface ISignature
    {
        Guid Id { get; set; }
        Guid BallotId { get; set; }
        Guid ElectionId { get; set; }
        string Name { get; set; }
        bool Confirmed { get; set; }
        string DeviceId { get; set; }
        int BirthYear { get; set; }
        byte[] ImageArray { get; set; }
        DateTime CreateDate { get; }
        double Longitude { get; set; }
        double Latitude { get; set; }
        int Platform { get; set; }
        Guid? PreviousSignature { get; set; }
        int SignatureStatus { get; set; }
        DateTime SubmitDate { get; set; }

        List<Vote> Votes { get; set; }
    }

    public class Signature
    {
        public Guid Id { get; set; }
        public Guid BallotId { get; set; }
        public Guid ElectionId { get; set; }
        public string Name { get; set; }
        public bool Confirmed { get; set; }
        public string DeviceId { get; set; }
        public int BirthYear { get; set; }
        public byte[] ImageArray { get; set; }
        public DateTime CreateDate { get; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Platform { get; set; }
        public Guid? PreviousSignature { get; set; }
        public int SignatureStatus { get; set; }
        public DateTime SubmitDate { get; set; }

        public List<Vote> Votes { get; set; }

        public string PhoneNumber { get; set; }
    }
}
