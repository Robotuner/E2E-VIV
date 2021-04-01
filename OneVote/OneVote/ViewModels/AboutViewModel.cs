using ElectionModels;
using ElectionModels.Misc;
using Newtonsoft.Json;
using OneVote.Models;
using OneVote.Services;
using OneVote.Views;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OneVote.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public Action<string,string> DisplayAlert { get; set; }

        private AboutStatusEnum aboutStatus;
        public AboutStatusEnum AboutStatus
        {
            get { return aboutStatus; }
            set
            {
                if (aboutStatus != value)
                {
                    aboutStatus = value;
                    OnPropertyChanged("AboutStatus");
                    UpdateStatus();
                }
            }
        }

        public ICommand OpenWebCommand { get; }
        private Guid electionId { get; set; }

        private string heading1;
        public string Heading1
        {
            get { return heading1; }
            set
            {
                if (heading1 != value)
                {
                    heading1 = value;
                    OnPropertyChanged("Heading1");
                }
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private string warning;
        public string Warning
        {
            get { return warning; }
            set
            {
                if (warning != value)
                {
                    warning = value;
                    OnPropertyChanged("Warning");
                }
            }
        }

        private string buttonTitle;
        public string ButtonTitle
        {
            get { return buttonTitle; }
            set
            {
                if (buttonTitle != value)
                {
                    buttonTitle = value;
                    OnPropertyChanged("ButtonTitle");
                }
            }
        }

        private string scanQR;
        public string ScanQR
        {
            get { return scanQR; }
            set
            {
                if (scanQR != value)
                {
                    scanQR = value;
                    OnPropertyChanged("ScanQR");
                }
            }
        }

        private string appUrl;
        public string AppUrl
        {
            get { return appUrl; }
            set
            {
                if (appUrl != value)
                {
                    appUrl = value;
                    OnPropertyChanged("AppUrl");
                }
            }
        }

        private bool retryButtonEnabled;
        public bool RetryButtonEnabled
        {
            get { return retryButtonEnabled; }
            set
            {
                if (retryButtonEnabled != value)
                {
                    retryButtonEnabled = value;
                    OnPropertyChanged("RetryButtonEnabled");
                }
            }
        }

        private bool scanQREnabled;
        public bool ScanQREnabled
        {
            get { return scanQREnabled; }
            set
            {
                if (scanQREnabled != value)
                {
                    scanQREnabled = value;
                    OnPropertyChanged("ScanQREnabled");
                }
            }
        }

        //private bool needsSSN;
        //public bool NeedsSSN
        //{
        //    get { return needsSSN; }
        //    set
        //    {
        //        if (needsSSN != value)
        //        {
        //            needsSSN = value;
        //            OnPropertyChanged("NeedsSSN");
        //        }
        //    }
        //}

        //private string ssn1;
        //public string SSN1
        //{
        //    get { return ssn1; }
        //    set
        //    {
        //        if (ssn1 != value)
        //        {
        //            ssn1 = value;
        //            OnPropertyChanged("SSN1");
        //            this.UpdateStatus();
        //        }
        //    }
        //}

        //private string ssn2;
        //public string SSN2
        //{
        //    get { return ssn2; }
        //    set
        //    {
        //        if (ssn2 != value)
        //        {
        //            ssn2 = value;
        //            OnPropertyChanged("SSN2");
        //            this.UpdateStatus();
        //        }
        //    }
        //}

        public AboutViewModel()
        {
            Title = Resource.AboutTitle;
            Heading1 = Resource.AboutHeading1;
            Description = Resource.AboutDescription;
            AboutStatus = AboutStatusEnum.intro;
            ScanQR = Resource.AboutScanQR;
            AppUrl = Resource.AboutLearnMoreUrl;

            OpenWebCommand = new Command(async () => {
                switch (AboutStatus)
                {
                    case AboutStatusEnum.intro:
                        await Browser.OpenAsync(Resource.AboutLearnMoreUrl);
                        break;
                    case AboutStatusEnum.loading:
                    case AboutStatusEnum.retry:
                        await DataService.InitElection(electionId);
                        break;
                    case AboutStatusEnum.hasLoaded:
                        await Shell.Current.GoToAsync("//ItemsPage");
                        break;
                }
            });
        
            MessagingCenter.Subscribe<BlankClass>(this, MessagingEvents.ElectionLoaded, (s) =>
             {
                if (DataService.CanCastBallot())
                {
                    Warning = string.Format(Resource.AboutPollsOpenDateRange, DataService.Election.StartDateLocal.ToString(),
                        DataService.Election.EndDateLocal.ToString());
                     this.ButtonTitle = Resource.AboutVote;
                }
                else
                {
                     Warning = Resource.AboutPollsNotOpen;
                     this.ButtonTitle = Resource.AboutTryAgain;
                }
             });
            MessagingCenter.Subscribe<BlankClass, string>(this, MessagingEvents.ErrorLoadingElection, (s,e) =>
            {
                if (!DataService.CanCastBallot())
                {
                    Warning = e;
                    this.ButtonTitle = Resource.AboutTryAgain;
                }
            });
        }

        private void UpdateStatus()
        {
            RetryButtonEnabled = false;
            ScanQREnabled = false;
            //this.NeedsSSN = SSN1 == SSN2 || string.IsNullOrEmpty(SSN1);
            switch (this.AboutStatus)
            {
                case AboutStatusEnum.intro:
                    Warning = Resource.AboutWaitMessage;
                    ButtonTitle = Resource.AboutLearnMore;
                    ScanQREnabled = true;
                    RetryButtonEnabled = true;
                    break;
                case AboutStatusEnum.hasQRText:

                    break;
                case AboutStatusEnum.loading:
                    Warning = Resource.AboutLoadingElectionMessage;
                    ButtonTitle = Resource.AboutLearnMore;                    
                    break;
                case AboutStatusEnum.hasLoaded:
                    RetryButtonEnabled = true;
                    break;
                case AboutStatusEnum.retry:

                    break;
                case AboutStatusEnum.noInternet:
                    Warning = Resource.AppInternetMessage;
                    break;
                case AboutStatusEnum.noCameraSupport:
                    Warning = Resource.AppInternetMessage;
                    break;
                //case AboutStatusEnum.needsSSN:
                //    ButtonTitle = Resource.AboutNeedsSSN;
                //    this.NeedsSSN = true;
                //    break;

            }
        }

        public async Task OnQRScanned(string qrText)
        {
            if (string.IsNullOrEmpty(qrText))
                return;

            //(Guid electionId, string registration, int birthYear, Guid ballotId) = Utils.DisectQR(qrText);       
            QRModel model = Models.Utils.DisectQR(qrText, null);
            if (model.ElectionId == Guid.Empty)
                return;

            AboutStatus = AboutStatusEnum.loading;

            this.electionId = model.ElectionId;
            await DataService.InitElection(model.ElectionId);

            if (Models.Utils.BallotHasBeenSubmitted(model.BallotId, false))
            {
                this.DisplayAlert?.Invoke(Resource.Warning, Resource.AboutBallotHasBeeSubmitted);
                this.electionId = Guid.Empty;
                DataService.Election = null;
                DataService.QRText = null;
                AboutStatus = AboutStatusEnum.intro;
                return;
            }

            AboutStatus = AboutStatusEnum.hasLoaded;
        }

        public void OnAppearing()
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                AboutStatus = AboutStatusEnum.noInternet;
            }
            if (!MediaPicker.IsCaptureSupported)
            {
                AboutStatus = AboutStatusEnum.noCameraSupport;
            }
            else
            {
                AboutStatus = AboutStatusEnum.intro;          
            }
            UpdateStatus();
        }

        private string  haarcascade_frontalface_alt;
        public async Task CheckOutStuff()
        {
            bool FaceFound = false;

            MediaPickerOptions options = new MediaPickerOptions();
            options.Title = "Test stuff in ios";
            
            var photo = await MediaPicker.PickPhotoAsync(options);
            using (var stream = await photo.OpenReadAsync())
            using (Mat frame = OpenCvSharp.Cv2.ImRead(photo.FullPath))
            {
                if (frame != null)
                {
                    OpenCvSharp.Rect[] faces = GetHaarCascadeFaces(frame, 3);
                    FaceFound = faces.Length == 1;
                }
            }
        }

        private OpenCvSharp.Rect[] GetHaarCascadeFaces(Mat frame, int maxFaces)
        {
            double scaleFactor = 1.3;
            int minNeighbors = 2;
            haarcascade_frontalface_alt = PutFileName("haarcascade_frontalface_alt.xml", "ElectionModels", "Haarcascade");

            using (var gray = new Mat())
            {
                OpenCvSharp.Rect[] faces = new OpenCvSharp.Rect[0];
                Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);

                try
                {
                    CascadeClassifier faceClassifier = new CascadeClassifier(haarcascade_frontalface_alt);
                    faces = faceClassifier.DetectMultiScale(gray, scaleFactor, minNeighbors, HaarDetectionType.ScaleImage);
                    List<System.Drawing.Rectangle> rfaces = new List<System.Drawing.Rectangle>();
                    foreach (OpenCvSharp.Rect rect in faces)
                    {
                        System.Drawing.Rectangle r = new System.Drawing.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
                        rfaces.Add(r);
                    }

                    List<OpenCvSharp.Rect> largest = faces?.OrderByDescending(n => n.Width * n.Height).Take(maxFaces).ToList();
                    return largest.ToArray();
                }
                catch
                {

                }
            }
            return null;
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