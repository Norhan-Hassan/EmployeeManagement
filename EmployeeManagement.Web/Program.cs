using EmployeeManagement.DataAccess.Data;
using EmployeeManagement.DataAccess.Repo_Implementations;
using EmployeeManagement.Entities.Models;
using EmployeeManagement.Entities.Repo_Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
                )
            );
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var app = builder.Build();

            // Seed roles Creation
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await RoleSeeder.SeedRoles(services); 
            }

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "Identity",
                pattern: "{area=Identity}/{controller=Account}/{action=Register}/{id?}"
                );

            app.MapControllerRoute(
                name: "Employee",
                pattern: "{area=Employee}/{controller=Home}/{action=Details}/{id?}"
                );

            app.MapControllerRoute(
               name: "Admin",
               pattern: "{area=Admin}/{controller=Department}/{action=Index}/{id?}"
             );
            
            app.Run();
        }
    }
}
