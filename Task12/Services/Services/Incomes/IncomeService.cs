using Task12.Models;

namespace Task12.Services.Incomes
{
    public class IncomeService : IIncomeService
    {
        private readonly AppDBContext _dbContext;
        public IncomeService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateIncome(FinancialOperation income)
        {
            _dbContext.FinancialOperations.Add(income);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteIncome(Guid id)
        {
            var income = await _dbContext.FinancialOperations.FindAsync(id);
            if (income != null)
            {
                _dbContext.FinancialOperations.Remove(income);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public async Task<FinancialOperation> GetIncome(Guid id)
        {
            var income = await _dbContext.FinancialOperations.FindAsync(id);

            if (income != null)
            {
                return income;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public async Task UpdateIncome(FinancialOperation income)
        {
            var existingInstance = await _dbContext.Set<FinancialOperation>().FindAsync(income.Id);
            if (existingInstance != null)
            {
                _dbContext.Entry(existingInstance).CurrentValues.SetValues(income);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
