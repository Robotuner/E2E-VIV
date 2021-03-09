using ElectionModels;
using ElectionModels.Misc;
using Newtonsoft.Json;
using OneVote.Models;
using OneVote.Services;
using OneVote.Views;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OneVote.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Guid signatureConfirmationGuid { get; set; }

        private readonly string CaffeModel;
        private readonly string PrototextPath;
        private readonly string haarcascade_frontalface_alt;
        private CascadeClassifier faceClassifier { get; set; }
        public bool isSubmitting { get; set; }
        
        public Action<Signature> SubmittalConfirmation { get; set; }
        public Action<string> ErrorMessage { get; set; }
        public Action<string> ErrorMessage2 { get; set; }
        public ObservableCollection<CategoryTypeItem> Items { get; set; }
        public Command LoadItemsCommand { get; }
        public Command<CategoryTypeItem> ItemTapped { get; }
        public ICommand SubmitBallotCommand { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<CategoryTypeItem>();
            SubmitBallotCommand = new Command(async () => await OnSubmitBallot());
            ItemTapped = new Command<CategoryTypeItem>(OnItemSelected);  
            haarcascade_frontalface_alt = PutFileName("haarcascade_frontalface_alt.xml", "ElectionModels", "Haarcascade");
            CaffeModel = PutFileName("res10_300x300_ssd_iter_140000.caffemodel", "ElectionModels", "Dnn");
            PrototextPath = PutFileName("deploy.prototxt", "ElectionModels", "Dnn");
            CheckCanSubmitBallot();
        }

        public async void OnAppearing()
        {
            try
            {
                IsBusy = true;
                SelectedItem = null;
                await DataService.InitCategoryTypes();
                if (DataService.CategoryTypeItems != null)
                {
                    Items.Clear();
                    foreach(CategoryTypeItem ct in DataService.CategoryTypeItems)
                    {
                        (int selected, int total) = DataService.GetCategoryStatus(ct);
                        ct.Selected = selected;
                        ct.Total = total;
                        Items.Add(ct);
                    }                    
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private CategoryTypeItem _selectedItem;
        public CategoryTypeItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
                CheckCanSubmitBallot();
            }
        }

        async void OnItemSelected(CategoryTypeItem item)
        {
            if (item == null)
                return;

            if (!this.isSubmitting)
            {
                // This will push the ItemDetailPage onto the navigation stack
                await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?CatType={item.Id.ToString()}");
                CheckCanSubmitBallot();
            }
        }

        public bool BallotIsFilled()
        {
            if (DataService.Election == null)
            {
                ErrorMessage(Resource.NoVotesCast);
                return false;
            }
            try
            {
                bool found = DataService.CategoryList.Any(n => n.Selection != null);
                return found;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }

        private string elapsedTime;
        public string ElapsedTime
        {
            get { return elapsedTime; }
            set
            {
                if (elapsedTime != value)
                {
                    elapsedTime = value;
                    OnPropertyChanged("ElapsedTime");
                }
            }
        }

        private string encryptionMessage;
        public string EncryptionMessage
        {
            get { return encryptionMessage; }
            set
            {
                if (encryptionMessage != value)
                {
                    encryptionMessage = value;
                    OnPropertyChanged("EncryptionMessage");
                }
            }
        }

        private bool canSubmitBallot;
        public bool CanSubmitBallot
        {
            get { return canSubmitBallot; }
            set
            {
                if (canSubmitBallot != value)
                {
                    canSubmitBallot = value;
                    OnPropertyChanged("CanSubmitBallot");
                }
            }
        }

        private bool choicesHaveBeenApproved;
        public bool ChoicesHaveBeenApproved
        {
            get { return choicesHaveBeenApproved; }
            set
            {
                if (choicesHaveBeenApproved != value)
                {
                    choicesHaveBeenApproved = value;
                    OnPropertyChanged("ChoicesHaveBeenApproved");
                }
            }
        }

        private string ssn;
        public string SSN
        {
            get { return ssn; }
            set
            {
                if (ssn != value)
                {
                    ssn = value;
                    OnPropertyChanged("SSN");
                }
            }
        }
       
        public async Task OnSubmitBallot()
        {
            if (!this.BallotIsFilled() || string.IsNullOrEmpty(DataService.QRText) || string.IsNullOrEmpty(this.SSN))
                return;

            this.EncryptionMessage = Resource.ItemsViewModelEncryptionMessage;
            (Guid electionId, string registration, int birthYear, Guid ballotId) = Models.Utils.DisectQR(DataService.QRText, this.SSN);

            if (electionId == Guid.Empty)
            {
                this.SSN = null;
                MessagingCenter.Send<ItemsViewModel>(this, MessagingEvents.BadSSN);
                return;
            }
            if (Models.Utils.BallotHasBeenSubmitted(ballotId, allowUpdates: DataService.Election.AllowUpdates))
            {
                ErrorMessage?.Invoke(Resource.BallotAlreadySubmitted);
                return;
            }

            // We already did a check isBallotFilled() so we know there are votes.
            List<Vote> votes = DataService.Validate(ballotId: ballotId);

            // check count just in case.
            if (votes.Count == 0)
                return;

            this.isSubmitting = true;
#if DEBUG
            LogVote(votes);
#endif
            try
            {
                FileResult photo = await GetHeadShot();    
                if (await IsValidFace(photo))
                {
                    await this.SubmitBallot(photo, ballotId, registration, birthYear, votes);
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ErrorMessage2?.Invoke(Resource.FoundFacesFailed);
                    });
                }
            }
            catch (Exception ex)
            {
                ErrorMessage?.Invoke(ex.Message);
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                this.isSubmitting = false;
                ElapsedTime = $"Confirmed {DateTime.Now.ToString("MM/dd/yyyy HH:mm")}\nId: {this.signatureConfirmationGuid}";
                this.EncryptionMessage = null;
            }
        }

        public void SetApprovedState(bool approved)
        {
            try
            {
                this.ChoicesHaveBeenApproved = approved;
                this.CheckCanSubmitBallot();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void CheckCanSubmitBallot()
        {
            try
            {
                CanSubmitBallot = BallotIsFilled() &&
                    DataService.CanCastBallot() &&
                    ChoicesHaveBeenApproved;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task<FileResult> GetHeadShot()
        {
            MediaPickerOptions options = new MediaPickerOptions()
            {
                Title = Resource.ItemsViewModelHeadShot
            };
            var photo = await MediaPicker.CapturePhotoAsync(options);
            return photo;
        }
 
        private async Task SubmitBallot(FileResult photo, Guid ballotId, string registration, int birthYear, List<Vote> votes)
        { 
            byte[] imageArray = await AsByteArray(photo);

            Location location = await Models.Utils.GetDeviceLocation();

            DateTime approvalDate = DateTime.UtcNow;  
            foreach (Vote vote in votes)
            {
                vote.ApprovalDate = approvalDate;
            }
            string phoneNumber = null;

            //if (DeviceInfo.Platform == DevicePlatform.Android)
            //{
            //    phoneNumber = DependencyService.Get<IPhoneDevice>().GetIdentifier();
            //}
            //save signature;
            Signature sig = new Signature()
            {
                BallotId = ballotId,
                ElectionId = DataService.Election.Id,
                DeviceId = Models.Utils.GetId(),
                Name = registration,
                PhoneNumber = phoneNumber,
                BirthYear = birthYear,
                ImageArray = imageArray,
                Longitude = location.Longitude,
                Latitude = location.Latitude,
                PreviousSignature = null,
                SignatureStatus = (int)SignatureStatusEnum.isValidSignature,
                SubmitDate = DateTime.UtcNow,
                Platform = Device.RuntimePlatform == Device.Android ? (int)PlatformEnum.android : (int)PlatformEnum.iOS,
                Votes = votes
            };

            BlockChain electionChain = await this.ConvertToBlockChain(sig);
            int nonce = electionChain.GetLatestBlock().Nonce;
            var ans = await DataService.NotifyPendingSubmittal(nonce, ballotId);
            if (ans.Id != Guid.Empty)
            {
                Signature result = await DataService.PutSignature(electionChain);
                this.signatureConfirmationGuid = result.Id;

                SubmittalConfirmation?.Invoke(result);
                // reset properties
                this.SSN = null;
                SetApprovedState(false);

                if (!DataService.Election.AllowUpdates)
                {
                    DataService.ClearVotes();
                }
            }
        }

        // displays elapsed time as a nonce is being computed.
        // Misc.Utils.ConvertToBlockChain does not support UI Updates
        private async Task<BlockChain> ConvertToBlockChain(Signature signature)
        {
            BlockChain ElectionChain = new BlockChain();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try
            {
                Block block = new Block(DateTime.Now, null, JsonConvert.SerializeObject(signature));
                block.PropertyChanged += (s, a) =>
                {
                    if (a.PropertyName == "Nonce")
                    {
                        // https://lukealderton.com/blog/posts/2016/october/xamarin-forms-working-with-threads/
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            double ans = sw.ElapsedMilliseconds / 60000.0;
                            string msg = string.Format("Elapsed Time: {0} min\n", Math.Round(ans, 2));
                            string lne2 = string.Format(Resource.ItemsViewmodelProgressUpdate, block.Nonce);
                            this.ElapsedTime = msg + lne2;
                        });
                    }
                };
                await ElectionChain.AddBlock(block);
            }
            finally
            {
                sw.Stop();
            }
            return ElectionChain;
        }

        private async Task<Byte[]> AsByteArray(FileResult photo)
        {
            var stream = await photo.OpenReadAsync();
            byte[] ans = new byte[stream.Length];
            stream.Read(ans, 0, ans.Length);
            string image = ans.ToString();
            return ans;
        }

        private async Task<bool> IsValidFace(FileResult photo)
        {
            bool FaceFound = false;

            try
            {
                if (Device.RuntimePlatform == Device.Android)
                {
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
                else
                {
                    FaceFound = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return FaceFound;
        }

        private void LogVote(List<Vote> votes)
        {
            foreach (Vote vote in votes)
            {
                CategoryViewModel cat = DataService.CategoryList.SingleOrDefault(n => n.Id == vote.CategoryId);
                if (cat != null)
                {
                    TicketViewModel ticket = cat.Tickets.SingleOrDefault(n => n.Id == vote.SelectionId);
                    if (ticket != null)
                    {
                        string catName = cat.Category.CategoryTypeId == CategoryTypeEnum.measure ? cat.Title : cat.Heading;
                        Debug.WriteLine(string.Format("CategoryId: {0} Selection: {1}", catName, ticket.Ticket.Description));
                    }
                }
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

        private OpenCvSharp.Rect[] GetHaarCascadeFaces(Mat frame, int maxFaces)
        {
            double scaleFactor = 1.3;
            int minNeighbors = 2;

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
    }
}