
using EcsApp.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EcsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private readonly UserService _userService;
        public MainPage()
        {
            InitializeComponent();
            LoadUsersInformation();
        }

        public async void LoadUsersInformation()
        {
            var refreshToken = await Constants.GetRefreshToken();
            var userService = new UserService();

            var result = await userService.GetRefreshTokenAsync(refreshToken);

            if (result != null && result.IsAuthenticated)
            {
                Constants.SaveUsersDetails(result);

                labelName.Text = result.Name;
                labelEmail.Text = result.Email;
            }

        }

        public async void LoadUsersInformation(object sender, EventArgs args)
        {
            var refreshToken = await Constants.GetRefreshToken();

            var user = await _userService.GetRefreshTokenAsync(refreshToken);

            if (user != null)
            {
                Constants.SaveUsersDetails(user);
            }
        }
    }
}