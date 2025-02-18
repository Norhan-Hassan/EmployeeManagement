using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Entities.Models
{
    public class ApplicationUser:IdentityUser
    {
       
        [ForeignKey("department")]
        [DisplayName("Department")]
        public int ? DepartmentID { get; set; }  // for employee only so , it will be nullable
        [ValidateNever]
        public Department department { get; set; }
    }
}
