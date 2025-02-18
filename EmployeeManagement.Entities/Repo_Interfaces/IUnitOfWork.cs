using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Entities.Repo_Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IApplicationUserRepo ApplicationUserRepo { get; }
        public IDepartmentRepo DepartmentRepo { get; }

        int Save();
    }
}
