using EcsApp.Models.ApiModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EcsApp.Models
{
    class UserService : IUserServices
    {
        private readonly HttpClient _client;

        public UserService()
        {
            _client = new HttpClient();
        }

        public UserService(string authenticationToken)
        {
            var authenticationHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationToken));
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer Token", authenticationHeader);
        }

        public async Task<AuthenticationModel> GetTokenAsync(LoginModel model)
        {
            Uri uri = new Uri(String.Format(Constants.EcsApiUrl + "login", string.Empty));

            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                AuthenticationModel responseModel = JsonConvert.DeserializeObject<AuthenticationModel>(responseContent);
                return responseModel;
            }
            return null;
        }

        public async Task<AuthenticationModel> GetRefreshTokenAsync(string refreshToken)
        {
            Uri uri = new Uri(String.Format(Constants.EcsApiUrl + "refresh-token", string.Empty));

            string json = JsonConvert.SerializeObject(refreshToken);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                AuthenticationModel responseModel = JsonConvert.DeserializeObject<AuthenticationModel>(responseContent);
                return responseModel;
            }
            return null;
        }

        public async Task<string> RevokeToken(string refreshToken)
        {
            Uri uri = new Uri(String.Format(Constants.EcsApiUrl + "revoke-token", string.Empty));

            string json = JsonConvert.SerializeObject(refreshToken);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                string responseModel = JsonConvert.DeserializeObject<string>(responseContent);
                return responseModel;
            }
            return null;
        }

        public async Task<ClockResponseModel> ClockInAsync(ClockModel model)
        {
            Uri uri = new Uri(String.Format(Constants.EcsApiUrl + "clock-in", string.Empty));

            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                ClockResponseModel responseModel = JsonConvert.DeserializeObject<ClockResponseModel>(responseContent);
                return responseModel;
            }
            return null;
        }

        public async Task<ClockResponseModel> ClockOutAsync(ClockModel model)
        {
            Uri uri = new Uri(String.Format(Constants.EcsApiUrl + "clock-out", string.Empty));

            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                ClockResponseModel responseModel = JsonConvert.DeserializeObject<ClockResponseModel>(responseContent);
                return responseModel;
            }
            return null;
        }

        public async Task<ClockResponseModel> GetApplicationState(string email)
        {
            Uri uri = new Uri(String.Format(Constants.EcsApiUrl + "application-state", string.Empty));

            string json = JsonConvert.SerializeObject(email);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                ClockResponseModel responseModel = JsonConvert.DeserializeObject<ClockResponseModel>(responseContent);
                return responseModel;
            }
            return null;
        }

        public async Task<List<Clock>> GetAllClocksAsync(string email)
        {
            Uri uri = new Uri(String.Format(Constants.EcsApiUrl + "email", string.Empty));

            string json = JsonConvert.SerializeObject(email);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                List<Clock> responseModel = JsonConvert.DeserializeObject<List<Clock>>(responseContent);
                return responseModel;
            }
            return null;
        }
    }
}
