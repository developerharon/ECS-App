using EcsApp.Models.ApiModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcsApp.Models
{
    public interface IUserServices
    {
        Task<AuthenticationModel> LoginAsync(LoginModel model);
        Task<ClockResponseModel> ClockInAsync(ClockModel model);
        Task<ClockResponseModel> ClockOutAsync(ClockModel model);
        Task<ClockResponseModel> GetApplicationState(string email);
        Task<List<Clock>> GetAllClocksAsync(string email);
        Task<string> GetProfilePictureUrl(string email);
    }
}
