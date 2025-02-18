using EmployeeManagement.Entities.Repo_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles ="User")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //get the details for the employee but he hasn't had department yet
        //the admin is responsible for assigning department to each user in his own action edit
        [HttpGet]
        public IActionResult Details()
        {
            if (unitOfWork.ApplicationUserRepo.GetCurrentUser() != null)
            {
                string userId = unitOfWork.ApplicationUserRepo.GetCurrentUser();
                var UserDetails = unitOfWork.ApplicationUserRepo.GetFirstOrDefault(u => u.Id == userId, include: "department");

                return View("Details",UserDetails);
            }
            else
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            
               
        }
    }
}
