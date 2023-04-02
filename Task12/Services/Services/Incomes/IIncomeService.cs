using Task12.Models;

namespace Task12.Services.Incomes
{
    public interface IIncomeService
    {
        Task CreateIncome(FinancialOperation income);
        Task DeleteIncome(Guid id);
        Task<FinancialOperation> GetIncome(Guid id);
        Task UpdateIncome(FinancialOperation income);
    }
}