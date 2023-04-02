namespace Task12.Models
{
    public class DailyReport
    {
        public DateOnly Date { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public List<FinancialOperation> Operations { get; set; }
        public DailyReport(DateOnly date, decimal totalIncome, decimal totalExpense, List<FinancialOperation> operations)
        {
            Date = date;
            TotalIncome = totalIncome;
            TotalExpense = totalExpense;
            Operations = operations;
        }
    }
}
