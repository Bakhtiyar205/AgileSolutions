using CompanyEmployee.DTOs;
using CompanyEmployee.Helper.Pagination;
using CompanyEmployee.Models;
using System.Threading.Tasks;

namespace CompanyEmployee.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Paginate<EmployeeDTO>> GetAllAsync(int? take, int? page);
        Task<Paginate<EmployeeDTO>> DepartmentEmployeesAsync(int departmentId, int? take, int? page);
        Task CreateAsync(EmployeeDTO employeeDTO);
        Task UpdateAsync(int id, EmployeeDTO employeeDTO);
        Task DeleteAsync(int id);
        Task<EmployeeDTO> FindDTOAsync(int id);
    }
}
