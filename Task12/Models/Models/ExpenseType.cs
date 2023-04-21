using System.ComponentModel.DataAnnotations;

namespace Task12.Models
{
    public class ExpenseType
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Please input name")]
        public string Name { get; set; }
        public DateTime LastModified { get; set; }
        public ExpenseType(Guid id, Guid userId,string name, DateTime lastModified)
        {
            Id = id;
            UserId = userId;
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
