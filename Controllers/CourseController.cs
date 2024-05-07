using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Models;
using MVC.Repositories;

namespace MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CourseController : Controller
	{
		ICourseRepository _CourseRepository;
		IDepartmentRepository _departmentRepository;
		//private readonly Repository<Course> _CourseRepository;


        public CourseController(ICourseRepository courseRepository, IDepartmentRepository departmentRepository)
        {
			_CourseRepository = courseRepository;
			_departmentRepository = departmentRepository;
        }

        #region Helpers
        private void GetDepartments()
		{
			var departments = _departmentRepository.ReadAll();
			ViewBag.Departments = new SelectList(departments, "Id", "Name");
		}

		public IActionResult lessThanDegree(decimal Min_Degree, decimal Degree)
		{
			if (Min_Degree < Degree)
			{
				return Json(true);
			}
			return Json(false);
		}
		#endregion

		#region GetAll
		public IActionResult Index()
		{
			List<Course> courses = _CourseRepository.ReadAllWithDepartments();
			return View(courses);
		}
		#endregion

		#region Edit
		[HttpGet]
		public IActionResult Edit(int id)
		{
			Course? course = _CourseRepository.Read(id);
			GetDepartments();
			if (course != null)
			{
				return View(course);
			}
			else
			{
				return RedirectToAction("Index");
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken] // disable calling this method from any outsource website
		public IActionResult Edit(int id, Course newCourse)
		{
			Course course = _CourseRepository.Read(id)!;
			if (ModelState.IsValid == true)
			{
				_CourseRepository.Update(newCourse, id);
				return RedirectToAction("index");
			}
			GetDepartments();
			return View(course);
			
		}
		#endregion

		#region Add
		[HttpGet]
		public IActionResult Add()
		{
			GetDepartments();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Add(Course course) 
		{
			if(!ModelState.IsValid)
			{
				GetDepartments();
				return View();
			}
			await _CourseRepository.CreateAsync(course);
			return RedirectToAction("index");
		}
		#endregion

		#region Delete
		public IActionResult Delete(int id)
		{
			_CourseRepository.Delete(id);
			return RedirectToAction(nameof(Index));
		}
		#endregion
	}
}
