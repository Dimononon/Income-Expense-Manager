using Task12.Models;

namespace Task12.Services.ExpenseTypes
{
    public class ExpenseTypeService : IExpenseTypeService
    {
        private readonly AppDBContext _dbContext;

        public ExpenseTypeService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateExpenseType(ExpenseType expenseType)
        {
            _dbContext.Expenses.Add(expenseType);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> UpdateExpenseType(ExpenseType expenseType)
        {
            if (await _dbContext.Incomes.FindAsync(Guid.Parse(expenseType.Id.ToString())) != null)
            {
                _dbContext.Expenses.Update(expenseType);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                await _dbContext.Expenses.AddAsync(expenseType);
                await _dbContext.SaveChangesAsync();
                return false;
            }
        }
        public async Task DeleteExpenseType(Guid id)
        {

            var type = await _dbContext.Expenses.FindAsync(Guid.Parse(id.ToString()));
            if (type != null)
            {
                _dbContext.Expenses.Remove(type);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        public async Task<ExpenseType> GetExpenseType(Guid id)
        {
            var type = await _dbContext.Expenses.FindAsync(Guid.Parse(id.ToString()));
            if (type != null)
            {
                return type;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public List<ExpenseType> GetAllExpenseType()
        {
            var types = _dbContext.Expenses.ToList();
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
