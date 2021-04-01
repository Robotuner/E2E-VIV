using System;

namespace ElectionModels.Misc
{
    public class QRModel
    {
        public Guid ElectionId { get; set; }
        public string Registration { get; set; }
        public int BirthYear { get; set; }
        public string EncryptedBallotId { get; set; }
        public Guid BallotId { get; set; }
    }
}
