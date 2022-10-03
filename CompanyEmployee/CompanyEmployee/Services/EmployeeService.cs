using CompanyEmployee.Datas;
using CompanyEmployee.DTOs;
using CompanyEmployee.Helper.Pagination;
using CompanyEmployee.Models;
using CompanyEmployee.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployee.Services
{
    public class EmployeeService:IEmployeeService
    {
        private readonly AppDbContext _context;
        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        #region All
        public async Task<Paginate<EmployeeDTO>> GetAllAsync(int? take, int? page)
        {
            int newPage;
            int newTake;
            if (page == null)
            {
                newPage = 1;
            }
            else
            {
                newPage = (int)page;
            }

            if (take == null)
            {
                newTake = 3;
            }
            else
            {
                newTake = (int)take;
            }

            List<EmployeeDTO> employees = await _context.Employees
                                       .Where(m => !m.IsDeleted)
                                       .Include(m => m.Department)
                                       .OrderByDescending(m => m.Id)
                                       .Skip((newPage - 1) * newTake)
                                       .Take(newTake)
                                       .Select(m => new EmployeeDTO
                                       {
                                           Id = m.Id,
                                           Name = m.Name,
                                           Surname = m.Surname,
                                           BirthDate = m.BirthDate,
                                           Serie= m.Serie,
                                           SerialNumber = m.SerialNumber,
                                           Pin = m.Pin,
                                           Department = m.Department,
                                           DepartmentId = m.DepartmentId,
                                           IsDeleted = m.IsDeleted,
                                       })
                                       .ToListAsync();
            int countPages = await GetPageCount(newTake);
            Paginate<EmployeeDTO> resultEmployee = new(employees, newPage, countPages);
            return resultEmployee;

        }
        #endregion

        #region DepartmentEmployees
        public async Task<Paginate<EmployeeDTO>> DepartmentEmployeesAsync(int departmentId, int? take, int? page)
        {
            int newPage;
            int newTake;
            if (page == null)
            {
                newPage = 1;
            }
            else
            {
                newPage = (int)page;
            }

            if (take == null)
            {
                newTake = 1;
            }
            else
            {
                newTake = (int)take;
            }

            List<EmployeeDTO> employees = await _context.Employees
                                       .Where(m => !m.IsDeleted && m.DepartmentId == departmentId)
                                       .Include(m => m.Department)
                                       .OrderByDescending(m => m.Id)
                                       .Skip((newPage - 1) * newTake)
                                       .Take(newTake)
                                       .Select(m => new EmployeeDTO
                                       {
                                           Id = m.Id,
                                           Name = m.Name,
                                           Surname = m.Surname,
                                           BirthDate = m.BirthDate,
                                           Serie = m.Serie,
                                           SerialNumber = m.SerialNumber,
                                           Pin = m.Pin,
                                           Department = m.Department,
                                           DepartmentId = m.DepartmentId,
                                           IsDeleted = m.IsDeleted,
                                       })
                                       .ToListAsync();
            int countPages = await GetDepartmentEmployeePageCount(newTake,departmentId);
            Paginate<EmployeeDTO> resultEmployee = new(employees, newPage, countPages);
            return resultEmployee;
        }
        #endregion

        #region Create
        public async Task CreateAsync(EmployeeDTO employeeDTO)
        {
            Employee employee = new()
            {
                Name = employeeDTO.Name,
                Surname = employeeDTO.Surname,
                BirthDate = employeeDTO.BirthDate,
                Serie = employeeDTO.Serie,
                SerialNumber = employeeDTO.SerialNumber,
                Pin = employeeDTO.Pin,
                Department = employeeDTO.Department,
                DepartmentId = employeeDTO.DepartmentId,
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Update
        public async Task UpdateAsync(int id,EmployeeDTO employeeDTO)
        {
            Employee employee = await FindAsync(id);
            employee.Name = employeeDTO.Name;
            employee.DepartmentId = employeeDTO.DepartmentId;
            employee.Pin = employeeDTO.Pin;
            employee.SerialNumber = employeeDTO.SerialNumber;
            employee.Serie = employeeDTO.Serie;
            employee.Surname = employeeDTO.Surname;
            employee.BirthDate = employeeDTO.BirthDate;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(int id)
        {
            Employee employee = await FindAsync(id);
            employee.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region FindDTO
        public async Task<EmployeeDTO> FindDTOAsync(int id)
        {
            return await _context.Employees
                         .Where(m => m.Id == id)
                         .Select(m => new EmployeeDTO
                         {
                             Name=m.Name,
                             Surname=m.Surname,
                             BirthDate=m.BirthDate,
                             Serie=m.Serie,
                             SerialNumber =m.SerialNumber,
                             Pin=m.Pin,
                             DepartmentId=m.DepartmentId,
                             Department = m.Department,
                             IsDeleted=m.IsDeleted,
                             Id = m.Id
                         })
                         .FirstOrDefaultAsync();
        }
        #endregion

        #region Find
        private async Task<Employee> FindAsync(int id)
        {
            return await _context.Employees
                        .FirstOrDefaultAsync(m => m.Id == id);
        }
        #endregion

        #region PageCount
        private async Task<int> GetPageCount(int take)
        {
            return (int)Math.Ceiling((decimal)await _context.Employees
                                                            .Where(m => !m.IsDeleted)
                                                            .CountAsync() / take);
        }

        private async Task<int> GetDepartmentEmployeePageCount(int take, int departmentId)
        {
            return (int)Math.Ceiling((decimal)await _context.Employees
                                                            .Where(m => !m.IsDeleted 
                                                            && m.DepartmentId == departmentId)
                                                            .CountAsync() / take);
        }
        #endregion
    }
}
