using Election.Models;
using Election.ViewModels.Views;
using System.Collections.Generic;

namespace Election.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private List<ElectionMenu> menu;
        public List<ElectionMenu> Menu
        {
            get { return menu; }
            set
            {
                if (menu != value)
                {
                    menu = value;
                    OnPropertyChanged("Menu");
                }
            }
        }

        private ElectionMenu selectedMenu;
        public ElectionMenu SelectedMenu
        {
            get { return selectedMenu; }
            set
            {
                if (selectedMenu != value)
                {
                    selectedMenu = value;
                    OnPropertyChanged("SelectedMenu");
                    SetViewModel(value);
                }
            }
        }

        private BaseViewModel currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                if (currentViewModel != value)
                {
                    currentViewModel = value;
                    OnPropertyChanged("CurrentViewModel");
                }
            }
        }

        public MainWindowViewModel() : base()
        {
            Menu = new List<ElectionMenu>()
            {
                new ElectionMenu()
                {
                    Id = MenuEnum.undefined,
                    Name = FontAwesome.Building,
                    Description = "Home",
                    FontFamily = "FontAwesomeSolid",
                },
                new ElectionMenu()
                {
                    Id = MenuEnum.QRCode,
                    Name = FontAwesome.CheckCircle,
                    Description = "CreateQR",
                    FontFamily = "FontAwesomeRegular",
                },
                new ElectionMenu()
                {
                    Id = MenuEnum.FaceDetection,
                    Name = FontAwesome.Grin,
                    Description = "Face Detection",
                    FontFamily = "FontAwesomeRegular",
                },
                new ElectionMenu()
                {
                    Id = MenuEnum.Election,
                    Name = FontAwesome.CheckSquare,
                    Description = "Election Ballot",
                    FontFamily = "FontAwesomeSolid",
                },
                new ElectionMenu()
                {
                    Id = MenuEnum.ElectionSummary,
                    Name = FontAwesome.CheckSquare,
                    Description = "Election Summary",
                    FontFamily = "FontAwesomeSolid",
                },
                 new ElectionMenu()
                {
                    Id = MenuEnum.ElectionSignature,
                    Name = FontAwesome.CheckSquare,
                    Description = "Election Signature",
                    FontFamily = "FontAwesomeSolid",
                }
            };
        }

        public void SetViewModel(ElectionMenu menu)
        {
            switch (menu.Id)
            {
                case MenuEnum.QRCode:
                    this.CurrentViewModel = new CreateQRCodeViewModel();
                    break;
                case MenuEnum.FaceDetection:
                    //this.CurrentViewModel = new FaceDetectionViewModel();
                    this.CurrentViewModel = new WebCamPageViewModel();
                    break;
                case MenuEnum.Election:
                    this.CurrentViewModel = new ElectionViewModel();
                    break;
                case MenuEnum.ElectionSummary:
                    this.CurrentViewModel = new ElectionSummaryViewModel();
                    break;
                case MenuEnum.ElectionSignature:
                    this.CurrentViewModel = new ElectionSignatureViewModel();
                    break;
                default:
                    this.CurrentViewModel = null;
                    break;
            }
        }
    }
}
