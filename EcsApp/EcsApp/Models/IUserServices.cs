using EcsApp.Models.ApiModels;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EcsApp.Models
{
    public interface IUserServices
    {
        Task<AuthenticationModel> GetTokenAsync(LoginModel model);
        Task<AuthenticationModel> GetRefreshTokenAsync(string refreshToken);
        Task<object> RevokeToken(string refreshToken);
        Task<ClockResponseModel> ClockInAsync(ClockModel model);
        Task<ClockResponseModel> ClockOutAsync(ClockModel model);
        Task<ClockResponseModel> GetApplicationState(string email);
        Task<List<Clock>> GetAllClocksAsync(string email);
        Task<string> GetProfilePicAsync(string email);
    }
}
