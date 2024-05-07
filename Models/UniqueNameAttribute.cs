using MVC.Repositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
	internal class UniqueNameAttribute : ValidationAttribute
	{
		//ICourseRepository courseRepository;
   //     public UniqueNameAttribute(ICourseRepository courseRepository)
   //     {
			//this.courseRepository = courseRepository;
   //     }
        // Check Unique Name attribute for Class Course only
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			string name = (string)value!;
			Course course = (Course)validationContext.ObjectInstance;

			Db _context = new();
			Course? replicaCourse = _context.Courses.Where(c => c.DepartmentId == course.DepartmentId && c.Name == name && c.Id != course.Id).FirstOrDefault();
			//Course? replicaCourse = courseRepository.GetCourseWithSameNameAndDepartment(course);
			
			if (replicaCourse == null)
			{
				return ValidationResult.Success;
			}
			else
			{
				return new ValidationResult("Course with the same name already exists in this department");
			}
 		}
	}
}