using CompanyEmployee.DTOs;
using CompanyEmployee.Models;
using CompanyEmployee.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyEmployee.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        public EmployeeController(IEmployeeService employeeService,
                                  IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        #region Index
        public async Task<IActionResult> Index(int? take, int? page)
        {
            ViewBag.DepartmentSelectList = await GetDepartmentSelectList();
            return View(await _employeeService.GetAllAsync(take, page));
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.DepartmentSelectList = await GetDepartmentSelectList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeDTO employeeDTO)
        {
            ViewBag.DepartmentSelectList = await GetDepartmentSelectList();
            if (!ModelState.IsValid) return View(employeeDTO);
            await _employeeService.CreateAsync(employeeDTO);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) NotFound();
            ViewBag.DepartmentSelectList = await GetDepartmentSelectList();
            return View(await _employeeService.FindDTOAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, EmployeeDTO employeeDTO)
        {
            if(id <= 0) NotFound();
            ViewBag.DepartmentSelectList = await GetDepartmentSelectList();
            if (!ModelState.IsValid) return View(employeeDTO);
            await _employeeService.UpdateAsync(id, employeeDTO);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return NotFound();
            await _employeeService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region DepartmentEmployees
        public async Task<IActionResult> DepartmentEmployees(int departmentId, int? take , int? page)
        {
            if(departmentId <= 0) NotFound();
            return View(await _employeeService.DepartmentEmployeesAsync(departmentId, take, page));
        }
        #endregion

        #region GetDepartmentSelectList
        private async Task<SelectList> GetDepartmentSelectList()
        {
            List<Department> departments = await _departmentService.GetListAsync();
            return new SelectList(departments, "Id", "Name");
        }
        #endregion
    }
}
