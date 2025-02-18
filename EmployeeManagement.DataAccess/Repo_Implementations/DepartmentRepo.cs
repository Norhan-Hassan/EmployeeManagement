using EmployeeManagement.DataAccess.Data;
using EmployeeManagement.Entities.Models;
using EmployeeManagement.Entities.Repo_Interfaces;
using EmployeeManagement.Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Repo_Implementations
{
    public class DepartmentRepo : GenericRepo<Department>, IDepartmentRepo
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Department department)
        {
            if (department != null)
            {
                _context.Departments.Update(department);
            }
           
        }
        
    }
}
