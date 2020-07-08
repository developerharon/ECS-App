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

            try
            {
                var user = await service.GetTokenAsync(model);
                if (user == null)
                {
                    await DisplayAlert("Error", "An error occured when processing your request. Check your network connection and start again.", "OK");
                }

                if (user.IsAuthenticated)
                {
                    // Open the main page and dispaly the relevant details.
                    // Save the tokens safely in the application so we can reuse it.
                }
                else
                {
                    labelMessage.Text = "Invalid email or password";
                }
            }
            catch
            {
                await DisplayAlert("Error", "An error occurred", "OK");
            }

        }
    }
}