using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Repositories
{
	public class DepartmentRepository : IDepartmentRepository
	{
		readonly Db _context;

        public DepartmentRepository(Db context)
        {
			_context = context;
        }

        #region Create
        public async Task CreateAsync(Department Department)
		{
			await _context.Departments.AddAsync(Department);
			_context.SaveChanges();
		}
		#endregion

		#region Read
		public List<Department> ReadAll()
		{
			return _context.Departments.ToList();
		}
		public Department? Read(int id)
		{
			return _context.Departments.SingleOrDefault(c => c.Id == id);
		}
		public IEnumerable<SelectListItem> GetDepartmentsSLI()
		{
			return _context.Departments.Select(d => new SelectListItem(d.Name, d.Id.ToString()));
		}
        public Department? ReadWithCourses(int id)
		{
			return _context.Departments.Include("Courses").SingleOrDefault(d => d.Id == id);
		}
        #endregion

        #region Update
        public void Update(Department newDepartment, int id)
		{
			Department? department = Read(id);
            if (department != null)
            {
				department.Name = newDepartment.Name;
				department.Manager = newDepartment.Manager;
				_context.SaveChanges();
			}
		}
		#endregion

		#region Delete
		public void Delete(int id)
		{
			Department? Department = Read(id);
			if (Department != null)
			{
				_context.Departments.Remove(Department);
			}
			_context.SaveChanges();
		}

		#endregion
	}
}
