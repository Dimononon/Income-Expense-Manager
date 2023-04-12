using Task12.API.ExpenseTypes;

namespace BlazorClient.Services
{
    public interface IExpenseTypeService
    {
        Task<ExpenseTypeResponse> CreateExpenseType(CreateExpenseTypeRequest request);
        Task DeleteExpense(Guid id);
        Task<ExpenseTypeResponse> GetExpenseType(Guid id);
        Task UpdateExpenseType(Guid id, UpsertExpenseTypeRequest request);
        Task<string> GetExpenseTypeName(Guid guid);
        Task<AllExpenseTypeResponse> GetAllExpenseTypes();
    }
}