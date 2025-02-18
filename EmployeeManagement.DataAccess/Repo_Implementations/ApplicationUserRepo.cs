using EmployeeManagement.DataAccess.Data;
using EmployeeManagement.Entities.Models;
using EmployeeManagement.Entities.Repo_Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace EmployeeManagement.DataAccess.Repo_Implementations
{
    public class ApplicationUserRepo : GenericRepo<ApplicationUser>, IApplicationUserRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationUserRepo(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        //get the id of the current logged user 
        public string GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext.User;

            var claims = user.Identity as ClaimsIdentity;

            if (!user.Identity.IsAuthenticated || claims == null)
            {
                return null;
            }

            string User_ID = claims.FindFirst(ClaimTypes.NameIdentifier).Value;

            return User_ID;
        }

        public void Update(ApplicationUser applicationUser)
        {
            if (applicationUser != null)
            {
                _context.ApplicationUsers.Update(applicationUser);
            }

        }
    }
}
