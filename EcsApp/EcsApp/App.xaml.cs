using EcsApp.Models;
using Xamarin.Forms;

namespace EcsApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            LoadApplicationInitialState();
        }

        private async void LoadApplicationInitialState()
        {
            if (await Constants.GetEmail() == null)
            {
                MainPage = new NavigationPage(new LoginPage());

            }
            else
            {
                MainPage = new NavigationPage(new ClockPage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
