using CompanyEmployee.DTOs;
using CompanyEmployee.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CompanyEmployee.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        #region Index
        public async Task<IActionResult> Index(int? take, int? page)
        {
            return View(await _departmentService.GetAllAsync(take, page));
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentDTO departmentDTO)
        {
            if (!ModelState.IsValid) return View(departmentDTO);
            await _departmentService.CreateAsync(departmentDTO);
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return NotFound();
            return View(await _departmentService.FindDTOAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, DepartmentDTO departmentDTO)
        {
            if(id == 0) return NotFound();
            if (!ModelState.IsValid) return View(departmentDTO);
            await _departmentService.UpdateAsync(id, departmentDTO);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return NotFound();
            await _departmentService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int id)
        {
            return View(await _departmentService.FindDTOAsync(id));
        }
        #endregion
    }
}
