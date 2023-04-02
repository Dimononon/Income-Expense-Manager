using Task12.Models;

namespace Task12.Services.Expenses
{
    public interface IExpenseService
    {
        Task CreateExpense(FinancialOperation expense);
        Task UpdateExpense(FinancialOperation expense);
        Task DeleteExpense(Guid id);
        Task<FinancialOperation> GetExpanse(Guid id);
    }
}
