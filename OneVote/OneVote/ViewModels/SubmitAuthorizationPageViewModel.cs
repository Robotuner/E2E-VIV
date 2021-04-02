namespace OneVote.ViewModels
{
    public class SubmitAuthorizationPageViewModel : BaseViewModel
    {
        private bool okButtonEnabled;
        public bool OKButtonEnabled
        {
            get { return okButtonEnabled; }
            set
            {
                if (okButtonEnabled != value)
                {
                    okButtonEnabled = value;
                    OnPropertyChanged("OKButtonEnabled");
                }
            }
        }

        private string ssn1;
        public string SSN1
        {
            get { return ssn1; }
            set
            {
                if (ssn1 != value)
                {
                    ssn1 = value;
                    OnPropertyChanged("SSN1");
                    this.UpdateVisibility();
                }
            }
        }

        private string ssn2;
        public string SSN2
        {
            get { return ssn2; }
            set
            {
                if (ssn2 != value)
                {
                    ssn2 = value;
                    OnPropertyChanged("SSN2");
                    this.UpdateVisibility();
                }
            }
        }

        public string SSN
        {
            get
            {
                if (string.IsNullOrEmpty(SSN1) || string.IsNullOrEmpty(SSN2))
                    return null;

                return StripSpaces(SSN1)?.Trim() == StripSpaces(SSN2)?.Trim() ? StripSpaces(SSN1)?.Trim() : null;
            }
        }

        private string authorizationMsg;
        public string AuthorizationMsg
        {
            get { return authorizationMsg; }
            set
            {
                if (authorizationMsg != value)
                {
                    authorizationMsg = value;
                    OnPropertyChanged("AuthorizationMsg");
                }
            }
        }

        public SubmitAuthorizationPageViewModel()
        {
            AuthorizationMsg = Resource.SubmitAuthMessage;
        }

        public void UpdateVisibility()
        {
            if (string.IsNullOrEmpty(SSN1) || string.IsNullOrEmpty(SSN2))
                this.OKButtonEnabled = false;
            else
                this.OKButtonEnabled = SSN1?.Trim() == SSN2?.Trim() ? true : false;
        }

        private string StripSpaces(string value)
        {
            string ans = value.Replace(" ", null);
            ans = ans.Replace(".", null);
            ans = ans.Replace("-", null);
            ans = ans.Replace("'", null);
            return ans;
        }
    }
}
