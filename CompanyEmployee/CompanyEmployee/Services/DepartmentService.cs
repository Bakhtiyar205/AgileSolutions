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
    public class DepartmentService:IDepartmentService
    {
        private readonly AppDbContext _context;
        public DepartmentService(AppDbContext context)
        {
            _context = context;
        }

        #region List
        public async Task<List<Department>> GetListAsync()
        {
            return await _context.Departments
                   .Where(m => !m.IsDeleted)
                   .OrderByDescending(m => m.Id)
                   .ToListAsync();
        }
        #endregion

        #region All
        public async Task<Paginate<DepartmentDTO>> GetAllAsync(int? take, int? page)
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

            List<DepartmentDTO> departments = await _context.Departments
                                                    .Where(m => !m.IsDeleted)
                                                    .Include(m => m.Employees)
                                                    .OrderByDescending(m => m.Id)
                                                    .Skip((newPage - 1) * newTake)
                                                    .Take(newTake)
                                                    .Select(m => new DepartmentDTO
                                                    {
                                                        Id = m.Id,
                                                        Name = m.Name,
                                                        Employees = m.Employees,
                                                        IsDeleted = m.IsDeleted,
                                                    })
                                                    .ToListAsync();
            int countPages = await GetPageCount(newTake);
            Paginate<DepartmentDTO> resultDepartment = new(departments, newPage, countPages);
            return resultDepartment;
        }
        #endregion

        #region Create
        public async Task CreateAsync(DepartmentDTO departmentDTO)
        {
            Department department = new()
            {
                Id = departmentDTO.Id,
                Name = departmentDTO.Name,
            };
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Update
        public async Task UpdateAsync(int id,DepartmentDTO departmentDTO)
        {
            Department department = await FindAsync(id);
            department.Name = departmentDTO.Name;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(int id)
        {
            Department department = await FindAsync(id);
            department.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
        #endregion

        #region FindDTO
        public async Task<DepartmentDTO> FindDTOAsync(int id)
        {
            return await _context.Departments
                        .Where(m => m.Id == id)
                        .Select(m => new DepartmentDTO
                        {
                            Name = m.Name,
                            IsDeleted = m.IsDeleted,
                            Employees = m.Employees,
                            Id = m.Id
                        })
                        .FirstOrDefaultAsync();

        }
        #endregion

        #region Find
        private async Task<Department> FindAsync(int id)
        {
            return await _context.Departments
                         .FirstOrDefaultAsync(m => m.Id == id);
        }
        #endregion

        #region PageCount
        private async Task<int> GetPageCount(int take)
        {
            return (int)Math.Ceiling((decimal)await _context.Departments
                                                               .Where(m => !m.IsDeleted)
                                                               .CountAsync() / take);
        }
        #endregion
    }
}
