using MVC.Models;

namespace MVC.Repositories
{
	public interface IRepository <T>
	{
		public Task CreateAsync(T c);
		public List<T> ReadAll();
		public T? Read(int id);
		public void Update(T newObject, int id);
		public void Delete(int id);
	}
}
