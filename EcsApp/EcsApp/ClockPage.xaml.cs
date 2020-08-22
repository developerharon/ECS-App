using EcsApp.Models;
using EcsApp.Models.ApiModels;
using Newtonsoft.Json;
using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EcsApp
{
    public
        class MainMenuItem
    {
        public string Title { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClockPage : MasterDetailPage
    {
        private UserService _userService;
        public List<MainMenuItem> MainMenuItems { get; set; }

        public ClockPage()
        {
            _userService = new UserService();
            BindingContext = this;

            MainMenuItems = new List<MainMenuItem>()
            {
                new MainMenuItem() { Title = "List All Clocks" },
                new MainMenuItem() { Title = "Logout"}
            };

            InitializeComponent();
            LoadUsersInformation();
            buttonClock.Clicked += OnButtonClockClicked;
        }

        private async void LoadUsersInformation()
        {
            var user = new AuthenticationModel();
            var applicationState = new ClockResponseModel();
            string jsonProfilePictureUrl = null;
            var model = new LoginModel { Email = await Constants.GetEmail(), Password = await Constants.GetPassword() };
            
            try
            {
                user = await _userService.LoginAsync(model);
                jsonProfilePictureUrl = await _userService.GetProfilePictureUrl(model.Email);
                applicationState = await _userService.GetApplicationState(model.Email);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }

            if (user != null && user.IsAuthenticated)
            {
                Constants.RemoveAll();
                Constants.SaveUsersDetails(model);

                labelName.Text = user.Name;
                labelEmail.Text = user.Email;
                if (jsonProfilePictureUrl != null)
                {
                    Dictionary<string, string> objectProfile = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonProfilePictureUrl);
                    Uri uri = new Uri(String.Format(Constants.EcsApiUrl + "images/" + objectProfile["pic"], string.Empty));
                    entryProfilePic.Source = ImageSource.FromUri(uri);
                }
                else
                {
                    entryProfilePic.BackgroundColor = Color.AliceBlue;
                }

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
            else
            {
                Navigation.InsertPageBefore(new LoginPage(), this);
                await Navigation.PopAsync();
            }
        }

        public async void MainMenuItem_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainMenuItem;

            if (item != null)
            {
                if (item.Title.Equals("List All Clocks"))
                {
                    await Navigation.PushAsync(new ListClocks());
                }
                else if (item.Title.Equals("Logout"))
                {
                    Constants.RemoveAll();
                    Navigation.InsertPageBefore(new LoginPage(), this);
                    await Navigation.PopAsync();
                }

                MenuListView.SelectedItem = null;
                IsPresented = false;
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

        private async void ClockUser()
        {
            var clockModel = new ClockModel();
            var result = new ClockResponseModel();

            var usersLocation = await GetUsersLocationAsync();

            if (usersLocation != null)
            {
                clockModel.Email = await Constants.GetEmail();
                clockModel.ClockLocation = usersLocation;
                clockModel.ClockTime = DateTime.UtcNow.AddHours(3);

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