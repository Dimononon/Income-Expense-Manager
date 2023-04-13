
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task12.Models
{
    public class FinancialOperation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Please input name of operation")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Please input amount")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage ="Please input date and time operation")]
        public DateTime DT { get; set; }
        public DateTime LastModified { get; set;}
        [Required]
        public Guid IncomeTypeId { get; set;  }
        [Required]
        public Guid ExpenseTypeId { get; set;  }
        public OperationType OpType { get; set; }
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
        public FinancialOperation(Guid id, string name, decimal amount, DateTime date, DateTime lm, Guid incomeTypeId, Guid expenseTypeId, OperationType operationType)
        {
            Id = id;
            Name = name;
            Amount = amount;
            DT = date;
            LastModified = lm;
            IncomeTypeId = incomeTypeId;
            ExpenseTypeId = expenseTypeId;
            OpType = operationType;
        }
    }
}
