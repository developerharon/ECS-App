using System.Threading.Tasks;
using Xamarin.Essentials;
using EcsApp.Models.ApiModels;

namespace EcsApp.Models
{
    public static class Constants
    {
        public static string EcsApiUrl { get; } = "http://192.168.43.129:45457/";

        public async static void SaveUsersDetails(LoginModel model)
        {
            try
            {
                await SecureStorage.SetAsync("email", model.Email);
                await SecureStorage.SetAsync("password", model.Password);
            }
            catch
            {
                // Possible that the device doesn't support secure store but it's not important to the user, we can skip it.
            }
        }

        public async static Task<string> GetEmail()
        {
            try
            {
                var email = await SecureStorage.GetAsync("email");
                return email;
            }
            catch
            {
                return null;
            }
        }

        public async static Task<string> GetPassword()
        {
            try
            {
                var password = await SecureStorage.GetAsync("password");
                return password;
            }
            catch
            {
                return null;
            }
        }

        public static void RemoveAll()
        {
            SecureStorage.RemoveAll();
        }
    }
}
