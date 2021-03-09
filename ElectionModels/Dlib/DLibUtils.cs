using DlibDotNet;
using System.Diagnostics;

namespace ElectionModels.Dlib
{
    public class DLibUtils
    {
        public static OpenCvSharp.Point GetPoint(FullObjectDetection landmarks, uint pt)
        {
            //Debug.WriteLine($"pt {pt}: {landmarks.GetPart(pt).X} / {landmarks.GetPart(pt).Y}");
            return new OpenCvSharp.Point(landmarks.GetPart(pt).X, landmarks.GetPart(pt).Y);
        }
    }
}
