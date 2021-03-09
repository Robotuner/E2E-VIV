using System;
using System.Collections.Generic;
using System.Text;

namespace OneVote.Models
{
    public enum AboutStatusEnum
    {
        intro,
        hasQRText,
        loading,
        hasLoaded,
        retry,
        noInternet,
        noCameraSupport
        //needsSSN
    }
}
