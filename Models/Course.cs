using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MVC.Repositories;
using NuGet.Protocol.Core.Types;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
	public class Course
	{
		public int Id { get; set; }
		[Required]
		[MinLength(2)]
		[MaxLength(20)]
		[UniqueName]
		public string Name { get; set; } = string.Empty;
		[Required]
		[Range(50, 100)]
		public Decimal Degree { get; set; }
		[Required]
		[Remote("lessThanDegree", "Course", 
			AdditionalFields = "Degree", 
			ErrorMessage = "Min Degree must be less than Degree")]
		public decimal Min_Degree { get; set;}

		#region Relation
		[DisplayName("Department Name")]
		public int DepartmentId { get; set; }
		public Department? Department { get; set; }
		public List<Instructor> Instructors { get; set; } = new List<Instructor>();
		public List<CourseResult> CourseResults { get; set; } = new List<CourseResult>();

		#endregion

	}
}
