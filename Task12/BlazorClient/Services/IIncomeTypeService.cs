using Task12.API.IncomeTypes;

namespace BlazorClient.Services
{
    public interface IIncomeTypeService
    {
        Task<IncomeTypeResponse> CreateIncomeType(CreateIncomeTypeRequest request);
        Task DeleteIncome(Guid id);
        Task<string> GetIncomeTypeName(Guid guid);
        Task<IncomeTypeResponse> GetIncomeType(Guid id);
        Task UpdateIncomeType(Guid id, UpsertIncomeTypeRequest request);
        Task<AllIncomeTypeResponse> GetAllIncomeTypes();
    }
}