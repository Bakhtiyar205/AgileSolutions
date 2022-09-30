using CompanyEmployee.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployee.Datas
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
