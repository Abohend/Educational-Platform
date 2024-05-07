using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Repositories
{
	public class InstructorRepository(Db context, UserManager<ApplicationUser> userManager) : IInstructorRepository
	{
		private readonly Db _context = context;
		private readonly UserManager<ApplicationUser> _userManager = userManager;


		#region Create
		public async Task CreateAsync(Instructor instructor)
		{
			
			_context.Instructors.Add(instructor);
			_context.SaveChanges();
			await _userManager.UpdateSecurityStampAsync(instructor);
			await _userManager.AddToRoleAsync(instructor, "Instructor");
			_context.SaveChanges();
		}
		#endregion

		#region Delete
		public void Delete(int id)
		{
			Instructor? instructor = Read(id);
			if (instructor != null)
			{
				_context.Instructors.Remove(instructor);
			}
			_context.SaveChanges();
		}
		#endregion

		#region Read
		public Instructor? Read(int id)
		{
			return _context.Instructors.SingleOrDefault(c => c.Id == id);
		}

		public List<Instructor> ReadAll()
		{
			return _context.Instructors.ToList();
		}

		public Instructor? ReadByEmail(string email)
		{
			return _context.Instructors.FirstOrDefault(i => i.Email == email);
		}

		public Instructor? ReadWithDepartment(int id)
		{
			return _context.Instructors.Include(i => i.Department).SingleOrDefault(s => s.Id == id);
		}
		#endregion

		#region Update
		public void Update(Instructor newInstructor, int id)
		{
			Instructor? instructor = Read(id);
			if (instructor != null)
			{
				instructor.Name = newInstructor.Name;
				instructor.Img = newInstructor.Img;
				instructor.Address = newInstructor.Address;
				instructor.Salary = newInstructor.Salary;
				instructor.CourseId = newInstructor.CourseId;
				instructor.DepartmentId = newInstructor.DepartmentId;
				_context.SaveChanges();
			}

		}
		#endregion
	}
}
