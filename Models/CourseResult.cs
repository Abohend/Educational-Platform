namespace MVC.Models
{
	public class CourseResult
	{
		public int Id { get; set; }
		public Decimal? Degree { get; set; }

		#region Relation
		public int CourseId { get; set; }
		public required Course Course { get; set; }
		public int TraineeId { get; set; }
		public required Trainee Trainee { get; set; }
		#endregion
	
	}
}
