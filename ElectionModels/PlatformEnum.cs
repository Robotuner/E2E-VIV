using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionModels
{
    public enum PlatformEnum
    {
        unknown,
        android,
        iOS,
        windows,
        mac,
        other
    }

    public static class PlatformEnumExtension
    {
        public static string ToDisplay(this PlatformEnum type)
        {
            switch (type)
            {
                case PlatformEnum.unknown:
                    return "Unknown";
                case PlatformEnum.android:
                    return "Android";
                case PlatformEnum.iOS:
                    return "iOS";
                case PlatformEnum.windows:
                    return "Windows";
                case PlatformEnum.mac:
                    return "Mac";
                default:
                    return "Other";
            }
        }
    }
}
