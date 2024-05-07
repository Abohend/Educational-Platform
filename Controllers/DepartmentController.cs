using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;

namespace MVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class DepartmentController(IDepartmentRepository departmentRepository) : Controller
    {
        private readonly IDepartmentRepository departmentRepository = departmentRepository;

        // GET: DepartmentController
        #region index
        public ActionResult Index()
        {
            List<Department> deps = departmentRepository.ReadAll();
            return View(deps);
        }
        #endregion

        // GET: DepartmentController/Details/5
        #region Details
        public ActionResult Details(int id)
        {
            Department? dep = departmentRepository.ReadWithCourses(id);
            if (dep == null)
                return NotFound();
            return View(dep);
        }
        #endregion

        // GET: DepartmentController/Create
        #region Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await departmentRepository.CreateAsync(department);
                return RedirectToAction(nameof(Details), new { id = department.Id });
            }
            return View(department);
        }
        #endregion

        // GET: DepartmentController/Edit/5
        #region Edit
        public ActionResult Edit(int id)
        {
            return View(departmentRepository.Read(id));
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Department newDepartment)
        {
            departmentRepository.Update(newDepartment, id);
            return RedirectToAction(nameof(Details), new { id });
        }
        #endregion

        // GET: DepartmentController/Delete/5
        #region Delete
        public ActionResult Delete(int id)
        {
            return View(departmentRepository.Read(id));
        }

        // POST: DepartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Department department)
        {
            departmentRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
