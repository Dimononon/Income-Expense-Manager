using Task12.API.ExpenseTypes;
using Task12.Models;

namespace BlazorClient.Services
{
    public interface IExpenseTypeService
    {
        Task<ExpenseTypeResponse> CreateExpenseType(ExpenseType expenseType);
        Task DeleteExpense(Guid id);
        Task<ExpenseTypeResponse> GetExpenseType(Guid id);
        Task UpdateExpenseType(Guid id, ExpenseType expenseType);
        Task<string> GetExpenseTypeName(Guid guid);
        Task<AllExpenseTypeResponse> GetAllExpenseTypes();
    }
}