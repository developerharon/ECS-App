using System;
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
            if (string.IsNullOrWhiteSpace(emailEntry.ToString()) || string.IsNullOrWhiteSpace(passwordEntry.ToString()))
            {
                // Print an error Message
            }
            // Login logic here
        }
    }
}