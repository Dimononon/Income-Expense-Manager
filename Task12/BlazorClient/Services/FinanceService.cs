using Newtonsoft.Json;
using System.Text;
using Task12.API.Expenses;
using Task12.API.Incomes;
using Task12.API.Reports;

namespace BlazorClient.Services
{
    public class FinanceService : IFinanceService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5232";

        public FinanceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ExpenseResponse> CreateExpense(CreateExpenseRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/expense", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ExpenseResponse>(responseString);
        }

        public async Task<ExpenseResponse> GetExpense(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/expense/{id}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ExpenseResponse>(responseString);
        }

        public async Task UpdateExpense(Guid id, UpsertExpenseRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/expense/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteExpense(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/expense/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<IncomeResponse> CreateIncome(CreateIncomeRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/income", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IncomeResponse>(responseString);
        }

        public async Task<IncomeResponse> GetIncome(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/income/{id}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IncomeResponse>(responseString);
        }

        public async Task UpdateIncome(Guid id, UpsertIncomeRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/income/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteIncome(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/income/{id}");
            response.EnsureSuccessStatusCode();
        }
        public async Task<PeriodReportResponse> GetPeriodReport(DateTime start, DateTime end)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/periodreport?dateStart={start:s}&dateEnd={end:s}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PeriodReportResponse>(responseString);

        }
        public async Task<DailyReportResponse> GetDailyReport(DateTime date)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/dailyreport?date={date:s}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DailyReportResponse>(responseString);

        }
    }
}
