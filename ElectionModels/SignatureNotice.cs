﻿using System;

namespace ElectionModels
{
    public class SignatureNotice
    {
        public Guid Id { get; set; }
        public Guid BallotId { get; set; }
        public int Nonce { get; set; }
        public string DeviceId { get; set; }
    }
}
