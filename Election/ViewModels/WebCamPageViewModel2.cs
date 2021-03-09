using DlibDotNet;
using Election.Models;
using ElectionModels;
using ElectionModels.Misc;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Election.ViewModels
{
    // works with OpenCvSharp
    public class WebCamPageViewModel2 : BaseViewModel
    {
        private VideoCapture cap = null;
        public ICommand LoadedCommand { get; set; }
        public ICommand StopVideoCommand { get; set; }
        public ICommand StartVideoCommand { get; set; }
        private double scaleFactor = 1.3;
        private int minNeighbors = 2;
        private int frameskip = 1;
        private CascadeClassifier faceClassifier { get; set; }
        private System.Collections.Generic.Queue<Mat> FrameQueue { get; set; }
        private string haarcascade_frontalface_alt;
        private string CaffeModel;
        private string PrototextPath;
        private static string shapePredictorDataFile = @"./Dlib/shape_predictor_68_face_landmarks.dat";

        // https://ourcodeworld.com/articles/read/761/how-to-take-snapshots-with-the-web-camera-with-c-sharp-using-the-opencvsharp-library-in-winforms
        //private Thread camera;
        //private Mat frame;
        //private VideoCapture capture;
        //private bool isCameraRunning = false;
        //private BitmapImage image;

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

        private BackgroundWorker webCamBackgroundWorker;

        public WebCamPageViewModel2() : base()
        {
            CaffeModel = PutFileName("res10_300x300_ssd_iter_140000.caffemodel", "ElectionModels", "Dnn");
            PrototextPath = PutFileName("deploy.prototxt", "ElectionModels", "Dnn");
            haarcascade_frontalface_alt = PutFileName("haarcascade_frontalface_alt.xml", "ElectionModels", "Haarcascade");
            faceClassifier = new CascadeClassifier(haarcascade_frontalface_alt);
            LoadedCommand = new Command(OnLoaded);
            StopVideoCommand = new Command(OnStopVideo);
            StartVideoCommand = new Command(OnStartVideo);
            InitializeBackgroundWorker();
            FrameQueue = new System.Collections.Generic.Queue<Mat>();
            IsNotRunning = true;
        }

        private void InitializeBackgroundWorker()
        {
            webCamBackgroundWorker = new BackgroundWorker();
            webCamBackgroundWorker.DoWork += new DoWorkEventHandler(WebCamBackgroundWorker_DoWork);
            webCamBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WebCamBackgroundWorker_RunWorkerCompleted);
            webCamBackgroundWorker.ProgressChanged += WebCamBackgroundWorker_ProgressChanged;
            webCamBackgroundWorker.WorkerReportsProgress = true;
            webCamBackgroundWorker.WorkerSupportsCancellation = true;
        }

        private void WebCamBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //camera = new Thread(new ThreadStart(CaptureCameraCallbacK));
            BackgroundWorker worker = sender as BackgroundWorker;
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                cap.Release();
            }
            else
            {
                StartWebCam(worker);
            }
        }

        private void StartWebCam(BackgroundWorker worker = null)
        {
            if (cap == null)
            {
                cap = new VideoCapture(0);
            }
            if (!cap.Open(0))
            {
                return;
            }
            OpenCvSharp.Cv2.NamedWindow("Video", WindowMode.AutoSize);
            int cnt = 0;
            Mat frame = new Mat();
            EyePoints rightEye = new EyePoints(true);
            EyePoints leftEye = new EyePoints(false);
            IsRunning = true;
            while (IsRunning)
            {
                bool result = cap.Read(frame);
                if (!result)
                {
                    worker.CancelAsync();
                    IsRunning = false;
                }
                if (frame != null && (frame.Rows * frame.Cols > 0))
                {
                    cnt++;
                    if (cnt % frameskip == 0)
                    {
                        FrameQueue.Enqueue(frame);
                        cnt = 0;
                    }
                }
                while (FrameQueue.Count > 0)
                {
                    Mat que = FrameQueue.Dequeue();
                    Rect[] faces = GetFaces(que, 1);
                    for (int i = 0; i < faces.Length; i++)
                    {
                        //GetFaceInRect(faces[i], que, i);
                        Scalar eyecolor = new Scalar(0, 0, 255);
                        Array2D<byte> gray = ConvertMatToDlib2DArray(que);
                        FullObjectDetection landmarks = predictor.Detect(gray, ConvertToDlib(faces[i]));
                        InitializeEyes(landmarks, leftEye, rightEye);
                        //DrawEye(que, landmarks, leftEye);
                        //DrawEye(que, landmarks, rightEye);
                        Rect leftboundingBox = BoundingBoxAroundEye(leftEye, 0);
                        DrawRect(que, leftboundingBox);
                        OpenCvSharp.Point centerOfLeftEye = DetectCenterOfEye(que, leftboundingBox);
                        centerOfLeftEye.X += leftboundingBox.X;

                        Rect rightboundingBox = BoundingBoxAroundEye(rightEye, 0);
                        DrawRect(que, rightboundingBox);
                        OpenCvSharp.Point centerOfRightEye = DetectCenterOfEye(que, rightboundingBox);
                        centerOfRightEye.X += rightboundingBox.X;

                        EyeDirection leftEyeDirection = leftEye.GetEyePosition(centerOfLeftEye);
                        EyeDirection rightEyeDirection = rightEye.GetEyePosition(centerOfRightEye);

                        EyeDirection eyeDirection = EyeDirection.unknown;
                        if (leftEyeDirection == EyeDirection.center || rightEyeDirection == EyeDirection.center) eyeDirection = EyeDirection.center;
                        else if (leftEyeDirection == EyeDirection.left) eyeDirection = EyeDirection.left;
                        else if (rightEyeDirection == EyeDirection.right) eyeDirection = EyeDirection.right;

                        OpenCvSharp.Point position = new OpenCvSharp.Point(50, 50);
                        Cv2.PutText(img: que, text: eyeDirection.ToDisplay(), org: position, fontFace: HersheyFonts.HersheySimplex, fontScale: 2, new Scalar(0, 0, 255));

                    }
                    //BitmapImage bmi = ConvertToBMI(frame, cnt, "D:/junk/TestCamImages");
                    if (worker != null)
                    {
                        //worker.ReportProgress(cnt, bmi);      
                        try
                        {
                            OpenCvSharp.Cv2.ImShow("Video", que);
                            int key = Cv2.WaitKey(10);   // as in 10 milliseconds
                            if (key == 27)
                            {
                                worker.CancelAsync();
                                IsRunning = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            string msg = ex.Message;
                        }
                    }
                    if (worker.CancellationPending)
                    {
                        Cv2.DestroyWindow("Video");
                        break;
                    }
                }
            }
        }

        private OpenCvSharp.Point DetectCenterOfEye(Mat frame, Rect boundingBox)
        {
            // Gets location relative to frame
            // https://pysource.com/2019/01/04/eye-motion-tracking-opencv-with-python/
            //extract the eye region by coordinates.
            Mat Roi = frame.Clone(boundingBox);
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

        private Rect[] GetFaces(Mat frame, int maxFaces)
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

        //private Rect BoundingBoxAroundEyes(EyePoints leftEye, EyePoints rightEye, int buffer = 0)
        //{
        //    // get upper left corner
        //    int upperLeftX = leftEye.Points[0].X - buffer;
        //    List<OpenCvSharp.Point> lst = new List<OpenCvSharp.Point>();
        //    lst.Add(leftEye.Points[1]);
        //    lst.Add(leftEye.Points[2]);
        //    lst.Add(rightEye.Points[1]);
        //    lst.Add(rightEye.Points[2]);
        //    int upperLeftY = lst.Min(n => n.Y) - buffer;

        //    int lowerRightX = rightEye.Points[3].X + buffer;
        //    lst.Clear();
        //    lst.Add(leftEye.Points[4]);
        //    lst.Add(leftEye.Points[5]);
        //    lst.Add(rightEye.Points[4]);
        //    lst.Add(rightEye.Points[5]);

        //    int lowerRightY = lst.Max(n => n.Y) + buffer;

        //    Rect boundingBox = new Rect(upperLeftX, upperLeftY, lowerRightX - upperLeftX, lowerRightY - upperLeftY);
        //    return boundingBox;
        //}

        private void WebCamBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null)
            {
                FrameQueue.Clear();
                //cap.Release();
                IsRunning = false;
            }
            else
            {
                //if (cap != null)
                //{
                //    cap.Release();
                //}
            }
        }

        private void WebCamBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is BitmapImage bmi)
            {
                // this doesn't work!
                //ImageSource = bmi;
            }
        }

        public void OnLoaded(object p)
        {
            //StartWebCam();
        }

        private void OnStopVideo(object p)
        {
            if (webCamBackgroundWorker.WorkerSupportsCancellation)
            {
                webCamBackgroundWorker.CancelAsync();
                cap.Dispose();
                cap = null;
            }
        }

        private void OnStartVideo(object p)
        {
            if (!webCamBackgroundWorker.IsBusy)
            {
                webCamBackgroundWorker.RunWorkerAsync();
            }
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

        private BitmapImage ConvertToBMI(Mat frame, int cnt, string folder = null)
        {
            BitmapSource bitmapSource = OpenCvSharp.WpfExtensions.BitmapSourceConverter.ToBitmapSource(frame);
            return ConvertToBMI(bitmapSource, cnt, folder);
        }

        private BitmapImage ConvertToBMI(BitmapSource bms, int cnt, string folder = null)
        {
            BitmapImage bmi = new BitmapImage();

            using (MemoryStream ms = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bms));
                encoder.Save(ms);

                if (!string.IsNullOrEmpty(folder))
                {
                    string filepath = string.Format(@"{0}\image{1}.png", folder, cnt);
                    SaveToDisk(ms, filepath);
                }

                ms.Position = 0;
                bmi.BeginInit();
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.StreamSource = ms;
                bmi.EndInit();
            }
            return bmi;
        }

        private void SaveToDisk(MemoryStream ms, string filePath)
        {
            using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
            {
                ms.WriteTo(fileStream);
            }
        }

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
