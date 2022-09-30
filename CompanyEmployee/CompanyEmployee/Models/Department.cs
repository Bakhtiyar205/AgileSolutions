using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyEmployee.Models
{
    public class Department:BaseEntity
    {
        [Required]
        [DataType(DataType.Text),MaxLength(155)]
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
