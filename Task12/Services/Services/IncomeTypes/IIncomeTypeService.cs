using Task12.Models;

namespace Task12.Services.IncomeTypes
{
    public interface IIncomeTypeService
    {
        Task CreateIncomeType(IncomeType incomeType);
        Task DeleteIncomeType(Guid id);
        Task<IncomeType> GetIncomeType(Guid id);
        Task<bool> UpdateIncomeType(IncomeType incomeType);
    }
}