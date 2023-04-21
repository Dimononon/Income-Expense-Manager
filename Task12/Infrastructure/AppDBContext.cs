using Microsoft.EntityFrameworkCore;
using Task12.Models;

namespace Task12
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
        {

        }
        public DbSet<FinancialOperation> FinancialOperations { get; set; }
        public DbSet<ExpenseType> Expenses { get; set; }
        public DbSet<IncomeType> Incomes { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
    }
}
