using ElectionModels;
using OneVote.Models;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace OneVote.ViewModels
{
    // check out https://stackoverflow.com/questions/56096061/how-to-integrate-xamarin-android-with-opencv

    public class OpenCVPageViewModel : BaseViewModel
    {
        private CascadeClassifier faceClassifier { get; set; }

        private readonly string haarcascade_frontalface_alt;
        private readonly string CaffeModel;
        private readonly string PrototextPath;
        private readonly string ShapePredictorDataFile;
        private readonly double scaleFactor = 1.3;
        private readonly int minNeighbors = 2;
        private readonly int frameskip = 1;

        private Queue<Mat> FrameQueue { get; set; }
        private readonly BackgroundWorker webCamBackgroundWorker;

        private ImageSource imgSource;
        public ImageSource ImgSource
        {
            get { return imgSource; }
            set
            {
                if (imgSource != value)
                {
                    imgSource = value;
                    OnPropertyChanged("ImgSource");
                }
            }
        }

        private bool facesFound;
        public bool FacesFound
        {
            get { return facesFound; }
            set
            {
                if (facesFound != value)
                {
                    facesFound = value;
                    OnPropertyChanged("FacesFound");
                }
            }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                if (message != value)
                {
                    message = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        public OpenCVPageViewModel() : base()
        {
            Message = "Hello World";
            ShapePredictorDataFile = PutFileName("shape_predictor_68_face_landmarks.dat", "ElectionModels", "Dlib");
            CaffeModel = PutFileName("res10_300x300_ssd_iter_140000.caffemodel", "ElectionModels", "Dnn");
            PrototextPath = PutFileName("deploy.prototxt", "ElectionModels", "Dnn");
            haarcascade_frontalface_alt = PutFileName("haarcascade_frontalface_alt.xml", "ElectionModels", "Haarcascade");           
        }

        public async Task StartWebCam()
        {
            FacesFound = false;
            while (!FacesFound)
            {
                FileResult photo = await MediaPicker.PickPhotoAsync();

                ImgSource = ImageSource.FromFile(photo.FullPath);
                //using (var stream = await photo.OpenReadAsync())
                //{
                //    Mat frame = OpenCvSharp.Cv2.ImRead(photo.FullPath);
                //    if (frame != null)
                //    {
                //        faceClassifier = new CascadeClassifier(haarcascade_frontalface_alt);
                //        OpenCvSharp.Rect[] faces = GetFaces(frame, 1);
                //        FacesFound = faces.Length != 1;
                //        Message = FacesFound ? null : "No faces or more that one face found";
                //    }
                //}

            }



            //https://medium.com/analytics-vidhya/face-recognition-using-knn-open-cv-9376e7517c9f
            //faceClassifier = new CascadeClassifier(haarcascade_frontalface_alt);
            //if (!File.Exists(PrototextPath) || !File.Exists(CaffeModel) || !File.Exists(ShapePredictorDataFile))
            //    return;

            try
            {
                //OpenCvSharp.Native.Capture ncap = OpenCvSharp.Android.AndroidCapture.New(1);
                //ncap.CaptureStarted += (s,a) =>
                //{

                //};
                //ncap.CaptureStopped += (s, a) =>
                //{

                //};

                //ncap.FrameReady += (s, a) =>
                //{
                    
                //};


                // VideoCapture cap = new VideoCapture(0);
                //Mat frame = new Mat();
                //EyePoints leftEye = new EyePoints(true);
                //EyePoints rightEye = new EyePoints(false);
                //bool Cancelled = false;
                //while (!Cancelled)
                //{
                //    if (cap.Read(frame))
                //    {
                //        //if (frame.Rows == 0 || frame.Cols == 0)
                //        //    continue;

                //        //OpenCvSharp.Rect[] faces = GetFaces(frame, 1);
                //        //for (int i = 0; i < faces.Length; i++)
                //        //{
                //        //    //GetFaceInRect(faces[i], que, i);


                //        //}

                //        try
                //        {
                //            if (frame != null)
                //            {
                //                OpenCvSharp.Cv2.ImShow("Video", frame);
                //                Cv2.WaitKey(1);
                //            }
                //        }
                //        catch (Exception ex)
                //        {
                //            string msg = ex.Message;
                //            Cancelled = true;
                //        }    
                //    }        
                //}
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            await Task.CompletedTask;
        }

        private OpenCvSharp.Rect[] GetFaces(Mat frame, int maxFaces)
        {
            using (var gray = new Mat())
            {
                OpenCvSharp.Rect[] faces = new OpenCvSharp.Rect[0];
                Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);

                // Detect faces
                faces = faceClassifier.DetectMultiScale(gray, scaleFactor, minNeighbors, HaarDetectionType.ScaleImage);
                List<System.Drawing.Rectangle> rfaces = new List<System.Drawing.Rectangle>();
                foreach (OpenCvSharp.Rect rect in faces)
                {
                    System.Drawing.Rectangle r = new System.Drawing.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
                    rfaces.Add(r);
                }

                List<OpenCvSharp.Rect> largest = faces?.OrderByDescending(n => n.Width * n.Height).Take(maxFaces).ToList();
                //DrawOnImage?.Invoke(rfaces.ToArray(), new System.Drawing.Size(result.Width, result.Height));
                return largest.ToArray();
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
    }
}
