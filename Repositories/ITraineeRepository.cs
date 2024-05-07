using MVC.Models;

namespace MVC.Repositories
{
	public interface ITraineeRepository : IRepository<Trainee>
	{
		public Trainee? ReadTraineeWithResults(int id);
		public List<Trainee>? ReadAllWithDepartment();
		public List<Trainee>? ReadAllWithDepartment(int id);
		
	}
}
