namespace Task12.Models
{
    public class PeriodReport : DailyReport
    {
        public DateOnly DateEnd { get; set; }
        public PeriodReport(DateOnly dateStart, DateOnly dateEnd, decimal totalIncome, decimal totalExpense, List<FinancialOperation> operations) : base(dateStart, totalIncome, totalExpense, operations)
        {
            DateEnd = dateEnd;
        }
    }
}
