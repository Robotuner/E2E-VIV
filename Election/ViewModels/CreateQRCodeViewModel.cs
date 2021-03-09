using Election.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Election.ViewModels
{
    public class CreateQRCodeViewModel : BaseViewModel
    {
        public ICommand RefreshBallotIdCommand { get; set; }
        public ICommand CreateQRCommand { get; set; }
        private string registration;
        public string Registration
        {
            get { return registration; }
            set
            {
                if (registration != value)
                {
                    registration = value;
                    OnPropertyChanged("Registration");
                    this.CanCreateQR(null);
                }
            }
        }

        private int birthYear;
        public int BirthYear
        {
            get { return birthYear; }
            set
            {
                if (birthYear != value)
                {
                    birthYear = value;
                    OnPropertyChanged("BirthYear");
                    this.CanCreateQR(null);
                }
            }
        }

        private Guid ballotId;
        public Guid BallotId
        {
            get { return ballotId; }
            set
            {
                if (ballotId != value)
                {
                    ballotId = value;
                    OnPropertyChanged("BallotId");
                    this.CanCreateQR(null);
                }
            }
        }

        private BitmapImage qrImage;
        public BitmapImage QRImage
        {
            get { return qrImage; } 
            set
            {
                if (qrImage != value)
                {
                    qrImage = value;
                    OnPropertyChanged("QRImage");
                }
            }
        }

        private bool createQRButtonEnabled;
        public bool CreateQRButtonEnabled
        {
            get { return createQRButtonEnabled; }
            set
            {
                if (createQRButtonEnabled != value)
                {
                    createQRButtonEnabled = value;
                    OnPropertyChanged("CreateQRButtonEnabled");
                }
            }
        }

        private List<SelectGuidItem> elections;
        public List<SelectGuidItem> Elections
        {
            get { return elections; }
            set
            {
                if (elections != value)
                {
                    elections = value;
                    OnPropertyChanged("Elections");
                }
            }
        }

        private SelectGuidItem selectElection;
        public SelectGuidItem SelectedElection
        {
            get { return selectElection; }
            set
            {
                if (selectElection != value)
                {
                    selectElection = value;
                    OnPropertyChanged("SelectedElection");
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
        
        public CreateQRCodeViewModel() : base()
        {
            Registration = "Lenny Kong";
            SSN = "404323839";
            BallotId = Guid.NewGuid();
            BirthYear = DateTime.Today.AddYears(-18).Year;
            CreateQRButtonEnabled = false;
            RefreshBallotIdCommand = new Command( OnRefreshBallotId);
            CreateQRCommand = new Command(OnCreateQR,  CanCreateQR);
        }

        private void OnRefreshBallotId(object p)
        {
            this.BallotId = Guid.NewGuid();
            this.OnCreateQR(null);
        }

        private void OnCreateQR(object p)
        {
            if (this.SelectedElection == null || BallotId == Guid.Empty || string.IsNullOrEmpty(SSN))
                return;

            string encryptedstring = ElectionModels.Misc.Utils.Encrypt(this.BallotId.ToString("n"), this.SSN);
            string decryptedBallotId = ElectionModels.Misc.Utils.Decrypt(encryptedstring, this.SSN);

            string code = string.Format("{0}|{1}|{2}|{3}", this.SelectedElection.Id, Registration, BirthYear, encryptedstring);
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            System.Drawing.Bitmap qrCodeImage = qrCode.GetGraphic(40);

            QRImage = qrCodeImage.ToBitmapImage();
        }

        private bool CanCreateQR(object p)
        {
            CreateQRButtonEnabled = !string.IsNullOrEmpty(Registration) && BirthYear <= DateTime.Today.AddYears(-18).Year && BallotId != Guid.Empty;
            if (CreateQRButtonEnabled)
            {
                OnCreateQR(null);
            }
            return CreateQRButtonEnabled;
        }


    }
}
