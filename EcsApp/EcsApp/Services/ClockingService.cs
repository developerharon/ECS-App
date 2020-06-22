using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EcsApp.Services
{
    class ClockingService : IClockingService
    {
        public async Task<Location> GetEmployeeLocationAsync()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(1));
            var location = await Geolocation.GetLocationAsync(request);

            if (location != null)
                return location;
            else
                return null;
        }
    }
}