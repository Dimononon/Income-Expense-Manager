using Task12.Models;

namespace Task12.Services.IncomeTypes
{
    public class IncomeTypeService : IIncomeTypeService
    {
        private readonly AppDBContext _dbContext;

        public IncomeTypeService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateIncomeType(IncomeType incomeType)
        {
            await _dbContext.Incomes.AddAsync(incomeType);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> UpdateIncomeType(IncomeType incomeType)
        {
            if (await _dbContext.Incomes.FindAsync(Guid.Parse(incomeType.Id.ToString())) != null)
            {
                _dbContext.Incomes.Update(incomeType);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                await _dbContext.Incomes.AddAsync(incomeType);
                await _dbContext.SaveChangesAsync();
                return false;
            }
        }
        public async Task DeleteIncomeType(Guid id)
        {

            var type = await _dbContext.Incomes.FindAsync(Guid.Parse(id.ToString()));
            if (type != null)
            {
                _dbContext.Incomes.Remove(type);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        public async Task<IncomeType> GetIncomeType(Guid id)
        {
            var type = await _dbContext.Incomes.FindAsync(Guid.Parse(id.ToString()));
            if (type != null)
            {
                return type;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        public List<IncomeType> GetAllIncomeType()
        {
            var types = _dbContext.Incomes.ToList();
            if (types != null)
            {
                return types;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
