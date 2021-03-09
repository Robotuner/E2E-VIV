using OneVote.Services;
using Xamarin.Forms;

namespace OneVote
{
    public partial class App : Application
    {
        public App()
        {            
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
