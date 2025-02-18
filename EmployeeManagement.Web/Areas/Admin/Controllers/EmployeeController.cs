using EmployeeManagement.Entities.Models;
using EmployeeManagement.Entities.Repo_Interfaces;
using EmployeeManagement.Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeManagement.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //get current admin user for not showing him again in the table
            var UserId = unitOfWork.ApplicationUserRepo.GetCurrentUser();
            if (UserId != null)
            {
                var EmployeeWithDepartment = unitOfWork.ApplicationUserRepo.GetAll(i => i.Id != UserId, include: "department").ToList();
                return View("Index", EmployeeWithDepartment);
            }
            
            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var UserEmployee = unitOfWork.ApplicationUserRepo.GetFirstOrDefault(u => u.Id == id);
            var departments = unitOfWork.DepartmentRepo.GetAll();
            EmployeeDepartmentsViewModel viewModel = new EmployeeDepartmentsViewModel
            {
                applicationUser = UserEmployee,
                departments = departments
            };
            if(UserEmployee != null)
            {
                return View("Edit", viewModel);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditInAction(EmployeeDepartmentsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userFromRequest = unitOfWork.ApplicationUserRepo.GetFirstOrDefault(u => u.Id == viewModel.applicationUser.Id);
                if (userFromRequest == null)
                {
                    return NotFound();
                }   
                userFromRequest.UserName = viewModel.applicationUser.UserName;
                userFromRequest.Email = viewModel.applicationUser.Email;
                userFromRequest.DepartmentID= viewModel.applicationUser.DepartmentID;

                unitOfWork.ApplicationUserRepo.Update(userFromRequest);
                unitOfWork.Save();
                TempData["Updated"] = "Employee is Updated successfully";
                return RedirectToAction("Index");
            }
            viewModel.departments= unitOfWork.DepartmentRepo.GetAll();
            return View("Edit", viewModel);
        }

    }
}
