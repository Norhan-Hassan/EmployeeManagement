using EmployeeManagement.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(e => e.department)
                .WithMany(d => d.applicationUsers)
                .HasForeignKey(e => e.DepartmentID)
                .OnDelete(DeleteBehavior.SetNull);  

            base.OnModelCreating(modelBuilder);
        }



    }
}
