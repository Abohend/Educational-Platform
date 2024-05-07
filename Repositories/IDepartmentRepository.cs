using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Repositories
{
	public interface IDepartmentRepository : IRepository<Department>
	{
		public IEnumerable<SelectListItem> GetDepartmentsSLI();
		public Department? ReadWithCourses(int id);
	}
}
