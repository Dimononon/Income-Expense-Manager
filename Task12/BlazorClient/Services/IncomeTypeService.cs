using Newtonsoft.Json;
using System.Text;
using Task12.API.IncomeTypes;

namespace BlazorClient.Services
{
    public class IncomeTypeService : IIncomeTypeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5232";

        public IncomeTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IncomeTypeResponse> CreateIncomeType(CreateIncomeTypeRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/IncomeType", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IncomeTypeResponse>(responseString);
        }

        public async Task<IncomeTypeResponse> GetIncomeType(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/IncomeType/{id}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IncomeTypeResponse>(responseString);
        }

        public async Task UpdateIncomeType(Guid id, UpsertIncomeTypeRequest request)
        {
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
            return (await GetIncomeType(guid)).name;
        }
    }
}
