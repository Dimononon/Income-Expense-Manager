using Newtonsoft.Json;
using System.Text;
using Task12.API.ExpenseTypes;
using Task12.Models;

namespace BlazorClient.Services
{
    public class ExpenseTypeService : IExpenseTypeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ExpenseTypeService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetValue<string>("ApiConnection");
        }
        public async Task<ExpenseTypeResponse> CreateExpenseType(ExpenseType expenseType)
        {
            var request = new CreateExpenseTypeRequest(expenseType.UserId, expenseType.Name);
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/ExpenseType", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ExpenseTypeResponse>(responseString);
        }

        public async Task<ExpenseTypeResponse?> GetExpenseType(Guid id)
        {
            try
            {

                var response = await _httpClient.GetAsync($"{_baseUrl}/ExpenseType/{id}");
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ExpenseTypeResponse>(responseString);
            }
            catch (HttpRequestException)
            {
                return null;
                //return new ExpenseTypeResponse(id, "deleted type", DateTime.UtcNow);
            }
        }

        public async Task UpdateExpenseType(Guid id, ExpenseType expenseType)
        {
            var request = new UpsertExpenseTypeRequest(expenseType.UserId, expenseType.Name);
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
            var response =await GetExpenseType(guid);
            if( response == null)
            {
                return String.Empty;
            }
            else
            {
                return response.Name;
            }
        }

        public async Task<AllExpenseTypeResponse> GetAllExpenseTypes()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/ExpenseType");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AllExpenseTypeResponse>(responseString);
        }
    }
}
