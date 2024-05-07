using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Repositories
{
	public class TraineeRepository(Db context, UserManager<ApplicationUser> userManager) : ITraineeRepository
	{
		private readonly Db _context = context;
		private readonly UserManager<ApplicationUser> _userManager = userManager;
		
		#region Create
		public async Task CreateAsync(Trainee trainee)
		{
			_context.Trainees.Add(trainee);
			await _userManager.AddToRoleAsync(trainee, "Trainee");
			_context.SaveChanges();
		}
		#endregion

		#region Delete
		public void Delete(int id)
		{
			Trainee? trainee = Read(id);
			if (trainee != null)
			{
				_context.Trainees.Remove(trainee);
			}
			_context.SaveChanges();
		}
		#endregion

		#region Read
		public Trainee? Read(int id)
		{
			return _context.Trainees.SingleOrDefault(c => c.Id == id);
		}

		public List<Trainee> ReadAll()
		{
			return _context.Trainees.ToList();
		}
		public Trainee? ReadTraineeWithResults(int id)
		{
			return _context.Trainees.Include(t => t.CourseResults).SingleOrDefault(t => t.Id == id);
		}
		public List<Trainee>? ReadAllWithDepartment()
		{
			return _context.Trainees.Include(t => t.Department).ToList();
		}
		public List<Trainee>? ReadAllWithDepartment(int id)
		{
			return _context.Trainees.Include(t => t.Department).Where(i => i.DepartmentId == id).ToList();
		}

		#endregion

		#region Update
		public void Update(Trainee newTrainee, int id)
		{
			Trainee? trainee = Read(id);
			if (trainee != null)
			{
				trainee.Name = newTrainee.Name;
				trainee.Img = newTrainee.Img;
				trainee.Address = newTrainee.Address;
				trainee.Grade = newTrainee.Grade;
				trainee.PhoneNumber = newTrainee.PhoneNumber;
				if (newTrainee.DepartmentId != null)
					trainee.DepartmentId = newTrainee.DepartmentId;
				_context.SaveChanges();
			}

		}
		#endregion
	}
}
