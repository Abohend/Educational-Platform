using Microsoft.AspNetCore.Identity;

namespace MVC.Models
{
    public class ApplicationUser: IdentityUser<int>
    {
        public string? Name { get; set; }
        public string? Img { get; set; }
        public string? Address { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
