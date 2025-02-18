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
        //public EmployeeDepartmentsViewModel PrepareEmpDeptViewModel(ApplicationUser? user = null)
        //{
        //    var departments = _context.Departments.ToList();
        //    if (user == null)
        //    {
        //        EmployeeDepartmentsViewModel viewModel = new EmployeeDepartmentsViewModel()
        //        {
        //            applicationUser = new ApplicationUser(),
        //            departments = departments,
        //        };
        //        return viewModel;
        //    }
        //    else if (user != null)
        //    {
        //        var departmentsInDb = _context.Departments.FirstOrDefault(d=> d.ID == user.DepartmentID);
        //        EmployeeDepartmentsViewModel viewModel = new EmployeeDepartmentsViewModel()
        //        {
        //            applicationUser=user,
        //            departments=departments
        //        };
        //        return viewModel;
        //    }
        //    return null;
        //}
    }
}
