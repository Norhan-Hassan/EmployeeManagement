using EmployeeManagement.DataAccess.Data;
using EmployeeManagement.Entities.Repo_Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Repo_Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        public IApplicationUserRepo ApplicationUserRepo { get; private set; }
        public IDepartmentRepo DepartmentRepo { get; private set; }

        public UnitOfWork(ApplicationDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.ApplicationUserRepo = new ApplicationUserRepo(context, httpContextAccessor);
            this.DepartmentRepo = new DepartmentRepo(context);

        }
        public void Dispose()
        {
            context.Dispose();
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
