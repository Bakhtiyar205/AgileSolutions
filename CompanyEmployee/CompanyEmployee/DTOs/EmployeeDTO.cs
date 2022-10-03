using CompanyEmployee.Helper.Attribute;
using CompanyEmployee.Models;
using System;
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
        public DateTime BirthDate { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
