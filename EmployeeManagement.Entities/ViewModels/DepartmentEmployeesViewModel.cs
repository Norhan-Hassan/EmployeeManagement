using EmployeeManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Entities.ViewModels
{
    public class DepartmentEmployeesViewModel
    {
        public Department department { get; set; }
        public IEnumerable<ApplicationUser> applicationUsers { get; set; }
    }
}
