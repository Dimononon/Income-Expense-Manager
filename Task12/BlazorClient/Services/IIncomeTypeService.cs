using Task12.API.IncomeTypes;
using Task12.Models;

namespace BlazorClient.Services
{
    public interface IIncomeTypeService
    {
        Task<IncomeTypeResponse> CreateIncomeType(IncomeType incomeType);
        Task DeleteIncome(Guid id);
        Task<string> GetIncomeTypeName(Guid guid);
        Task<IncomeTypeResponse> GetIncomeType(Guid id);
        Task UpdateIncomeType(Guid id, IncomeType incomeType);
        Task<AllIncomeTypeResponse> GetAllIncomeTypes();
    }
}