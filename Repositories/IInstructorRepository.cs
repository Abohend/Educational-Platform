using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Models;

namespace MVC.Repositories
{
	public interface IInstructorRepository: IRepository<Instructor>
	{
		public Instructor? ReadByEmail(string email);
		public Instructor? ReadWithDepartment(int id);
	}
}
