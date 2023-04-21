using System.ComponentModel.DataAnnotations;

namespace Task12.Models
{
    public class IncomeType
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Please input name")]
        public string Name { get; set; }
        public DateTime LastModified { get; set; }
        public IncomeType(Guid id, string name, DateTime lastModified)
        {
            Id = id;
            Name = name;
            LastModified = lastModified;
        }
        public IncomeType()
        {
            Id= Guid.Empty;
            Name= string.Empty;
            LastModified= DateTime.MinValue;
        }
    }
}
