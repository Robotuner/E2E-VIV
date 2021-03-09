using Election.Models;
using Election.Services;
using ElectionModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Election.ViewModels.Views
{
    public class ElectionSignatureViewModel : ElectionBaseViewModel
    {
        public ObservableCollection<SignatureViewModel> Signatures { get; set; }
        public ICommand GetSignaturesCommand { get; set; }

        private int skip;
        public int Skip
        {
            get { return skip; }
            set
            {
                if (skip != value)
                {
                    skip = value;
                    OnPropertyChanged("Skip");
                }
            }
        }

        private int take;
        public int Take
        {
            get { return take; }
            set
            {
                if (take != value)
                {
                    take = value;
                    OnPropertyChanged("Take");
                }
            }
        }

        private SignatureViewModel selectedSignature;
        public SignatureViewModel SelectedSignature
        {
            get { return selectedSignature; }
            set
            {
                if (selectedSignature != value)
                {
                    selectedSignature = value;
                    OnPropertyChanged("SelectedSignature");
                }
            }
        }

        public ElectionSignatureViewModel() : base()
        {
            Skip = 0;
            Take = 5;
            GetSignaturesCommand = new Command(OnGetSignatures);

            Signatures = new ObservableCollection<SignatureViewModel>();
        }

        public override async Task OnLoaded()
        {
            await base.OnLoaded();
            OnGetSignatures(null);
        }

        private async void OnGetSignatures(object p)
        {
            if (SelectedElection == null)
                return;

            Signatures.Clear();
            List<Signature> result = await DataService.GetSignatures(SelectedElection.Id, Skip, Take);
            foreach (Signature sig in result)
            {
                Signatures.Add(mapper.Map<Signature, SignatureViewModel>(sig));
            }
        }
    }
}
