using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Instructor : ApplicationUser
	{
		//public int Id { get; set; }
		//public string Name { get; set; } = string.Empty;
		//public string? Img { get; set; }
		//public string? Address { get; set; }
		[Required]
		public override string? Email { get => base.Email; set => base.Email = value; }
		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public override string? PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }
		public decimal? Salary { get; set; }
		
		#region Relation
		public int CourseId {  get; set; }
		public Course? Course { get; set;}
		#endregion
	
	}
}
