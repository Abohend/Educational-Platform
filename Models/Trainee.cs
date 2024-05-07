using MVC.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
	public class Trainee : ApplicationUser
	{
		//public int Id { get; set; }
		//public string Name { get; set; } = string.Empty;
		//public string? Img { get; set; }
		//public string? Address { get; set; }
		[Range(1, 4)]
		public int? Grade { get; set; }
		#region Relation
        public List<CourseResult> CourseResults { get; set; } = new List<CourseResult>();
		#endregion

	}
}
