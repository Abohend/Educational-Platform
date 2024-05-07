using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC.Models;
using MVC.Repositories;

namespace MVC
{
    public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Services
			//! Notes
			//! There are 3 types of services:
			/* 1. Built in already loaded in the IOC container. ex IConfiguration
			   2. Built in needed to be loaded in the IOC container. ex AddSession
			   3. Custom services ex AddScoped */
			//! There are 3 ways to load a service:
			/* 1. AddScoped(): create a object for each request.
			   2. AddTrainsiant(): create an object for each injection.
			   3. AddSingelton(): create an object for different requests and clients.*/

			// Add services to the container.
			builder.Services.AddDbContext<Db>(optionBuiler =>
				{
					optionBuiler.UseSqlServer(builder.Configuration.GetConnectionString("DB"));
				}
			);
			builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
				{
					options.Password.RequireNonAlphanumeric = false;
					options.User.RequireUniqueEmail = true;
				})
				.AddUserManager<UserManager<ApplicationUser>>()
				//.AddUserManager<UserManager<Instructor>>()
				.AddEntityFrameworkStores<Db>();
			builder.Services.AddControllersWithViews();
			builder.Services.AddSession(); // default time is 20min from unuse
			builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
			builder.Services.AddScoped<ICourseRepository, CourseRepository>();
			builder.Services.AddScoped<ITraineeRepository, TraineeRepository>();
			builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
			builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
			#endregion
			
			var app = builder.Build();

            #region Seed Roles & Default Admin
            using (var scope = app.Services.CreateScope())
            {
                var roleManager =
                    scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

                string[] roles = { "Admin", "Trainee", "Instructor" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole<int>(role));
                    }
                }

                var userManager =
                    scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var configuration =
                    scope.ServiceProvider.GetRequiredService<IConfiguration>();

				string email = configuration["Admin:email"]!;
				string password = configuration["Admin:password"]!;

				if (await userManager.FindByEmailAsync(email) == null)
                {
                    ApplicationUser user = new();
                    user.Email = email;
                    user.UserName = email;
                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

			using (var scope = app.Services.CreateScope())
			{
                
            }
            #endregion

            #region custom middelwares
            //app.Use(async (httpContext, next) =>
            //{
            //	await httpContext.Response.WriteAsync("1) MW 1\n");
            //	await next.Invoke();
            //             await httpContext.Response.WriteAsync("3) MW 1\n");

            //         });
            //app.Run(async (httpContext) =>
            //{
            //	await httpContext.Response.WriteAsync("2) teraminate\n");
            //});
            #endregion

            #region middlewares
            // use => execute and pass to next, execute request and terminate, map => map url to execute

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
				{
					app.UseExceptionHandler("/Home/Error");
					// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
					app.UseHsts();
				}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseSession();

			app.UseAuthentication();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Account}/{action=Login}/{id?}");
            #endregion
            
			

            app.Run();
		}
	}
}
