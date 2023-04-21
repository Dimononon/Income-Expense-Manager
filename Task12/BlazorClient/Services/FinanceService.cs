using Newtonsoft.Json;
using System.Text;
using Task12.API.Expenses;
using Task12.API.Incomes;
using Task12.API.Reports;
using Task12.Models;

namespace BlazorClient.Services
{
    public class FinanceService : IFinanceService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public FinanceService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration.GetValue<string>("ApiConnection");
        }

        public async Task<ExpenseResponse> CreateExpense(FinancialOperation model)
        {
            var request = new CreateExpenseRequest(model.UserId, model.Name, model.DT, model.ExpenseTypeId, model.Amount);
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

        public async Task UpdateExpense(Guid id, FinancialOperation model)
        {
            var request = new UpsertExpenseRequest(model.UserId,model.Name, model.DT, DateTime.UtcNow, model.ExpenseTypeId, model.Amount);
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

        public async Task<IncomeResponse> CreateIncome(FinancialOperation model)
        {
            var request = new CreateIncomeRequest(model.UserId,model.Name, model.DT, model.IncomeTypeId, model.Amount);
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

        public async Task UpdateIncome(Guid id, FinancialOperation model)
        {
            var request = new UpsertIncomeRequest(model.UserId, model.Name, model.DT, DateTime.UtcNow, model.IncomeTypeId, model.Amount);
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
        public async Task<PeriodReportResponse> GetPeriodReport(DateTime start, DateTime end, Guid userId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/periodreport?dateStart={start:s}&dateEnd={end:s}&userid={userId}");
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
