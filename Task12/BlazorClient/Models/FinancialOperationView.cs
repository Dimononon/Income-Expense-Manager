using Task12.Models;

namespace BlazorClient.Models
{
    public class FinancialOperationView : FinancialOperation
    {
        public string? TypeName { get; set; }
        public string? Currency { get; set; }
        public string? AmountIn { get; set; }
        public FinancialOperationView()
        {

        }
        public FinancialOperationView(FinancialOperation op)
        {
            Id = op.Id;
            UserId = op.UserId;
            Name = op.Name;
            ExpenseTypeId = op.ExpenseTypeId;
            IncomeTypeId = op.IncomeTypeId;
            DT = op.DT;
            LastModified = op.LastModified;
            Amount = op.Amount;
            TypeName = String.Empty;
            OpType = op.OpType;
            Currency = "UAH";
            AmountIn = String.Empty;
        }
        public FinancialOperation ToFinancialOperation(List<ExchangeRate> exchangeRates)
        {
            decimal amount = 0;
            foreach(var exchangeRate in exchangeRates)
            {
                if(exchangeRate.currency == Currency)
                {
                    amount = Amount * exchangeRate.saleRateNB;
                    break;
                }
            }
            return new FinancialOperation
            {
                Id = Id,
                UserId = UserId,
                Name = Name,
                ExpenseTypeId = ExpenseTypeId,
                IncomeTypeId = IncomeTypeId,
                DT = DateTime.Now,
                LastModified = LastModified,
                Amount = amount,
                OpType = OpType
            };
        }
    }
}
