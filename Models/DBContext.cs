using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MVC.Models
{
    public class Db : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public Db() : base() { }
        public Db(DbContextOptions optionBuilder):base(optionBuilder) { }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseResult> CoursesResults { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=MVC;Trusted_Connection=True;Encrypt=False");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
		
		public override DbSet<IdentityUserRole<int>> UserRoles { get => base.UserRoles; set => base.UserRoles = value; }
    }
}
