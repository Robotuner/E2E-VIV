using DlibDotNet;
using Election.Models;
using ElectionModels.Misc;
using Emgu.CV;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Election.ViewModels.Views
{
    public class BaseFaceViewModel : BaseViewModel
    {
        private readonly static string shapePredictorDataFile = @"./Dlib/shape_predictor_68_face_landmarks.dat";
        public Action ClearCanvas { get; set; }
        public Action<System.Drawing.Rectangle[], System.Drawing.Size> DrawOnImage { get; set; }
        public Action<List<ConfidenceRect>, System.Drawing.Size> DrawDnnOnImage { get; set; }

        protected string ImagePath { get; set; }
        protected double Ratio { get; set; }

        public ICommand RotateCCWCommand { get; set; }
        public ICommand RotateCWCommand { get; set; }
        public ICommand DetectCommand { get; set; }

        private Rotation rotation;
        public Rotation Rotation
        {
            get { return rotation; }
            set
            {
                if (rotation != value)
                {
                    rotation = value;
                    OnPropertyChanged("Rotation");
                }
            }
        }

        private BitmapImage imageSource;
        public BitmapImage ImageSource
        {
            get { return imageSource; }
            set
            {
                if (imageSource != value)
                {
                    imageSource = value;
                    OnPropertyChanged("ImageSource");
                }
            }
        }

        private double width;
        public double Width
        {
            get { return width; }
            set
            {
                if (width != value)
                {
                    width = value;
                    OnPropertyChanged("Width");
                }
            }
        }

        public BaseFaceViewModel() : base()
        {
            RotateCCWCommand = new Command(OnRotateCCW, null);
            RotateCWCommand = new Command(OnRotateCW, null);
            DetectCommand = new Command(OnDetect);
        }

        private void OnRotateCCW(object p)
        {
            switch (Rotation)
            {
                case Rotation.Rotate0: Rotation = Rotation.Rotate270; break;
                case Rotation.Rotate90: Rotation = Rotation.Rotate0; break;
                case Rotation.Rotate180: Rotation = Rotation.Rotate90; break;
                case Rotation.Rotate270: Rotation = Rotation.Rotate180; break;
            }
            LoadImage();
            SaveBMI();
        }

        private void OnRotateCW(object p)
        {
            switch (Rotation)
            {
                case Rotation.Rotate0: Rotation = Rotation.Rotate90; break;
                case Rotation.Rotate90: Rotation = Rotation.Rotate180; break;
                case Rotation.Rotate180: Rotation = Rotation.Rotate270; break;
                case Rotation.Rotate270: Rotation = Rotation.Rotate0; break;
            }
            LoadImage();
            SaveBMI();
        }

        private void OnDetect(object p)
        {
            try
            {
                IsBusy = true;
                int FaceCount = 0;
                bool UseHaarCascade = true;
                if (ImageSource != null)
                {
                    //EmguFaceDetector();
                    if (UseHaarCascade)
                    {
                        FaceCount = OpenCVFaceDetector(ImagePath);
                    }
                    else
                    {
                        //FaceCount = OpenCVDeepLearningDetector(ImagePath);
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }


        private void EmguFaceDetector()
        {
            Emgu.CV.CascadeClassifier emguFaceClassifier = null;
            if (File.Exists(this.ImagePath))
            {
                if (File.Exists(@"./haarcascade/haarcascade_frontalface_alt.xml"))
                {
                    emguFaceClassifier = new Emgu.CV.CascadeClassifier(@"./haarcascade/haarcascade_frontalface_alt.xml");
                    Emgu.CV.Mat src = CvInvoke.Imread(this.ImagePath, 0);
                    Emgu.CV.Mat gray = new Emgu.CV.Mat();
                    CvInvoke.CvtColor(src, gray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
                    var faces = emguFaceClassifier.DetectMultiScale(gray, 1.1, 2, new System.Drawing.Size(30, 30));
                    int facecnt = faces.Length;
                }
            }
        }

        private int OpenCVFaceDetector(string path)
        {
            // uses openCv Library
            OpenCvSharp.CascadeClassifier faceClassifier = new OpenCvSharp.CascadeClassifier(@"./haarcascade/haarcascade_frontalface_alt.xml");

            OpenCvSharp.Mat result;
            Rect[] faces = new Rect[0];

            using (var src = new OpenCvSharp.Mat(path, OpenCvSharp.ImreadModes.Color))
            using (var gray = new OpenCvSharp.Mat())
            {
                result = src.Clone();
                Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);

                // Detect faces
                faces = faceClassifier.DetectMultiScale(gray, 1.08, 2, OpenCvSharp.HaarDetectionType.ScaleImage);
                List<System.Drawing.Rectangle> rfaces = new List<System.Drawing.Rectangle>();
                foreach (Rect face in faces)
                {
                    System.Drawing.Rectangle r = new System.Drawing.Rectangle(face.X, face.Y, face.Width, face.Height);
                    this.GetLandmarks(gray, face, rfaces);
                    rfaces.Add(r);
                }
                DrawOnImage?.Invoke(rfaces.ToArray(), new System.Drawing.Size(result.Width, result.Height));
            }
            result.Dispose();
            return faces.Length;
        }

        private void GetLandmarks(OpenCvSharp.Mat frame, OpenCvSharp.Rect face, List<System.Drawing.Rectangle> rfaces)
        {
            EyePoints rightEye = new EyePoints(leftEye: true);
            EyePoints leftEye = new EyePoints(leftEye: false);
            ShapePredictor predictor = ShapePredictor.Deserialize(shapePredictorDataFile);

            //Scalar eyecolor = new Scalar(0, 0, 255);
            Array2D<byte> gray = ConvertMatToDlib2DArray(frame);
            FullObjectDetection landmarks = predictor.Detect(gray, ConvertToDlib(face));
            InitializeEyes(landmarks, leftEye, rightEye);
            //DrawEye(que, landmarks, leftEye);
            //DrawEye(que, landmarks, rightEye);
            Rect leftboundingBox = BoundingBoxAroundEye(leftEye, 0);
            rfaces.Add(FromOpenCvRect(leftboundingBox));
            //DrawRect(frame, leftboundingBox);
            OpenCvSharp.Point centerOfLeftEye = DetectCenterOfEye(frame, leftboundingBox);
            centerOfLeftEye.X += leftboundingBox.X;

            Rect rightboundingBox = BoundingBoxAroundEye(rightEye, 0);
            rfaces.Add(FromOpenCvRect(rightboundingBox));
            //DrawRect(frame, rightboundingBox);
            OpenCvSharp.Point centerOfRightEye = DetectCenterOfEye(frame, rightboundingBox);
            centerOfRightEye.X += rightboundingBox.X;

            EyeDirection leftEyeDirection = leftEye.GetEyePosition(centerOfLeftEye);
            EyeDirection rightEyeDirection = rightEye.GetEyePosition(centerOfRightEye);

            //EyeDirection eyeDirection = EyeDirection.unknown;
            //if (leftEyeDirection == EyeDirection.center || rightEyeDirection == EyeDirection.center) eyeDirection = EyeDirection.center;
            //else if (leftEyeDirection == EyeDirection.left) eyeDirection = EyeDirection.left;
            //else if (rightEyeDirection == EyeDirection.right) eyeDirection = EyeDirection.right;

            //OpenCvSharp.Point position = new OpenCvSharp.Point(50, 50);
            //Cv2.PutText(img: frame, text: eyeDirection.ToDisplay(), org: position, fontFace: HersheyFonts.HersheySimplex, fontScale: 2, new Scalar(0, 0, 255));
        }

        private System.Drawing.Rectangle FromOpenCvRect(OpenCvSharp.Rect rect)
        {
            return new System.Drawing.Rectangle(new System.Drawing.Point(rect.X, rect.Y), new System.Drawing.Size(rect.Size.Width, rect.Size.Height));
        }
        private Array2D<byte> ConvertMatToDlib2DArray(OpenCvSharp.Mat frame)
        {
            // https://github.com/takuya-takeuchi/DlibDotNet/issues/213
            Array2D<byte> gray = null;   // target Dlib array, it's grayscale, 1 byte per pixel
            int size = frame.Width * frame.Height * frame.ElemSize();
            var array = new byte[size];
            Marshal.Copy(frame.Data, array, 0, size);
            gray = Dlib.LoadImageData<byte>(array, (uint)frame.Height, (uint)frame.Width, (uint)(frame.Width * frame.ElemSize()));

            return gray;
        }

        private int OpenCVDeepLearningDetector(string path)
        {
            // uses emugu library
            //https://medium.com/@vinuvish/face-detection-with-opencv-and-deep-learning-90bff9028fa8
            string prototextPath = @"./Dnn/deploy.prototxt";
            string caffeModelPath = @"./Dnn/res10_300x300_ssd_iter_140000.caffemodel";
            //// load the model;
            using (var net = OpenCvSharp.Dnn.CvDnn.ReadNetFromCaffe(prototxt: prototextPath, caffeModel: caffeModelPath))
            using (OpenCvSharp.Mat image = Cv2.ImRead(path))
            {
                // get the original image size
                OpenCvSharp.Size imageSize = image.Size();
                // the dnn detector works on a 300x300 image;
                // now resize the image for the Dnn dector;
                OpenCvSharp.Size size = new OpenCvSharp.Size(299, 299);
                // set the scalar property to RGB colors, don't know what these values represent.
                OpenCvSharp.Scalar mcvScalar = new OpenCvSharp.Scalar(104.0, 177.0, 123.0);
                using (var blob = OpenCvSharp.Dnn.CvDnn.BlobFromImage(image: image, scaleFactor: 1, size: size, mean: mcvScalar, swapRB: true))
                {
                    net.SetInput(blob, "data");
                    using (OpenCvSharp.Mat detections = net.Forward())
                    {
                        // convert the detected values to a faces object that we can use to
                        // draw rectangles.
                        List<ConfidenceRect> Faces = new List<ConfidenceRect>();
                        //var rows = detections.SizeOfDimension[2];
                        //Array ans = detections.GetData();
                        //for (int n = 0; n < rows; n++)
                        //{
                        //    object confidence = ans.GetValue(0, 0, n, 2);
                        //    object x1 = ans.GetValue(0, 0, n, 3);
                        //    object y1 = ans.GetValue(0, 0, n, 4);
                        //    object x2 = ans.GetValue(0, 0, n, 5);
                        //    object y2 = ans.GetValue(0, 0, n, 6);
                        //    ConfidenceRect cr = new ConfidenceRect(confidence, x1, y1, x2, y2, imageSize);
                        //    if (cr.Confidence > 0)
                        //    {
                        //        Debug.WriteLine($"Confidence {cr.Confidence}");
                        //    }
                        //    if (cr.Confidence > Confidence)
                        //    {
                        //        Faces.Add(cr);
                        //    }
                        //}

                        //// convert to a writeableBitmap
                        //WriteableBitmap writeableBitmap = new WriteableBitmap(ImageSource);

                        //ImageSource = ConvertWriteableBitmapToBitmapImage(writeableBitmap);
                        //OnPropertyChanged("ImageSource");

                        //DrawDnnOnImage?.Invoke(Faces, imageSize);
                        //return Faces.Count.ToString();
                    }
                }
            }
            return 0;
        }

        protected virtual void LoadImage()
        {

        }

        protected virtual void SaveBMI()
        {
            string photolocation = this.ImagePath;
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(ImageSource));
            using (FileStream fs = new FileStream(photolocation, FileMode.Create))
            {
                encoder.Save(fs);
            }
        }

        private void DrawRect(OpenCvSharp.Mat frame, Rect face)
        {
            OpenCvSharp.Cv2.Rectangle(img: frame, rect: face, color: new Scalar(0, 255, 0), thickness: 1);
        }

        private void PupilCenter(OpenCvSharp.Point[][] contours, int ndx, out OpenCvSharp.Point ctr, out int radius)
        {
            int maxArea = 0;
            Rect maxEyeRect = new Rect();
            int area = 0;
            int minx = 0;
            int maxx = 0;
            int miny = 0;
            int maxy = 0;

            for (int n = 0; n < contours[ndx].Length; n++)
            {
                int x = contours[ndx][n].X;
                int y = contours[ndx][n].Y;
                if (minx == 0) minx = x;
                if (miny == 0) miny = y;
                if (maxx == 0) maxx = x;
                if (maxy == 0) maxy = y;
                if (x < minx) minx = x;
                if (x > maxx) maxx = x;
                if (y < miny) miny = y;
                if (y > maxy) maxy = y;
            }
            area = (maxx - minx) * (maxy - miny);

            if (area > maxArea)
            {
                maxArea = area;
                maxEyeRect.X = minx;
                maxEyeRect.Y = miny;
                maxEyeRect.Width = maxx - minx;
                maxEyeRect.Height = maxy - miny;
            }

            ctr = new OpenCvSharp.Point(maxEyeRect.X + (int)(maxEyeRect.Width / 2), maxEyeRect.Y + (int)(maxEyeRect.Height / 2));
            radius = maxEyeRect.Width > maxEyeRect.Height ? maxEyeRect.Width : maxEyeRect.Height;
            return;
        }

        private OpenCvSharp.Point DetectCenterOfEye(OpenCvSharp.Mat frame, Rect boundingBox)
        {
            // Gets location relative to frame
            // https://pysource.com/2019/01/04/eye-motion-tracking-opencv-with-python/
            //extract the eye region by coordinates.
            OpenCvSharp.Mat Roi = frame.Clone(boundingBox);
            if (Roi.Rows == 0 && Roi.Cols == 0)
                return new OpenCvSharp.Point();

            // convert to grayscale
            OpenCvSharp.Mat grayRoi = new OpenCvSharp.Mat();
            Cv2.CvtColor(Roi, grayRoi, ColorConversionCodes.BGR2GRAY);
            // get rid of surrounding noise to isolate pupil
            OpenCvSharp.Mat grayRoi2 = new OpenCvSharp.Mat();
            Cv2.GaussianBlur(grayRoi, grayRoi2, new OpenCvSharp.Size(7, 7), 0);

            // try get rid of more noise
            OpenCvSharp.Mat threshold = new OpenCvSharp.Mat();
            Cv2.Threshold(grayRoi2, threshold, 5, 255, ThresholdTypes.BinaryInv);    //Cv2.Thresh_Binary

            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hi;
            Cv2.FindContours(threshold, out contours, out hi, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

            // get the contour with the largest area, the pupil of eye.
            OpenCvSharp.Point CenterOfEye = new OpenCvSharp.Point();
            int EyeRadius = 0;
            for (int i = 0; i < contours.Length; i++)
            {
                OpenCvSharp.Point thisEye;
                int thisEyeRadius = 0;
                PupilCenter(contours, i, out thisEye, out thisEyeRadius);
                if (i == 0 || thisEyeRadius > EyeRadius)
                {
                    CenterOfEye = thisEye;
                    EyeRadius = thisEyeRadius;
                }
            }
            if (EyeRadius > 0)
            {
                Scalar color = new Scalar(0, 0, 255);
                // locate center relative to the frame
                int FrameX = CenterOfEye.X + EyeRadius + boundingBox.X;
                int FrameY = CenterOfEye.Y + EyeRadius + boundingBox.Y;
                OpenCvSharp.Point ctr = new OpenCvSharp.Point(FrameX, FrameY);
                Cv2.Circle(frame, ctr, EyeRadius, color: color, thickness: 2);

                // draw vert line thru center of pupil
                Cv2.Line(img: frame, pt1: new OpenCvSharp.Point(FrameX, boundingBox.Y),
                                pt2: new OpenCvSharp.Point(FrameX, boundingBox.Y + boundingBox.Height), color, 1);
                //// draw horiz line thru center of pupil
                Cv2.Line(img: frame, pt1: new OpenCvSharp.Point(boundingBox.X, FrameY),
                                pt2: new OpenCvSharp.Point(boundingBox.X + boundingBox.Width, FrameY), color, 1);
            }

            Roi.Dispose();
            grayRoi.Dispose();
            grayRoi2.Dispose();
            threshold.Dispose();
            return CenterOfEye;
        }

        private Rect BoundingBoxAroundEye(EyePoints eye, int buffer = 0)
        {
            if (eye.Points.Length < 6)
                return new Rect();
            // get upper left corner
            int upperLeftX = eye.Points[0].X - buffer;
            List<OpenCvSharp.Point> lst = new List<OpenCvSharp.Point>();
            lst.Add(eye.Points[1]);
            lst.Add(eye.Points[2]);
            int upperLeftY = lst.Min(n => n.Y) - buffer;

            int lowerRightX = eye.Points[3].X + buffer;
            lst.Clear();
            lst.Add(eye.Points[4]);
            lst.Add(eye.Points[5]);
            int lowerRightY = lst.Max(n => n.Y) + buffer;

            Rect boundingBox = new Rect(upperLeftX, upperLeftY, lowerRightX - upperLeftX, lowerRightY - upperLeftY);
            return boundingBox;
        }

        private void InitializeEyes(FullObjectDetection landmarks, EyePoints leftEye, EyePoints rightEye)
        {
            leftEye.Init(landmarks);
            rightEye.Init(landmarks);
        }

        private DlibDotNet.Rectangle ConvertToDlib(Rect rect)
        {
            DlibDotNet.Rectangle dlibRect = new DlibDotNet.Rectangle()
            {
                Left = rect.X,
                Top = rect.Y,
                Right = rect.X + rect.Width,
                Bottom = rect.Y + rect.Height
            };

            return dlibRect;
        }
    }
}
