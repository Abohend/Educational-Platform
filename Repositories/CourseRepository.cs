using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Models;
using NuGet.Protocol.Core.Types;

namespace MVC.Repositories
{
	public class CourseRepository : ICourseRepository
	{
		Db _context;

		public CourseRepository(Db context)
		{
			_context = context;
		}

		#region Create
		public async Task CreateAsync(Course course)
		{
			await _context.Courses.AddAsync(course);
			_context.SaveChanges();
		}
		#endregion

		#region Read
		public List<Course> ReadAll()
		{
			return _context.Courses.ToList();
		}
		public Course? Read(int id)
		{
			return _context.Courses.SingleOrDefault(c => c.Id == id);
		}
		public List<Course> ReadAllWithDepartments()
		{
			return _context.Courses.Include(s => s.Department).ToList();
		}
		public Course? GetCourseWithSameNameAndDepartment(Course course)
		{
			return _context.Courses.Where(c => c.DepartmentId == course.DepartmentId && c.Name == course.Name && c.Id != course.Id).FirstOrDefault();
		}

		public IEnumerable<SelectListItem> GetCoursesSLI()
		{
			return _context.Courses.Select(d => new SelectListItem(d.Name, d.Id.ToString()));
		}

		public List<Course> GetCoursesForDepartment(int? id)
		{
			return _context.Courses
						   .Where(c => c.DepartmentId == id)
						   .ToList();
		}


		#endregion

		#region Update
		public void Update(Course newCourse, int id)
		{
			Course? course = Read(id);
			if (course != null)
			{
				course.Name = newCourse.Name;
				course.Min_Degree = newCourse.Min_Degree;
				course.Degree = newCourse.Degree;
				course.DepartmentId = newCourse.DepartmentId;
				_context.SaveChanges();
			}

		}
		#endregion

		#region Delete
		public void Delete(int id)
		{
			Course? course = Read(id);
			if (course != null)
			{
				_context.Courses.Remove(course);
			}
			_context.SaveChanges();
		}
		#endregion
	}

	//public class CourseRepository : Repository<Course>
	//{
	//       public CourseRepository(Db context): base(context){}
	//	public List<Course> ReadAllWithDepartments()
	//	{
	//		return _context.Courses.Include(s => s.Department).ToList();
	//	}
	//	public Course? GetCourseWithSameNameAndDepartment(Course course)
	//	{
	//		return _context.Courses.Where(c => c.DepartmentId == course.DepartmentId && c.Name == course.Name && c.Id != course.Id).FirstOrDefault();
	//	}
	//}
}
