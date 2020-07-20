using EcsApp.Models;
using Xamarin.Forms;

namespace EcsApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Based on the system's state, it determine's which form to be displayed first.
            LoadApplicationInitialState();
        }

        private async void LoadApplicationInitialState()
        {
            // If there is no token saved in the phone display the login page
            if (await Constants.GetRefreshToken() == null)
            {
                MainPage = new NavigationPage(new LoginPage());

            }
            else
            {
                // If there is a token but it has expired display the login page
                if (await Constants.IsRefreshTokenExpired())
                {
                    MainPage = new NavigationPage(new LoginPage());
                }
                else
                {
                    // There is a refresh token and it has not expired, we display the main page instead
                    MainPage = new NavigationPage(new ClockPage());

                }
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
