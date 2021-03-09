
using Election.Models;
using ElectionModels.Misc;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Dnn;
using OpenCvSharp;
using OpenCvSharp.Dnn;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Threading;
//using Emgu.CV.CvEnum;

namespace Election.ViewModels
{
    public class FaceDetectionViewModel : BaseViewModel
    {
        public Action ClearCanvas { get; set; }

        public Action<Rectangle[], System.Drawing.Size> DrawOnImage { get; set; }
        public Action<List<ConfidenceRect>, System.Drawing.Size> DrawDnnOnImage { get; set; }
        public ICommand OpenFileCommand { get; set; }
        public ICommand RotateCCWCommand { get; set; }
        public ICommand RotateCWCommand { get; set; }
        public ICommand DetectCommand { get; set; }

        private string tempPath { get; set; }
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

        private string openFileDialogIcon;
        public string OpenFileDialogIcon
        {
            get { return openFileDialogIcon; }
            set
            {
                if (openFileDialogIcon != value)
                {
                    openFileDialogIcon = value;
                    OnPropertyChanged("OpenFileDialogIcon");
                }
            }
        }

        private string filePath;
        public string FilePath
        {
            get { return filePath; }
            set
            {
                if (filePath != value)
                {
                    filePath = value;
                    OnPropertyChanged("FilePath");
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

        private double scaleFactor;
        public double ScaleFactor
        {
            get { return scaleFactor; }
            set
            {
                if (scaleFactor != value)
                {
                    scaleFactor = value;
                    OnPropertyChanged("ScaleFactor");
                }
            }
        }

        private int neighbors;
        public int Neighbors
        {
            get { return neighbors; }
            set
            {
                if (neighbors != value)
                {
                    neighbors = value;
                    OnPropertyChanged("Neighbors");
                }
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    OnPropertyChanged("IsBusy");
                }
            }
        }

        private string faceCount;
        public string FaceCount
        {
            get { return faceCount; }
            set
            {
                if (faceCount != value)
                {
                    faceCount = value;
                    OnPropertyChanged("FaceCount");
                }
            }
        }

        private bool useEmGu;
        public bool UseEmgu
        {
            get { return useEmGu; }
            set
            {
                if (useEmGu != value)
                {
                    useEmGu = value;
                    OnPropertyChanged("UseEmgu");
                }
            }
        }

        private double confidence;
        public double Confidence
        {
            get { return confidence; }
            set
            {
                if (confidence != value)
                {
                    confidence = value;
                    OnPropertyChanged("Confidence");
                }
            }
        }

        private bool useHaarCascade;
        public bool UseHaarCascade
        {
            get { return useHaarCascade; }
            set
            {
                if (useHaarCascade != value)
                {
                    useHaarCascade = value;
                    OnPropertyChanged("UseHaarCascade");
                }
            }
        }

        public FaceDetectionViewModel():base()
        {
            tempPath = "d:\\junk\temp.jpg";
            OpenFileDialogIcon = FontAwesome.Grin;
            OpenFileCommand = new Command(OnOpenFile, null);
            RotateCCWCommand = new Command(OnRotateCCW, null);
            RotateCWCommand = new Command(OnRotateCW, null);
            DetectCommand = new Command(OnDetect);
            ScaleFactor = 1.28;
            Neighbors = 2;
            Confidence = 0.9;
            UseHaarCascade = true;
        }

        private static OpenCvSharp.VideoCapture openCvCapture;

        public async Task InitOpenCVVideo()
        {
            new Thread(LoadVideoFrames).Start();
        }

        private void LoadVideoFrames()
        {
            if (openCvCapture == null)
            {
                openCvCapture = new OpenCvSharp.VideoCapture(0);
            }
            OpenCvSharp.Mat frame = new OpenCvSharp.Mat();
            bool readingVideo = true;
            int cnt = 0;
            while (readingVideo)
            {
                if (openCvCapture.Read(frame))
                {
                    cnt++;
                    frame.SaveImage(@"d:\junk\testCamImages\image{cnt}.png");
                    byte[] imagearray = frame.ImEncode(".png");
                    BitmapImage bmi = new BitmapImage();
                    using (MemoryStream ms = new MemoryStream(imagearray))
                    {
                        ms.Position = 0;
                        bmi.BeginInit();
                        bmi.CacheOption = BitmapCacheOption.OnLoad;
                        bmi.StreamSource = ms;
                        bmi.EndInit();
                    }

                    
                    //this.ImageSource.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                    //    new Action(() =>
                    //        {
                    //            ImageSource = bmi;
                    //        }));


                    //capture.
                    OpenCvSharp.Cv2.ImShow("video", frame);
                    int key = OpenCvSharp.Cv2.WaitKey(27);
                    if (key == 27)
                    {
                        readingVideo = false;
                    }
                }
            }
        }

        public void OnOpenFile(object p)
        {
            OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false };
            if (ofd.ShowDialog() == true)
            {
                rotation = Rotation.Rotate0;
                FilePath = ofd.FileName;
                LoadImage();              
            }
        }

        private void OnRotateCCW(object p)
        {
            if (string.IsNullOrEmpty(FilePath))
                return;
            switch(Rotation)
            {
                case Rotation.Rotate0: Rotation = Rotation.Rotate270; break;
                case Rotation.Rotate90: Rotation = Rotation.Rotate0; break;
                case Rotation.Rotate180: Rotation = Rotation.Rotate90; break;
                case Rotation.Rotate270: Rotation = Rotation.Rotate180; break;
            }
            LoadImage();
        }

        private void OnRotateCW(object p)
        {
            if (string.IsNullOrEmpty(FilePath))
                return;
            switch (Rotation)
            {
                case Rotation.Rotate0: Rotation = Rotation.Rotate90; break;
                case Rotation.Rotate90: Rotation = Rotation.Rotate180; break;
                case Rotation.Rotate180: Rotation = Rotation.Rotate270; break;
                case Rotation.Rotate270: Rotation = Rotation.Rotate0; break;
            }

            LoadImage();
        }

        private void OnDetect(object p)
        {
            try
            {
                IsBusy = true;
                if (!string.IsNullOrEmpty(FilePath))
                {
                    FaceCount = null;
                    if (UseEmgu)
                    {
                        EmguFaceDetector(FilePath);
                    }
                    else
                    {

                        if (UseHaarCascade)
                        {
                            FaceCount = OpenCVFaceDetector(FilePath);
                        }
                        else
                        {
                            FaceCount = OpenCVDeepLearningDetector(FilePath);
                        }
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void LoadImage()
        {
            BitmapImage bmi = new BitmapImage();
            bmi.BeginInit();
            bmi.CacheOption = BitmapCacheOption.OnLoad;
            bmi.Rotation = this.Rotation;
            bmi.UriSource = new Uri(FilePath, UriKind.Relative);
            bmi.EndInit();
            ImageSource = bmi;
            OnPropertyChanged("ImageSource");
            ClearCanvas?.Invoke();
            if (bmi.Height > bmi.Width)
            {
#pragma warning disable CS0219 // The variable 'ans' is assigned but its value is never used
                string ans = "Is Landscape";
#pragma warning restore CS0219 // The variable 'ans' is assigned but its value is never used
            }
        }

        private string OpenCVDeepLearningDetector(string path)
        {
            // uses emugu library
            //https://medium.com/@vinuvish/face-detection-with-opencv-and-deep-learning-90bff9028fa8
            string prototextPath = @"./Dnn/deploy.prototxt";
            string caffeModelPath = @"./Dnn/res10_300x300_ssd_iter_140000.caffemodel";
            // load the model;
            var net = CvDnn.ReadNetFromCaffe(prototxt: prototextPath, caffeModel: caffeModelPath);
            // get the image
            OpenCvSharp.Mat image = Cv2.ImRead(path);

            // get the original image size
            OpenCvSharp.Size imageSize = image.Size();
            // the dnn detector works on a 300x300 image;
            // now resize the image for the Dnn dector;
            OpenCvSharp.Size size = new OpenCvSharp.Size(299, 299);
            image = image.Resize(size);

            // set the scalar property to RGB colors, don't know what these values represent.
            OpenCvSharp.Scalar mcvScalar = new OpenCvSharp.Scalar(104.0, 177.0, 123.0);          
            var blob = CvDnn.BlobFromImage(image: image, scaleFactor: 1, size: size, mean: mcvScalar, swapRB: true);
            net.SetInput(blob);
            OpenCvSharp.Mat detections = net.Forward();
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
            return null; 
        }

        //private string OpenCVDeepLearningDetector(string path)
        //{
        //    // cant get this to work with opencvsharp or emguCV
        //    Emgu.CV.Mat image = Emgu.CV.CvInvoke.Imread(path);

        //    BitmapImage ans = Election.Models.Utils.ConvertToBMI(image, 1, @"d:\junk\TestCamImages");

        //    System.Drawing.Size imageSize = new System.Drawing.Size(image.Width, image.Height);
        //    string prototextPath = @"./Dnn/deploy.prototxt";
        //    string caffeModelPath = @"./Dnn/res10_300x300_ssd_iter_140000.caffemodel";
        //    // load the model;
        //    //var net = DnnInvoke.ReadNetFromCaffe(prototxt: prototextPath, caffeModel: caffeModelPath);
        //    var net = Emgu.CV.Dnn.DnnInvoke.ReadNetFromCaffe(prototxt: prototextPath, caffeModel: caffeModelPath);
        //    // get the image
        //    double aspectRatio = (image.Width * 1.0) / image.Height;
        //    // this is the size of the convolution Neural Network.  either 224x224 or 227x227 or 299x299;
        //    int targetWidth = aspectRatio > 1 ? 299 : (int)(299 / aspectRatio);
        //    int targetHeight = aspectRatio > 1 ? (int)(299 / aspectRatio) : 299;
        //    // now resize the image for the Dnn dector;
        //    //image = GetContentScaled(image, image.Width / 299, image.Height / 299, 0, 0);
        //    //image = image.Resize(new OpenCvSharp.Size(targetWidth, targetHeight));

        //    //ans = Election.Models.Utils.ConvertToBMI(image, 2, @"d:\junk\TestCamImages");
        //    //    (targetWidth, targetHeight, Emgu.CV.CvEnum.Inter.Lanczos4);
        //    //OpenCvSharp.Size size = new OpenCvSharp.Size(targetWidth, targetHeight);
        //    //var blob = CvDnn.BlobFromImage(image: image);
        //    //var blob = CvDnn.BlobFromImage(image: image, scaleFactor: ScaleFactor, size: size, mean: new Scalar(104,177.0,123.0));
        //    var blob = Emgu.CV.Dnn.DnnInvoke.BlobFromImage(image: image, scaleFactor: ScaleFactor, size: new System.Drawing.Size(300, 300),
        //        mean: new MCvScalar(104, 177.0, 123.0));
        //    net.SetInput(blob);
        //    Emgu.CV.Mat detections = net.Forward();
        //    List<ConfidenceRect> faces = new List<ConfidenceRect>();
        //    for (int i = 0; i < detections.Rows; i++)
        //    {
        //        float confidence = detections.At<float>(1, 2);
        //        if (confidence < Confidence)
        //        {
        //            continue;
        //        }
        //        int x_left_bottom = (int)(detections.At<float>(i, 3));

        //        int y_left_bottom = (int)(detections.At<float>(i, 4));

        //        int x_right_top = (int)(detections.At<float>(i, 5));

        //        int y_right_top = (int)(detections.At<float>(i, 6));

        //        new ConfidenceRect(confidence, x_left_bottom,
        //            y_left_bottom,
        //            x_right_top - x_left_bottom,
        //            y_right_top - y_left_bottom,
        //            imageSize);
        //    }
        //    convert the detected values to a faces object that we can use to
        //     draw rectangles.
        //    List<ConfidenceRect> Faces = new List<ConfidenceRect>();
        //    var rows = detections.SizeOfDimension[2];
        //    Array ans = detections.GetData();
        //    for (int n = 0; n < rows; n++)
        //    {
        //        object confidence = ans.GetValue(0, 0, n, 2);
        //        object x1 = ans.GetValue(0, 0, n, 3);
        //        object y1 = ans.GetValue(0, 0, n, 4);
        //        object x2 = ans.GetValue(0, 0, n, 5);
        //        object y2 = ans.GetValue(0, 0, n, 6);
        //        ConfidenceRect cr = new ConfidenceRect(confidence, x1, y1, x2, y2, imageSize);
        //        if (cr.Confidence > 0)
        //        {
        //            Debug.WriteLine($"Confidence {cr.Confidence}");
        //        }
        //        if (cr.Confidence > Confidence)
        //        {
        //            Faces.Add(cr);
        //        }
        //    }

        //    convert to a writeableBitmap
        //    WriteableBitmap writeableBitmap = new WriteableBitmap(ImageSource);

        //    ImageSource = ConvertWriteableBitmapToBitmapImage(writeableBitmap);
        //    OnPropertyChanged("ImageSource");

        //    DrawDnnOnImage?.Invoke(faces, imageSize);
        //    return faces.Count.ToString();
        //    return "0";
        //}

        private string OpenCVFaceDetector(string path)
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
                List<Rectangle> rfaces = new List<Rectangle>();
                foreach (Rect rect in faces)
                {
                    Rectangle r = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
                    rfaces.Add(r);
                }
                DrawOnImage?.Invoke(rfaces.ToArray(), new System.Drawing.Size(result.Width, result.Height));
            }
            result.Dispose();
            return faces.Length.ToString();
        }

        private Emgu.CV.VideoCapture capture;
        Emgu.CV.CascadeClassifier emguFaceClassifier = null;
        private void EmguFaceDetector(string path)
        {
            if (capture == null)
            {
                capture = new Emgu.CV.VideoCapture(0);
            }
            capture.ImageGrabbed += Capture_ImageGrabbed;
            capture.Start();

            emguFaceClassifier = new Emgu.CV.CascadeClassifier(@"./haarcascade/haarcascade_frontalface_alt.xml");

        }

        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                Emgu.CV.Mat frame = new Emgu.CV.Mat();
                if (capture.Retrieve(frame))
                {
                    Emgu.CV.Mat grayFrame = new Emgu.CV.Mat();
                    Emgu.CV.CvInvoke.CvtColor(frame, grayFrame, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);      
   
                    Rectangle[] faces = emguFaceClassifier.DetectMultiScale(grayFrame, ScaleFactor, Neighbors);
                    foreach (var face in faces)
                    {
                        Emgu.CV.CvInvoke.Rectangle(frame, face, new MCvScalar(0, 0, 255));
                    }
                    //ImageSource = ToBitmapSource(currentFrame);
                    //Bitmap bmi = frame.ToBitmap();
                    //ImageSource = ToBitmapImage(bmi);
                }

            }
            catch (Exception ex)
            {

            }
        }

        //BitmapImage
        public BitmapImage ConvertWriteableBitmapToBitmapImage(WriteableBitmap wbm)
        {
            BitmapImage bmi = new BitmapImage();
            using (MemoryStream stream = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(wbm));
                encoder.Save(stream);

                bmi.BeginInit();
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.Rotation = this.Rotation;
                bmi.StreamSource = stream;
                bmi.EndInit();
            }
            return bmi;
        }

        private BitmapImage ConvertToBMI(BitmapSource bms)
        {
            BitmapImage bmi = new BitmapImage();

            using (MemoryStream ms = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bms));
                encoder.Save(ms);
                ms.Position = 0;
                bmi.BeginInit();
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.StreamSource = ms;
                bmi.EndInit();
            }
            return bmi;
        }

        private Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        public BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        //private Emgu.CV.Mat GetContentScaled(Emgu.CV.Mat src, double xscale, double yscale, 
        //    double xTrans, double yTrans, Inter interpolation=Inter.Linear)
        //{
        //    var dst = new Emgu.CV.Mat(src.Size, src.Depth, src.NumberOfChannels);
        //    var translateTransform = new Matrix<double>(2, 3)
        //    {
        //        [0, 0] = xscale, // xScale
        //        [1, 1] = yscale, // yScale
        //        [0, 2] = xTrans + (src.Width - src.Width * xscale) / 2.0, //x translation + compensation of  x scaling
        //        [1, 2] = yTrans + (src.Height - src.Height * yscale) / 2.0 // y translation + compensation of y scaling
        //    };
        //    CvInvoke.WarpAffine(src, dst, translateTransform, dst.Size, interpolation);

        //    return dst;
        //}
    }
}
