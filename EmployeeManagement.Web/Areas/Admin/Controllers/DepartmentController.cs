using EmployeeManagement.DataAccess.Repo_Implementations;
using EmployeeManagement.Entities.Models;
using EmployeeManagement.Entities.Repo_Interfaces;
using EmployeeManagement.Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public DepartmentController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var Departments = unitOfWork.DepartmentRepo.GetAll();
            if (Departments == null)
            {
                NotFound();
            }
            return View("Index", Departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.DepartmentRepo.Add(department);
                unitOfWork.Save();
                //for toaster notification (in _Confirmations.cshtml view)
                TempData["Created"] = "Department is created successfully";
                return RedirectToAction("Index");
            }
            return View("Create", department);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var department = unitOfWork.DepartmentRepo.GetFirstOrDefault(d => d.ID == id);
            return View("Edit", department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditInAction(Department department)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.DepartmentRepo.Update(department);
                unitOfWork.Save();
                TempData["Updated"] = "Department is Updated successfully";
                return RedirectToAction("Index");
            }
            return View("Edit", department);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var department = unitOfWork.DepartmentRepo.GetFirstOrDefault(d => d.ID == id);
            return View("Delete", department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteInAction(Department department)
        {
            unitOfWork.DepartmentRepo.Remove(department);
            unitOfWork.Save();
            TempData["Deleted"] = "Department is Deleted successfully";
            return RedirectToAction("Index");
        }

        //showing the employees in this department by passing dept id
        [HttpGet]
        public IActionResult Details(int id)
        {
            var department = unitOfWork.DepartmentRepo.GetFirstOrDefault(d=>d.ID==id);
            var applicationUsers= unitOfWork.ApplicationUserRepo.GetAll(u=>u.DepartmentID==department.ID);
            var departmentEmployeesVM = new DepartmentEmployeesViewModel()
            {
                applicationUsers = applicationUsers,
                department=department,
            };
            return View("Details", departmentEmployeesVM);
        }
    }
}
