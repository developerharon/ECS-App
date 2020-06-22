using EcsApp.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EcsApp.Services
{
    class ClockingService : IClockingService
    {
        public async Task<LocationModel> GetEmployeeLocationAsync()
        {
            var employeeLocation = new LocationModel();

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(1));
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    employeeLocation.Message = "Location Found";
                    employeeLocation.EmployeeLocation = location;
                    return employeeLocation;
                }
                else
                {
                    employeeLocation.Message = "Location Not Found";
                    return employeeLocation;
                }
            }
            catch (FeatureNotSupportedException)
            {
                employeeLocation.Message = "Location feature is not supported on this device";
                return employeeLocation;
            }
            catch (FeatureNotEnabledException)
            {
                employeeLocation.Message = "Please turn on location";
                return employeeLocation;
            }
            catch (PermissionException)
            {
                employeeLocation.Message = "Allow the device to access your location";
                return employeeLocation;
            }
            catch (Exception)
            {
                employeeLocation.Message = "An error occured while processing your request";
                return employeeLocation;
            }
        }
    }
}