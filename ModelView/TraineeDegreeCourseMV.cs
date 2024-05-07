namespace MVC.ModelView
{
    public class TraineeDegreeCourseMV
    {
        public string TraineeName { get; set; } = string.Empty;
        public Dictionary<string, (Decimal?, bool)> TraineePoints { get; set;} = new ();
    }
}
