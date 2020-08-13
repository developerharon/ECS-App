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

        public async Task<AuthenticationModel> LoginAsync(LoginModel model)
        {
            Uri uri = new Uri(String.Format(Constants.EcsApiUrl + "api/user/login", string.Empty));

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

        public async Task<ClockResponseModel> ClockInAsync(ClockModel model)
        {
            Uri uri = new Uri(String.Format(Constants.EcsApiUrl + "api/user/clock-in", string.Empty));

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
            Uri uri = new Uri(String.Format(Constants.EcsApiUrl + "api/user/clock-out", string.Empty));

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
            Uri uri = new Uri(String.Format(Constants.EcsApiUrl + "api/user/application-state", string.Empty));

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
            Uri uri = new Uri(String.Format(Constants.EcsApiUrl + "api/user/get-clocks", string.Empty));

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

        public async Task<string> GetProfilePictureUrl(string email)
        {
            Uri uri = new Uri(String.Format(Constants.EcsApiUrl + "api/user/profile-picture", string.Empty));

            string json = JsonConvert.SerializeObject(email);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            return null;
        }
    }
}