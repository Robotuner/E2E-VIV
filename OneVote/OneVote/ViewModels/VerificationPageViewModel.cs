using ElectionModels;
using ElectionModels.Misc;
using OneVote.Services;
using OneVote.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneVote.ViewModels
{
    public class VerificationPageViewModel : BaseViewModel
    {
        public INavigation INav { get; set; }
        private ObservableCollection<VRViewModel> vrList;
        public ObservableCollection<VRViewModel> VRList
        {
            get { return vrList; }
            set
            {
                if (vrList != value)
                {
                    vrList = value;
                    OnPropertyChanged("VRList");
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

        private bool hasBeenInit;
        public VerificationPageViewModel()
        {
            PropertyChanged += VerificationPageViewModel_PropertyChanged;
        }

        private void VerificationPageViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsAccessibility")
            {
                foreach (VRViewModel vr in VRList)
                {
                    vr.IsAccessibility = this.IsAccessibility;
                }
            }
        }

        public async Task Init()
        {
            if (hasBeenInit) return;
            hasBeenInit = true;

            if (string.IsNullOrEmpty(SSN))
            {
                SubmitAuthorizationPage sap = new SubmitAuthorizationPage();
                sap.OnSubmitOKClicked = SubmitBallotClickOK;
                await INav.PushModalAsync(sap);
            }
        }

        private async void SubmitBallotClickOK(string ssn)
        {
            if (string.IsNullOrEmpty(ssn))
                return;

            SSN = ssn;
            QRModel model = Models.Utils.DisectQR(DataService.QRText, this.SSN);
            if (model.BallotId != Guid.Empty)
            {
                //Guid ballotid = Guid.Parse("05799411-335b-45a3-b93f-e075aacdc67c");
                List<VRecord> result = await DataService.GetVRecords(model.BallotId);
                VRList = new ObservableCollection<VRViewModel>();
                foreach(VRecord vr in result)
                {
                    VRList.Add(new VRViewModel(vr));
                }
            }
        }
    }
}
