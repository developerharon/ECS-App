using EcsApp.Models;
using EcsApp.Models.ApiModels;
using System;
using System.Runtime.InteropServices;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EcsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            buttonLogin.Clicked += OnButtonLoginClicked;
        }

        private async void OnButtonLoginClicked(object sender, EventArgs args)
        {
            if (string.IsNullOrWhiteSpace(emailEntry.Text) || string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                labelMessage.Text = "Please fill all the required fields";
            }

            UserService service = new UserService();

            LoginModel model = new LoginModel
            {
                Email = emailEntry.Text,
                Password = passwordEntry.Text
            };

            var result = await service.GetTokenAsync(model);

            if (result == null)
            {
                await DisplayAlert("Error", "API call failed", "OK");
            }

            await DisplayAlert("Success", $"Token: {result.Token}, Refresh Token: {result.RefreshToken}", "OK");
        }
    }
}