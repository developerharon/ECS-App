using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EcsApp.Services
{
    public interface IClockingService
    {
        Task<Location> GetEmployeeLocationAsync();
    }
}
