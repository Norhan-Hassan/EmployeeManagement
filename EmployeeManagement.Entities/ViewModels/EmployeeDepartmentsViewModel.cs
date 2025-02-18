using EmployeeManagement.Entities.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Entities.ViewModels
{
    public class EmployeeDepartmentsViewModel
    {
        public ApplicationUser applicationUser { get; set; }

        [ValidateNever]
        public IEnumerable<Department> departments { get; set; }
    }
}
