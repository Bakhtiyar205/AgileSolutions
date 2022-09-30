using CompanyEmployee.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyEmployee.DTOs
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(155)]
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
