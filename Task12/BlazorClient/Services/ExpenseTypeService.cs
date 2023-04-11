using Newtonsoft.Json;
using System.Text;
using Task12.API.ExpenseTypes;

namespace BlazorClient.Services
{
    public class ExpenseTypeService : IExpenseTypeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5232";

        public ExpenseTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ExpenseTypeResponse> CreateExpenseType(CreateExpenseTypeRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/ExpenseType", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ExpenseTypeResponse>(responseString);
        }

        public async Task<ExpenseTypeResponse> GetExpenseType(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/ExpenseType/{id}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ExpenseTypeResponse>(responseString);
        }

        public async Task UpdateExpenseType(Guid id, UpsertExpenseTypeRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/ExpenseType/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteExpense(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/ExpenseType/{id}");
            response.EnsureSuccessStatusCode();
        }
        public async Task<string> GetExpenseTypeName(Guid guid)
        {
            return (await GetExpenseType(guid)).Name;
        }
        
    }
}
