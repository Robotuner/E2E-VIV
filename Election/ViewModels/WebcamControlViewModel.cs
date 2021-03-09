using OpenCvSharp;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Election.ViewModels
{
    public class WebcamControlViewModel : BaseViewModel
    {
        private static VideoCapture capture;
        private DateTime previousTime;
        private readonly SynchronizationContext context;
        private Thread currentThread;

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

        public event EventHandler EventCallback;

        public WebcamControlViewModel() : base()
        {
            context = SynchronizationContext.Current;
            context = context ?? new SynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(context);

            currentThread = new Thread(new ParameterizedThreadStart(Run));
            currentThread.Start(SynchronizationContext.Current);

            capture = new VideoCapture(0);
            capture.Fps = 30;
        }


        private void CallEventHandler(object state)
        {
            EventHandler handler = EventCallback;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void Run(object state)
        {
            var context = state as SynchronizationContext;
            context.Send(UpdateImage, null);
        }

        ~WebcamControlViewModel()
        {
            if (capture.IsOpened())
            {
                capture.Release();                
            }
            capture.Dispose();
        }

        public async Task InitializeWebCam()
        {        
            this.previousTime = DateTime.Now;
            await Task.Run(() =>
            {
                if (!capture.IsOpened())
                {
                    capture = new VideoCapture(0);
                }
                int cnt = 0;
                // filePath = $"D://Junk/TestCamImages/image.png";
                using (Mat image = new Mat())
                {
                    while (true)
                    {
                        if (capture.Read(image))
                        {
                            //var ans = image.ToMemoryStream(ext: ".png");
                            byte[] encodedArray = image.ImEncode(".png");
                            using (MemoryStream stream = new MemoryStream(encodedArray))
                            //using (MemoryStream stream = image.ToMemoryStream(ext: ".png"))
                            {
                                cnt++;
                                //SaveAs(stream, filePath);
                                BitmapImage bmi = new BitmapImage();
                                bmi.BeginInit();
                                bmi.StreamSource = stream;
                                bmi.CacheOption = BitmapCacheOption.OnLoad; 
                                bmi.EndInit();
                                Run(bmi);
                            }
                        }

                    }
                }

            });               
        }


        private void UpdateImage(Object bmi)
        {         
            this.ImageSource = bmi as BitmapImage;
        }


        private void SaveAs(MemoryStream stream, string filePath)
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(stream));
            using (var filestream = new FileStream(filePath, FileMode.Create))
            {
                encoder.Save(filestream);
            }
        }
    }
}
