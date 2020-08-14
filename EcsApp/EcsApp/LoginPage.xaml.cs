using EcsApp.Models;
using EcsApp.Models.ApiModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EcsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly UserService _userService;
        public LoginPage()
        {
            _userService = new UserService();
            InitializeComponent();
            buttonLogin.Clicked += OnButtonLoginClicked;
        }

        private async void OnButtonLoginClicked(object sender, EventArgs args)
        {
            // Check if the user has filled the email and the password.
            if (!string.IsNullOrWhiteSpace(emailEntry.Text) || !string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                LoginModel model = new LoginModel
                {
                    Email = emailEntry.Text,
                    Password = passwordEntry.Text
                };

                try
                {
                    // Login the user with the backend so we can get an outh-token
                    var user = await _userService.LoginAsync(model);

                    if (user == null)
                    {
                        labelMessage.Text = "An error occured when processing your request";
                    }

                    if (user.IsAuthenticated)
                    {
                        // Save the token's to secure storage 
                        Constants.RemoveAll();
                        Constants.SaveUsersDetails(model);

                        // Open the main page
                        Navigation.InsertPageBefore(new ClockPage(), this);
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        labelMessage.Text = "Invalid email or password";
                    }
                }
                catch (Exception ex)
                {
                    labelMessage.Text = ex.Message;
                }
            }
            else
            {
                labelMessage.Text = "Please fill all the required fields";
            }
        }
    }
}