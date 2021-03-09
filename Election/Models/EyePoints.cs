using DlibDotNet;
using ElectionModels.Misc;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Election.Models
{
    public class EyePoints
    {
        private Scalar color = new Scalar(0, 0, 255);
        private int thickness = 2;
        private bool isLeftEye { get; set; }
        public OpenCvSharp.Point[] Points { get; set; }
        // EAR Eye Aspect Ratio
        public double EAR { get; set; }
        private double[] ratio { get; set; }
        private int ratioCnt { get; set; }
        private double AvgBlinkRatio { get; set; }

        public EyePoints(bool leftEye = true)
        {
            isLeftEye = leftEye;
            ratio = new double[10];
        }

        public double EyeHeight()
        {
            double h1 = Hypotenuse(Points[1], Points[5]);
            double h2 = Hypotenuse(Points[2], Points[4]);
            return (h1 + h2) / 2.0;
        }

        public void Init(FullObjectDetection landmarks)
        {
            uint startpt = 36;
            if (!isLeftEye) startpt = 42;
            List<OpenCvSharp.Point> lst = new List<OpenCvSharp.Point>();
            lst.Add(GetPoint(landmarks, startpt++));
            lst.Add(GetPoint(landmarks, startpt++));
            lst.Add(GetPoint(landmarks, startpt++));
            lst.Add(GetPoint(landmarks, startpt++));
            lst.Add(GetPoint(landmarks, startpt));
            Points = lst.ToArray();
        }


        private OpenCvSharp.Point GetPoint(FullObjectDetection landmarks, uint pt)
        {
            //Debug.WriteLine($"pt {pt}: {landmarks.GetPart(pt).X} / {landmarks.GetPart(pt).Y}");
            return new OpenCvSharp.Point(landmarks.GetPart(pt).X, landmarks.GetPart(pt).Y);
        }

        public EyeDirection GetEyePosition(OpenCvSharp.Point ctrOfEye)
        {

            if (ctrOfEye.X >= Points[0].X && ctrOfEye.X <= Points[3].X)
            {
                int eyeWidth = Points[3].X - Points[0].X;
                int lookingInside = 0;
                if (isLeftEye)
                {
                    lookingInside = Points[0].X + (int)(eyeWidth / 3) * 2;
                    return ctrOfEye.X > lookingInside ? EyeDirection.left : EyeDirection.center;
                }
                else
                {
                    lookingInside = Points[0].X + (int)(eyeWidth / 3);
                    return ctrOfEye.X < lookingInside ? EyeDirection.right : EyeDirection.center;
                }
            }

            return EyeDirection.unknown;
        }

        public bool HasBlinked(Mat frame)
        {
            // could not get this to reliably capture blinks.
            // just look at the ratios and see if they are changing
            // which would indicate that this is a real person and 
            // not a photo of a person.
            double threshold = 0.3;
            if (Points == null)
                return false;

            double VerticalLength = EyeHeight();
            double HorizontalLength = Hypotenuse(Points[0], Points[3]);
            EAR = VerticalLength / HorizontalLength;

            if (ArrayIsFull() && AvgBlinkRatio > 0)
            {
                if (Deviation())
                    return true;
                //if (EAR < threshold)
                //    return true;
            }

            ratioCnt++;
            if (ratioCnt >= 10) ratioCnt = 0;
            ratio[ratioCnt] = EAR;
            AvgBlinkRatio = ArrayAverage();

            Debug.WriteLine($"Average Blink Ratio: {AvgBlinkRatio} / {EAR} / {VerticalLength}");

            return false;
        }

        private bool ArrayIsFull()
        {
            for (int n = 0; n < 10; n++)
            {
                if (ratio[n] == 0.0)
                    return false;
            }
            return true;
        }

        private double ArrayAverage()
        {
            double sum = 0.0;
            for (int n = 0; n < 10; n++)
            {
                sum += ratio[n];
            }
            return sum / 10.0;
        }

        private bool Deviation()
        {
            double min = 0;
            double max = 0;
            for (int n = 0; n < 10; n++)
            {
                if (ratio[n] > 0)
                {
                    if (min == 0) min = ratio[n];
                    if (max == 0) max = ratio[n];
                    if (min > ratio[n]) min = ratio[n];
                    if (max < ratio[n]) max = ratio[n];
                }
            }
            double ans = ((max - min) / min);
            return ans > 1.0;
        }

        public void DrawPoints(Mat frame)
        {
            if (Points == null)
                return;
            foreach (OpenCvSharp.Point pt in Points)
            {
                Cv2.Circle(img: frame, centerX: pt.X, centerY: pt.Y, radius: 2, color: color, thickness: 1);
            }
        }

        public void DrawBaseLine(Mat frame)
        {
            if (Points == null)
                return;
            Cv2.Line(img: frame, Points[0], Points[3], color, thickness);
        }

        public void VerticalLine(Mat frame)
        {
            if (Points == null)
                return;
            OpenCvSharp.Point centerTop = Midpoints(Points[1], Points[2]);
            OpenCvSharp.Point centerBottom = Midpoints(Points[4], Points[5]);
            Cv2.Line(frame, centerTop, centerBottom, color, thickness);
        }

        private OpenCvSharp.Point Midpoints(OpenCvSharp.Point p1, OpenCvSharp.Point p2)
        {
            int x = (int)(p1.X + p2.X) / 2;
            int y = (int)(p1.Y + p2.Y) / 2;
            return new OpenCvSharp.Point(x, y);
        }

        private double Hypotenuse(OpenCvSharp.Point p1, OpenCvSharp.Point p2)
        {
            var widthSquared = Math.Pow(p1.X - p2.X, 2);
            var heightSquared = Math.Pow(p1.Y - p2.Y, 2);
            return Math.Sqrt(widthSquared + heightSquared);
        }
    }
}
