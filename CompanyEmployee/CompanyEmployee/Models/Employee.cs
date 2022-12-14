using CompanyEmployee.Helper.Attribute;
using System;
using System.ComponentModel.DataAnnotations;

namespace CompanyEmployee.Models
{
    public class Employee:BaseEntity
    {
        [Required]
        [DataType(DataType.Text), MaxLength(155)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text), MaxLength(155)]
        public string Surname { get; set; }
        [Required]
        [DataType(DataType.Text), MaxLength(7), MinLength(7)]
        public string Pin { get; set; }
        [Required]
        [DataType(DataType.Text), MaxLength(6), MinLength(6)]
        public string SerialNumber { get; set; }
        [Required]
        [DataType(DataType.Text), MaxLength(3), MinLength(1)]
        public string Serie { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; } = DateTime.Now;
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
