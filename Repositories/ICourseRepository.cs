using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Repositories
{
	public interface ICourseRepository: IRepository<Course>
	{
		public List<Course> ReadAllWithDepartments();
		public Course? GetCourseWithSameNameAndDepartment(Course course);
		public IEnumerable<SelectListItem> GetCoursesSLI();
		public List<Course> GetCoursesForDepartment(int? id);
	}
}
