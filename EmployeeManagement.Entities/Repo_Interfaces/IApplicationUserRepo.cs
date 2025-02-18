using EmployeeManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Entities.Repo_Interfaces
{
    public interface IApplicationUserRepo:IGenericRepo<ApplicationUser>
    {
        void Update(ApplicationUser applicationUser);
        string GetCurrentUser();
    }
}
