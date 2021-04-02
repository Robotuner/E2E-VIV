using System;

namespace ElectionModels
{
    public class BallotRequest
    {
        public Guid Id { get; set; }
        public Guid ElectionId { get; set; }
        public string DeviceId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
