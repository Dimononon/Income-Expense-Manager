using BlazorClient.Models;
using Newtonsoft.Json;
using System.Text;
using Task12.API.Authentication;
using Task12.Models;

namespace BlazorClient.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public AuthenticationService(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }
        public async Task<UserResponse> RegistrateUser(UserAccount user)
        {
            var request = new UserRequest(user.UserName, user.Password, user.Role);
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/Authentication", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserResponse>(responseString);
        }

        public async Task<UserAccount?> GetUser(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/Authentication/{id}");
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var userresponse = JsonConvert.DeserializeObject<UserResponse>(responseString);
                return new UserAccount() { Id = userresponse.Id, UserName = userresponse.UserName, Role = userresponse.Role };
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
        public async Task<UserAccount?> GetUser(string username)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/Authentication/{username}");
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var userresponse = JsonConvert.DeserializeObject<UserResponse>(responseString);
                return new UserAccount() { Id = userresponse.Id, UserName = userresponse.UserName, Role = userresponse.Role };
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task UpdateUser(Guid id, UserAccount user)
        {
            var request = new UserRequest(user.UserName, user.Password, user.Role);
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/Authentication/{id}", content);
            response.EnsureSuccessStatusCode();
        }
        public async Task<bool> ValidateUser(UserModel user)
        {
            var request = new ValidateRequest(user.UserName, user.Password);
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/Authentication", content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
