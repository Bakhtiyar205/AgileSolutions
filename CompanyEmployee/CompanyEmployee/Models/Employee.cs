using System.ComponentModel.DataAnnotations;

namespace CompanyEmployee.Models
{
    public class Employee:BaseEntity
    {
        [Required]
        [DataType(DataType.Text), MaxLength(155)]
        public string Name { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
