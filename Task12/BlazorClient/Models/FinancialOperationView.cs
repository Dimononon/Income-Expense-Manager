using Task12.Models;

namespace BlazorClient.Models
{
    public class FinancialOperationView : FinancialOperation
    {
        public string TypeName { get; set; }
        public FinancialOperationView(FinancialOperation op)
        {
            Id = op.Id;
            Name = op.Name;
            ExpenseTypeId = op.ExpenseTypeId;
            IncomeTypeId = op.IncomeTypeId;
            DT = op.DT;
            LastModified = op.LastModified;
            Amount = op.Amount;
            TypeName = String.Empty;
            OpType = op.OpType;
        }
    }
}
