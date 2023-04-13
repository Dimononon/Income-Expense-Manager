using Newtonsoft.Json;
using System.Text;
using Task12.API.IncomeTypes;
using Task12.Models;

namespace BlazorClient.Services
{
    public class IncomeTypeService : IIncomeTypeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public IncomeTypeService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetValue<string>("ApiConnection");

        }
        public async Task<IncomeTypeResponse> CreateIncomeType(IncomeType incomeType)
        {
            var request = new CreateIncomeTypeRequest(incomeType.Name);
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/IncomeType", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IncomeTypeResponse>(responseString);
        }

        public async Task<IncomeTypeResponse?> GetIncomeType(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/IncomeType/{id}");
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IncomeTypeResponse>(responseString);
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task UpdateIncomeType(Guid id, IncomeType incomeType)
        {
            var request = new UpsertIncomeTypeRequest(incomeType.Name);
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/IncomeType/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteIncome(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/IncomeType/{id}");
            response.EnsureSuccessStatusCode();
        }
        public async Task<string> GetIncomeTypeName(Guid guid)
        {
            var response = await GetIncomeType(guid);
            if(response == null)
            {
                return String.Empty;
            }
            else
            {
                return response.name;
            }
        }

        public async Task<AllIncomeTypeResponse> GetAllIncomeTypes()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/IncomeType");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AllIncomeTypeResponse>(responseString);
        }
    }
}
