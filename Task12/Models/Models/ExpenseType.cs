using System.ComponentModel.DataAnnotations;

namespace Task12.Models
{
    public class ExpenseType
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime LastModified { get; set; }
        public ExpenseType(Guid id, string name, DateTime lastModified)
        {
            Id = id;
            Name = name;
            LastModified = lastModified;
        }
        public ExpenseType()
        {
            Id= Guid.Empty;
            Name = string.Empty;
            LastModified = DateTime.MinValue;
        }
    }
}
