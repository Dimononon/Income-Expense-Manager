using Task12.Models;

namespace Task12.Services.Expenses
{
    public class ExpenseService : IExpenseService
    {
        private readonly AppDBContext _dbContext;
        public ExpenseService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateExpense(FinancialOperation expense)
        {
            _dbContext.FinancialOperations.Add(expense);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteExpense(Guid id)
        {
            var expense = await _dbContext.FinancialOperations.FindAsync(id);
            if (expense != null)
            {
                _dbContext.FinancialOperations.Remove(expense);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public async Task<FinancialOperation> GetExpanse(Guid id)
        {
            var expense = await _dbContext.FinancialOperations.FindAsync(id);

            if (expense != null)
            {
                return expense;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public async Task UpdateExpense(FinancialOperation expense)
        {
            var existingInstance = await _dbContext.Set<FinancialOperation>().FindAsync(expense.Id);
            if (existingInstance != null)
            {
                _dbContext.Entry(existingInstance).CurrentValues.SetValues(expense);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
