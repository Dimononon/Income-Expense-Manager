using Task12.API.Expenses;
using Task12.API.Incomes;
using Task12.API.Reports;
using Task12.Models;

namespace BlazorClient.Services
{
    public interface IFinanceService
    {
        Task<ExpenseResponse> CreateExpense(FinancialOperation model);
        Task<IncomeResponse> CreateIncome(FinancialOperation model);
        Task DeleteExpense(Guid id);
        Task DeleteIncome(Guid id);
        Task<DailyReportResponse> GetDailyReport(DateTime date);
        Task<ExpenseResponse> GetExpense(Guid id);
        Task<IncomeResponse> GetIncome(Guid id);
        Task<PeriodReportResponse> GetPeriodReport(DateTime start, DateTime end, Guid userId);
        Task UpdateExpense(Guid id, FinancialOperation model);
        Task UpdateIncome(Guid id, FinancialOperation model);
    }
}