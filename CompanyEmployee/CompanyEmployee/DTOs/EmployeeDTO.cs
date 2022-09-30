using CompanyEmployee.Models;
using System.ComponentModel.DataAnnotations;

namespace CompanyEmployee.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(155)]
        public string Name { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
