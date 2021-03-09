using Election.Models;
using System;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Election.ViewModels.Views
{
    public class SignatureViewModel : BaseFaceViewModel
    {
        public Guid Id { get; set; }
        public Guid BallotId { get; set; }
        public string Name { get; set; }
        public bool Confirmed { get; set; }
        public string DeviceId { get; set; }
        public int BirthYear { get; set; }

        private byte[] imageArray;
        public byte[] ImageArray
        {
            get { return imageArray; }
            set
            {
                if (imageArray != value)
                {
                    imageArray = value;
                    OnPropertyChanged("ImageArray");
                    this.LoadImage();
                }
            }
        }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Platform { get; set; }
        public Guid? PreviousSignature { get; set; }
        public int SignatureStatus { get; set; }
        public DateTime SubmitDate { get; set; }
        public string PhoneNumber { get; set; }

        public SignatureViewModel():base()
        {
            //this.ImagePath = string.Format(@"D:\\junk\testCamImages\{0}.png", Id.ToString("n"));
            LoadImage();
        }

        public void Init()
        {
            OnPropertyChanged("ImageSource");
            //this.ImagePath = string.Format(@"D:\\junk\testCamImages\{0}.png", Id.ToString("n"));
        }

        protected override void LoadImage()
        {
            if (imageArray == null)
                return;
            this.ImagePath = string.Format(@"D:\\junk\testCamImages\{0}.png", Id.ToString("n"));
            BitmapImage bmi = new BitmapImage();
            using (var mem = new MemoryStream(imageArray))
            {
                mem.Position = 0;
                bmi.BeginInit();
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.Rotation = this.Rotation;
                bmi.UriSource = null;
                bmi.StreamSource = mem;
                bmi.EndInit();
            }
            this.Ratio = bmi.Width / bmi.Height;
            Width = 200;
            ImageSource = bmi;
            OnPropertyChanged("ImageSource");

        }

        private Guid GetID()
        {
            return Id;
        }
        
    }
}
