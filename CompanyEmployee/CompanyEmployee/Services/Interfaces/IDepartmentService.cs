using CompanyEmployee.DTOs;
using CompanyEmployee.Helper.Pagination;
using CompanyEmployee.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyEmployee.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<Paginate<DepartmentDTO>> GetAllAsync(int? take, int? page);
        Task CreateAsync(DepartmentDTO departmentDTO);
        Task UpdateAsync(int id, DepartmentDTO departmentDTO);
        Task DeleteAsync(int id);
        Task<List<Department>> GetListAsync();
        Task<DepartmentDTO> FindDTOAsync(int id);
    }
}
