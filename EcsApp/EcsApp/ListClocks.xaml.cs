using EcsApp.Models;
using EcsApp.Models.ApiModels;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EcsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListClocks : ContentPage
    {
        private UserService _userService;
        public ObservableCollection<ClockViewModel> MyList { get; set; }

        public ListClocks()
        {
            InitializeComponent();
            this.BindingContext = this;
            LoadEssentials();
            ContactsList.ItemsSource = MyList;
        }

        private async void LoadEssentials()
        {
            _userService = new UserService();
            MyList = new ObservableCollection<ClockViewModel>();

            try
            {
                if (await Constants.GetEmail() != null)
                {
                    var clocks = await _userService.GetAllClocksAsync(await Constants.GetEmail());

                    if (clocks != null)
                    {
                        foreach (var clock in clocks)
                        {
                            MyList.Add(new ClockViewModel()
                            {
                                InTime = $"{clock.In.Day}/{clock.In.Month} {clock.In.ToShortTimeString()}",
                                InLocation = clock.InWhileOnPremises ? "Premises" : "NoP",
                                OutTime = $"{clock.Out.Day}/{clock.Out.Month} {clock.In.ToShortTimeString()}",
                                OutLocation = clock.OutWhileOnPremises ? "Premises" : "NoP"
                            });
                        }
                    }
                    else
                    {
                        await DisplayAlert("Information", "No Clocks At The Moment For The User", "OK");
                    }
                }
                else
                {
                    Navigation.InsertPageBefore(new LoginPage(), this);
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}