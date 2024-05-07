using Microsoft.EntityFrameworkCore;

namespace MVC.ModelView
{
    public class UserMV
    {
        // grade address email phonenumber name
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        #region Trainee related
        public int? Grade { get; set; }
        #endregion
        
        #region Instructor related
        public decimal? Salary { get; set; }
        #endregion
    
    }
}
