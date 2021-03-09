using DlibDotNet;
using Election.Models;
using ElectionModels;
using ElectionModels.Misc;
using OpenCvSharp;
using OpenCvSharp.Dnn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Election.ViewModels
{
    public class WebCamPageViewModel : BaseViewModel
    {
        private VideoCapture cap = null;
        public ICommand LoadedCommand { get; set; }
        public ICommand StopVideoCommand { get; set; }
        public ICommand StartVideoCommand { get; set; }
        private readonly double scaleFactor = 1.3;
        private readonly int minNeighbors = 2;
        private CascadeClassifier faceClassifier { get; set; }

        private readonly string haarcascade_frontalface_alt;
        private readonly string CaffeModel;
        private readonly string PrototextPath;
        private readonly static string shapePredictorDataFile = @"./Dlib/shape_predictor_68_face_landmarks.dat";

        // https://ourcodeworld.com/articles/read/761/how-to-take-snapshots-with-the-web-camera-with-c-sharp-using-the-opencvsharp-library-in-winforms
        private Thread camera;
        private Mat frame;
        private readonly VideoCapture capture;

        public static ShapePredictor predictor = ShapePredictor.Deserialize(shapePredictorDataFile);
        public Func<System.Windows.Controls.Image> GetImageControl { get; set; }

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

        private bool isRunning;
        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    OnPropertyChanged("IsRunning");
                    IsNotRunning = !value;
                }
            }
        }

        private bool isNotRunning;
        public bool IsNotRunning
        {
            get { return isNotRunning; }
            set
            {
                if (isNotRunning != value)
                {
                    isNotRunning = value;
                    OnPropertyChanged("IsNotRunning");
                }
            }
        }


        public WebCamPageViewModel() : base()
        {
            CaffeModel = PutFileName("res10_300x300_ssd_iter_140000.caffemodel", "ElectionModels", "Dnn");
            PrototextPath = PutFileName("deploy.prototxt", "ElectionModels", "Dnn");
            haarcascade_frontalface_alt = PutFileName("haarcascade_frontalface_alt.xml", "ElectionModels", "Haarcascade");
            faceClassifier = new CascadeClassifier(haarcascade_frontalface_alt);
            LoadedCommand = new Command(OnLoaded);
            StopVideoCommand = new Command(OnStopVideo);
            StartVideoCommand = new Command(OnStartVideo);

            IsNotRunning = true;
        }


        private void CaptureCameraCallbacK()
        {
            StartWebCam();
            //ImageSource = ConvertToBMI(frame, 0);
        }

        private void CaptureCamera()
        {
            camera = new Thread(new ThreadStart(CaptureCameraCallbacK));
            camera.Start();
        }

        private void StartWebCam()
        {
            bool useHaarcascade = true;
            if (cap == null)
            {
                cap = new VideoCapture(0);
            }
            if (!cap.Open(0))
            {
                return;
            }
            OpenCvSharp.Cv2.NamedWindow("Video", WindowMode.AutoSize);

            EyePoints rightEye = new EyePoints(true);
            EyePoints leftEye = new EyePoints(false);
            IsRunning = true;
            while (IsRunning)
            {
                frame = new Mat();
                bool result = cap.Read(frame);
                if (!result)
                {
                    IsRunning = false;
                    break;
                }

                Rect[] faces = useHaarcascade ? GetHaarcascadeFaces(frame, 1) : GetDnnFaces(frame, 1);
                if (faces == null)
                    continue;

                for (int i = 0; i < faces.Length; i++)
                {
                    //GetFaceInRect(faces[i], que, i);
                    Scalar eyecolor = new Scalar(0, 0, 255);
                    Array2D<byte> gray = ConvertMatToDlib2DArray(frame);
                    FullObjectDetection landmarks = predictor.Detect(gray, ConvertToDlib(faces[i]));
                    InitializeEyes(landmarks, leftEye, rightEye);
                    //DrawEye(que, landmarks, leftEye);
                    //DrawEye(que, landmarks, rightEye);
                    Rect leftboundingBox = BoundingBoxAroundEye(leftEye,0);
                    DrawRect(frame, leftboundingBox);
                    OpenCvSharp.Point centerOfLeftEye = DetectCenterOfEye(frame, leftboundingBox);
                    centerOfLeftEye.X += leftboundingBox.X;

                    Rect rightboundingBox = BoundingBoxAroundEye(rightEye, 0);
                    DrawRect(frame, rightboundingBox);
                    OpenCvSharp.Point centerOfRightEye = DetectCenterOfEye(frame, rightboundingBox);
                    centerOfRightEye.X += rightboundingBox.X;

                    EyeDirection leftEyeDirection = leftEye.GetEyePosition(centerOfLeftEye);
                    EyeDirection rightEyeDirection = rightEye.GetEyePosition(centerOfRightEye);

                    EyeDirection eyeDirection = EyeDirection.unknown;
                    if (leftEyeDirection == EyeDirection.center || rightEyeDirection == EyeDirection.center) eyeDirection = EyeDirection.center;
                    else if (leftEyeDirection == EyeDirection.left) eyeDirection = EyeDirection.left;
                    else if (rightEyeDirection == EyeDirection.right) eyeDirection = EyeDirection.right;

                    OpenCvSharp.Point position = new OpenCvSharp.Point(50, 50);
                    Cv2.PutText(img: frame, text: eyeDirection.ToDisplay(), org: position, fontFace: HersheyFonts.HersheySimplex, fontScale: 2, new Scalar(0, 0, 255));

                }
    
                try
                {
                    OpenCvSharp.Cv2.ImShow("Video", frame);
                    int key = Cv2.WaitKey(10);   // as in 10 milliseconds
                    if (key == 27)
                    {
                        IsRunning = false;
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
                frame.Dispose();
            }
        }

        private OpenCvSharp.Point DetectCenterOfEye(Mat frame, Rect boundingBox )
        {
            // Gets location relative to frame
            // https://pysource.com/2019/01/04/eye-motion-tracking-opencv-with-python/
            //extract the eye region by coordinates.
            Mat Roi = frame.Clone(boundingBox);
            if (Roi.Rows == 0 && Roi.Cols == 0)
                return new OpenCvSharp.Point();

            // convert to grayscale
            Mat grayRoi = new Mat();
            Cv2.CvtColor(Roi, grayRoi, ColorConversionCodes.BGR2GRAY);
            // get rid of surrounding noise to isolate pupil
            Mat grayRoi2 = new Mat();
            Cv2.GaussianBlur(grayRoi, grayRoi2, new OpenCvSharp.Size(7, 7), 0);

            // try get rid of more noise
            Mat threshold = new Mat();
            Cv2.Threshold(grayRoi2, threshold, 5, 255, ThresholdTypes.BinaryInv);    //Cv2.Thresh_Binary

            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hi;
            Cv2.FindContours(threshold, out contours, out hi, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);  
            
            // get the contour with the largest area, the pupil of eye.
            OpenCvSharp.Point CenterOfEye = new OpenCvSharp.Point();
            int EyeRadius = 0;
            for (int i=0; i<contours.Length; i++)
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

        private Rect[] GetHaarcascadeFaces(Mat frame, int maxFaces)
        {
            using (var gray = new Mat())
            {
                Rect[] faces = new Rect[0];
                Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);

                // Detect faces
                faces = faceClassifier.DetectMultiScale(gray, scaleFactor, minNeighbors, HaarDetectionType.ScaleImage);
                List<System.Drawing.Rectangle> rfaces = new List<System.Drawing.Rectangle>();
                foreach (Rect rect in faces)
                {
                    System.Drawing.Rectangle r = new System.Drawing.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
                    rfaces.Add(r);
                }

                List<Rect> largest = faces?.OrderByDescending(n => n.Width * n.Height).Take(maxFaces).ToList();
                //DrawOnImage?.Invoke(rfaces.ToArray(), new System.Drawing.Size(result.Width, result.Height));
                return largest.ToArray();
            }
        }

        private Rect[] GetDnnFaces(Mat frame, int maxFaces)
        {
            //double ScaleFactor = 1.28;
            double Confidence = 0.9;
            //int Neighbors = 2;

            System.Drawing.Size imageSize = new System.Drawing.Size(frame.Width, frame.Height);
            string prototextPath = @"./Dnn/deploy.prototxt";
            string caffeModelPath = @"./Dnn/res10_300x300_ssd_iter_140000.caffemodel";
            if (!File.Exists(prototextPath) || !File.Exists(caffeModelPath))
                return null;
            // load the model;
            //var net = DnnInvoke.ReadNetFromCaffe(prototxt: prototextPath, caffeModel: caffeModelPath);
            var net = CvDnn.ReadNetFromCaffe(prototxt: prototextPath, caffeModel: caffeModelPath);
            // get the image

            // the dnn detector works on a 300x300 image;
            int targetWidth = 300;
            int targetHeight = 300;
            // now resize the image for the Dnn dector;

            Mat image = frame.Resize(new OpenCvSharp.Size(targetWidth, targetHeight), 0, 0, InterpolationFlags.Lanczos4);
            //    (targetWidth, targetHeight, Emgu.CV.CvEnum.Inter.Lanczos4);
            OpenCvSharp.Size size = new OpenCvSharp.Size(targetWidth, targetHeight);
            var blob = CvDnn.BlobFromImage(image: image, scaleFactor: 1, size: size);
                //CvDnn.BlobFromImage(image: image, scaleFactor: ScaleFactor, size: size, mean: mcvScalar, swapRB: true);
                net.SetInput(blob);
            Mat detections = net.Forward();
            List<ConfidenceRect> faces = new List<ConfidenceRect>();
            for (int i = 0; i < detections.Rows; i++)
            {
                float confidence = detections.At<float>(1, 2);
                if (confidence < Confidence)
                {
                    continue;
                }
                int x_left_bottom = (int)(detections.At<float>(i, 3));

                int y_left_bottom = (int)(detections.At<float>(i, 4));

                int x_right_top = (int)(detections.At<float>(i, 5));

                int y_right_top = (int)(detections.At<float>(i, 6));

                faces.Add(new ConfidenceRect(confidence, x_left_bottom,
                    y_left_bottom,
                    x_right_top - x_left_bottom,
                    y_right_top - y_left_bottom,
                    imageSize));                
            }
            if (faces.Count > 0)
            {
                System.Drawing.Rectangle ans = faces.OrderBy(n => n.Confidence).First().AsRectangle();
                return new OpenCvSharp.Rect[] { new OpenCvSharp.Rect(ans.X, ans.Y, ans.Width, ans.Height) };
            }
            image.Dispose();
            detections.Dispose();
            return null;
        }

        // draws box around face
        private void GetFaceInRect(Rect face, Mat frame, int cnt)
        {
            Mat image = frame.Clone(face);
            DrawRect(frame, face);
            return;
        }

        private void DrawRect(Mat frame, Rect face)
        {
            OpenCvSharp.Cv2.Rectangle(img: frame, rect: face, color: new Scalar(0, 255, 0), thickness: 1);
        }

        private byte[] Get2DArrayFromMat(Mat frame)
        {
            int arraysize = frame.Cols * frame.Rows;
            byte[] data = new byte[arraysize];
            var ans = frame.GetArray<byte>(out data);

            return data;
        }

        private void InitializeEyes(FullObjectDetection landmarks, EyePoints leftEye, EyePoints rightEye)
        {
            leftEye.Init(landmarks);
            rightEye.Init(landmarks);
        }

        private void DrawEye(Mat frame, FullObjectDetection landmarks, EyePoints eye)
        {
            eye.DrawPoints(frame);
            eye.DrawBaseLine(frame);
            eye.VerticalLine(frame);        
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

        public void OnLoaded(object p)
        {
            //StartWebCam();
        }

        private void OnStopVideo(object p)
        {
            if (IsRunning)
            {
                cap.Release();
                IsRunning = false;

            }
        }

        private void OnStartVideo(object p)
        {
            CaptureCamera();
            IsRunning = true;
        }

        private Array2D<byte> ConvertMatToDlib2DArray(Mat frame)
        {
            // https://github.com/takuya-takeuchi/DlibDotNet/issues/213
            Array2D<byte> gray = null;   // target Dlib array, it's grayscale, 1 byte per pixel
            int size = frame.Width * frame.Height * frame.ElemSize();
            var array = new byte[size];
            Marshal.Copy(frame.Data, array, 0, size);
            gray = Dlib.LoadImageData<byte>(array, (uint)frame.Height, (uint)frame.Width, (uint)(frame.Width * frame.ElemSize()));

            return gray;
        }

        //public Array2D<RgbPixel> LoadFromSoftwareBitmap(SoftwareBitmap Bitmap)
        //{
        //    uint BufferSize = (uint)(Bitmap.PixelHeight * Bitmap.PixelWidth * 4);
        //    byte[] DlibImageArray = new byte[BufferSize];
        //    Windows.Storage.Streams.Buffer buffer = new Windows.Storage.Streams.Buffer(BufferSize);
        //    Bitmap.CopyToBuffer(buffer);
        //    using (var Reader = DataReader.FromBuffer(buffer))
        //    {
        //        Reader.ReadBytes(DlibImageArray);
        //    }
        //    return Dlib.LoadImageData<RgbPixel>(ImagePixelFormat.Bgra, DlibImageArray, (uint)Bitmap.PixelHeight, (uint)Bitmap.PixelWidth, (uint)Bitmap.PixelWidth * 4);
        //}

        //private BitmapImage ConvertToBMI(Mat frame, int cnt, string folder = null)
        //{
        //    BitmapSource bitmapSource = OpenCvSharp.WpfExtensions.BitmapSourceConverter.ToBitmapSource(frame);
        //    return ConvertToBMI(bitmapSource, cnt, folder);
        //}

        //private BitmapImage ConvertToBMI(BitmapSource bms, int cnt, string folder = null)
        //{
        //    BitmapImage bmi = new BitmapImage();

        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        PngBitmapEncoder encoder = new PngBitmapEncoder();
        //        encoder.Frames.Add(BitmapFrame.Create(bms));
        //        encoder.Save(ms);

        //        if (!string.IsNullOrEmpty(folder))
        //        {
        //            string filepath = string.Format(@"{0}\image{1}.png", folder, cnt);
        //            SaveToDisk(ms, filepath);
        //        }

        //        ms.Position = 0;
        //        bmi.BeginInit();
        //        bmi.CacheOption = BitmapCacheOption.OnLoad;
        //        bmi.StreamSource = ms;
        //        bmi.EndInit();
        //    }
        //    return bmi;
        //}

        //private void SaveToDisk(MemoryStream ms, string filePath)
        //{
        //    using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
        //    {
        //        ms.WriteTo(fileStream);
        //    }
        //}

        private string PutFileName(string filename, string modulename, string foldername)
        {
            string fullpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), filename);
            if (!File.Exists(filename))
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Category)).Assembly;
                string streamName;
                if (!string.IsNullOrEmpty(modulename))
                {
                    if (!string.IsNullOrEmpty(foldername))
                    {
                        streamName = $"{modulename}.{foldername}.{filename}";
                    }
                    else
                    {
                        streamName = $"{modulename}.{filename}";
                    }
                }
                else
                {
                    streamName = $"{filename}";
                }

                Stream stream = assembly.GetManifestResourceStream(streamName);
                string text = "";
                using (var reader = new System.IO.StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                    File.WriteAllText(fullpath, text);
                }
            }
            return fullpath;
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
