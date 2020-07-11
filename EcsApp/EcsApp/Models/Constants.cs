using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EcsApp.Models
{
    public static class Constants
    {
        public static string EcsApiUrl { get; } = "http://192.168.43.129:45457/api/user/";

        public async static void SaveUsersDetails(AuthenticationModel user)
        {
            try
            {
                await SecureStorage.SetAsync("token", user.Token);
                await SecureStorage.SetAsync("refreshToken", user.RefreshToken);
                await SecureStorage.SetAsync("refreshTokenExpires", user.RefreshTokenExpiration.ToString());
                await SecureStorage.SetAsync("email", user.Email);
            }
            catch
            {
                // Possible that the device doesn't support secure store but it's not important to the user, we can skip it.
            }
        }

        public async static Task<string> GetOuthToken()
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                return token;
            }
            catch
            {
                return null;
            }
        }

        public async static Task<string> GetRefreshToken()
        {
            try
            {
                var token = await SecureStorage.GetAsync("refreshToken");
                return token;
            }
            catch
            {
                // Error not important to the user
                return null;
            }
        }

        public async static Task<bool> IsRefreshTokenExpired()
        {
            try
            {
                var tokenExpireyDate = DateTime.Parse(await SecureStorage.GetAsync("refreshTokenExpires"));

                if (DateTime.UtcNow >= tokenExpireyDate)
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
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

        public static void RemoveAll()
        {
            SecureStorage.RemoveAll();
        }
    }
}
