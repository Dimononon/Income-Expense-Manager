using Task12.API.Expenses;
using Task12.API.Incomes;
using Task12.API.Reports;

namespace BlazorClient.Services
{
    public interface IFinanceService
    {
        Task<ExpenseResponse> CreateExpense(CreateExpenseRequest request);
        Task<IncomeResponse> CreateIncome(CreateIncomeRequest request);
        Task DeleteExpense(Guid id);
        Task DeleteIncome(Guid id);
        Task<DailyReportResponse> GetDailyReport(DateTime date);
        Task<ExpenseResponse> GetExpense(Guid id);
        Task<IncomeResponse> GetIncome(Guid id);
        Task<PeriodReportResponse> GetPeriodReport(DateTime start, DateTime end);
        Task UpdateExpense(Guid id, UpsertExpenseRequest request);
        Task UpdateIncome(Guid id, UpsertIncomeRequest request);
    }
}