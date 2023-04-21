using Task12.Models;

namespace Task12.Services.Reports
{
    public class ReportService : IReportService
    {
        private readonly AppDBContext _dbContext;
        public ReportService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public DailyReport GetDailyReport(DateOnly date, Guid userId)
        {
            DateTime dt = date.ToDateTime(TimeOnly.MinValue).Date;
            List<FinancialOperation> operations = _dbContext.FinancialOperations.Where(o => o.UserId == userId && o.DT.Date == dt).ToList();
            decimal totalIncome = 0;
            decimal totalExpense = 0;
            foreach (var operation in operations)
            {
                if (operation.IncomeTypeId != Guid.Empty)
                {
                    totalIncome += operation.Amount;
                }
                else
                {
                    totalExpense += operation.Amount;
                }
            }
            return new DailyReport(date, totalIncome, totalExpense, operations);
        }
        public PeriodReport GetPeriodReport(DateOnly dateStart, DateOnly dateEnd, Guid userId)
        {
            DateTime dateS = dateStart.ToDateTime(TimeOnly.MinValue).Date;
            DateTime dateE = dateEnd.ToDateTime(TimeOnly.MinValue).Date;
            List<FinancialOperation> operations = _dbContext.FinancialOperations.Where(o => o.UserId == userId && o.DT.Date >= dateS && o.DT.Date <= dateE).ToList();
            decimal totalIncome = 0;
            decimal totalExpense = 0;
            foreach (var operation in operations)
            {
                if (operation.IncomeTypeId != Guid.Empty)
                {
                    totalIncome += operation.Amount;
                }
                else
                {
                    totalExpense += operation.Amount;
                }
            }
            return new PeriodReport(dateStart, dateEnd, totalIncome, totalExpense, operations);
        }
    }
}
