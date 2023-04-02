using Task12.Models;

namespace Task12.Services.ExpenseTypes
{
    public interface IExpenseTypeService
    {
        Task CreateExpenseType(ExpenseType expenseType);
        Task DeleteExpenseType(Guid id);
        Task<ExpenseType> GetExpenseType(Guid id);
        Task<bool> UpdateExpenseType(ExpenseType expenseType);
    }
}