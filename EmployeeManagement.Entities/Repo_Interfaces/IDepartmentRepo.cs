using EmployeeManagement.Entities.Models;
using EmployeeManagement.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Entities.Repo_Interfaces
{
    public interface IDepartmentRepo : IGenericRepo<Department>
    {
        void Update(Department department);
    }
}
