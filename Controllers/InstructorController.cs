using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Models;
using MVC.Repositories;
using NuGet.Protocol;
using System.Security.Claims;

namespace MVC.Controllers
{
	[Authorize(Roles="Admin")]
	public class InstructorController(IInstructorRepository instructorRepository, ICourseRepository courseRepository, IDepartmentRepository departmentRepository, UserManager<ApplicationUser> userManager) : Controller
	{
		private readonly IInstructorRepository _instructorRepository = instructorRepository;
		private readonly ICourseRepository _courseRepository = courseRepository;
		private readonly IDepartmentRepository _departmentRepository = departmentRepository;
		private readonly UserManager<ApplicationUser> _userManager = userManager;

		#region Helpers
		private void GetLists()
		{
			ViewBag.Departments = _departmentRepository.GetDepartmentsSLI();
			ViewBag.Courses = _courseRepository.GetCoursesSLI();
		}
		
		private void GenerateErros(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				if (error.Code.Contains("Password"))
				{
					ModelState.AddModelError("Password", error.Description);
				}
				else if (error.Code.Contains("Email"))
				{
					ModelState.AddModelError("Email", error.Description);
				}
				else
				{
					ModelState.AddModelError("Summary", error.Description);
				}

			}
		}

		//> Instructor/GetCoursesForDepartment/id
		public IActionResult GetCoursesForDepartment(int id)
		{
			List<Course> relatedCourses = _courseRepository.GetCoursesForDepartment(id);
			return Json(relatedCourses);
		}
		#endregion

		#region get all / one
		[Authorize(Roles="Admin")]
		public IActionResult Index()
		{
			List<Instructor> instructors = _instructorRepository.ReadAll();
			return View(instructors);
		}
		public IActionResult GetOne(int id)
		{
			//if (User.IsInRole("Trainee"))
			//{
			//	id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
			//}
			Instructor? instructor = _instructorRepository.ReadWithDepartment(id);
			if (instructor == null) { return NotFound(); }
			return View(instructor);
		}
        #endregion

        #region new
        [Authorize(Roles = "Admin")]
        [HttpGet]
		public IActionResult Add()
		{
			GetLists();
			return View(); 
		}

		[HttpPost]
		public async Task<IActionResult> Add(Instructor ins)
		{
			// validating unique email
			//Instructor? duplicatedIns = _instructorRepository.ReadByEmail(ins.Email);
			//if (duplicatedIns != null)
			//{
			//	ModelState.AddModelError("Email", "Email Already Exists");
			//}
			//if (!ModelState.IsValid)
			//{
			//	GetLists();
			//	return View(ins);
			//}
			//PasswordHasher<Instructor> hasher = new();
			//ins.PasswordHash = hasher.HashPassword(ins, ins.PasswordHash!);
			//await _instructorRepository.CreateAsync(ins);
			ins.UserName = ins.Email;
			IdentityResult result = await _userManager.CreateAsync(ins, ins.PasswordHash!);
			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(ins, "Instructor");
				return RedirectToAction("index");
			}
			else
			{
				GenerateErros(result);
				GetLists();
				return View(ins);
			}
			
		}
		#endregion

		#region Edit
		[HttpGet]
		public IActionResult Edit(int id)
		{
			Instructor? ins = _instructorRepository.Read(id);
			if (ins == null) { return NotFound(); }
			GetLists();
			return View(ins);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Instructor newins)
		{
			try
			{
				_instructorRepository.Update(newins, newins.Id);
				return RedirectToAction("index");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("Summary", ex.Message);
			}
			return View(newins);
		}
		#endregion

		#region Delete
		public IActionResult Delete(int id)
		{
			_instructorRepository.Delete(id);
			return RedirectToAction("index");
		}
		#endregion
	}
}
