using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task12.Models
{
    public class FinancialOperation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime DT { get; set; }
        public DateTime LastModified { get; set;}
        public Guid IncomeTypeId { get; set;  }
        public Guid ExpenseTypeId { get; set;  }
        public FinancialOperation()
        {
            Id = Guid.NewGuid();
            Name = "";
            Amount = 0;
            DT= DateTime.MinValue;
            LastModified = DateTime.Now;
            IncomeTypeId = Guid.Empty;
            ExpenseTypeId = Guid.Empty;
        }
        public FinancialOperation(Guid id, string name, decimal amount, DateTime date, DateTime lm, Guid incomeTypeId, Guid expenseTypeId)
        {
            Id = id;
            Name = name;
            Amount = amount;
            DT = date;
            LastModified = lm;
            IncomeTypeId = incomeTypeId;
            ExpenseTypeId = expenseTypeId;
        }
    }
}
