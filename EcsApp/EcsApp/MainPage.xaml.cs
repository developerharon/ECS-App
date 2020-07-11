using EcsApp.Models;
using EcsApp.Models.ApiModels;
using Plugin.Fingerprint;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EcsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private UserService _userService;

        public MainPage()
        {
            InitializeComponent();
            LoadUsersInformation();
            buttonClock.Clicked += OnButtonClockClicked;
            buttonLocation.Clicked += OnButtonLocationClicked;
        }

        private async void LoadUsersInformation()
        {
            var user = new AuthenticationModel();
            var refreshToken = await Constants.GetRefreshToken();
            var userService = new UserService();
            try
            {
                // Get a new token and an user object from the backend. We assume the since application has been opened the refresh token has not expired. 
                user = await userService.GetRefreshTokenAsync(refreshToken);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }


            if (user != null && user.IsAuthenticated)
            {
                // Save the new user's details.
                Constants.RemoveAll();
                Constants.SaveUsersDetails(user);

                // Create the userservice object
                //_userService = new UserService(await Constants.GetOuthToken());
                _userService = new UserService();

                // Updates the form with the new values.
                labelName.Text = user.Name;
                labelEmail.Text = user.Email;
                entryProfilePic.Source = user.ProfilePicUrl;

                // Get application state and use to determine button clock
                try
                {
                    ClockResponseModel applicationState = await _userService.GetApplicationState(user.Email);
                    if (applicationState != null && applicationState.Succeeded)
                    {
                        if (applicationState.IsClockActive)
                        {
                            buttonClock.Text = "Clock Out";
                            buttonClock.BackgroundColor = Color.Red;
                        }
                        else
                        {
                            buttonClock.Text = "Clock In";
                            buttonClock.BackgroundColor = Color.FromHex("#77D065");
                        }
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }

            }
        }

        private async void OnButtonClockClicked(object sender, EventArgs args)
        {
            // Checks if fingerprint authention is supported on the device, return a boolean value
            var result = await CrossFingerprint.Current.IsAvailableAsync(true);

            if (result)
            {
                var cancellationToken = new System.Threading.CancellationToken();
                var reason = new Plugin.Fingerprint.Abstractions.AuthenticationRequestConfiguration("Fingerprint Authentication", "Please scan your fingerprints to confirm your identity");
                var auth = await CrossFingerprint.Current.AuthenticateAsync(reason, cancellationToken);

                if (auth.Authenticated)
                {
                    ClockUser();
                }
            }
            else
            {
                await DisplayAlert("Error", "Your device doesn't support fingerprint authentication", "OK");
            }
        }

        private async void OnButtonLocationClicked(object sender, EventArgs args)
        {
            var location = await GetUsersLocationAsync();

            if (location != null)
            {
                await DisplayAlert("Success", $"User's Location; Latitude: {location.Latitude}; Longitude: {location.Longitude}", "OK");
            }
            else
            {
                await DisplayAlert("Fail", "Location Not Found", "OK");
            }
        }

        private async void ClockUser()
        {
            var clockModel = new ClockModel();
            var result = new ClockResponseModel();

            var usersLocation = await GetUsersLocationAsync();

            if (usersLocation != null)
            {
                clockModel.Email = await Constants.GetEmail();
                clockModel.ClockLocation = usersLocation;
                clockModel.ClockTime = DateTime.UtcNow;

                if (buttonClock.Text == "Clock In")
                {
                    result = await _userService.ClockInAsync(clockModel);
                    if (result != null && result.Succeeded)
                    {
                        buttonClock.Text = "Clock Out";
                        buttonClock.BackgroundColor = Color.Red;
                        await DisplayAlert("Success", "Clocked In Successfully", "OK");
                    }
                }
                else
                {
                    result = await _userService.ClockOutAsync(clockModel);
                    if (result != null && result.Succeeded)
                    {
                        buttonClock.Text = "Clock In";
                        buttonClock.BackgroundColor = Color.FromHex("#77D065");
                        await DisplayAlert("Success", "Clocked Out Successfully", "OK");
                    }
                }
            }
        }

        private async Task<Location> GetUsersLocationAsync()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(1));
                var location = await Geolocation.GetLocationAsync(request);
                return location;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
                return null;
            }
        }
    }
}