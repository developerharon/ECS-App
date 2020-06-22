using EcsApp.Models;
using System.Threading.Tasks;

namespace EcsApp.Services
{
    public interface IClockingService
    {
        Task<LocationModel> GetEmployeeLocationAsync();
    }
}
