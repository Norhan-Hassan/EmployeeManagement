using EmployeeManagement.Entities.Models;
using EmployeeManagement.Entities.Repo_Interfaces;
using EmployeeManagement.Entities.ViewModels;
using EmployeeManagement.Web.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace EmployeeManagement.Web.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUnitOfWork unitOfWork;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();

                user.UserName = userViewModel.Name;
                user.Email = userViewModel.Email;
                
                //pass the password to enable it being hashed 
                IdentityResult result = await userManager.CreateAsync(user, userViewModel.Password);
                if (result.Succeeded)
                {
                    //we can change the role from here to become admin role 
                    //in folder utilities 
                    await userManager.AddToRoleAsync(user, Role.UserRole); 
                    return RedirectToAction("Login", "Account");

                }
                //show the errors in each span for each input field 
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View("Register", userViewModel);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel loginUser)
        {
            if (ModelState.IsValid)
            {
                //check first if the user exist by name
                var user = await userManager.FindByNameAsync(loginUser.UserName);

                if (user != null)
                {
                    //then check on password if wrong or pass
                    var finedUser = await userManager.CheckPasswordAsync(user, loginUser.Password);
                    if (finedUser == true)
                    { 
                        await signInManager.SignInAsync(user, loginUser.RememberMe);

                        //redirect users according to roles

                        if (await userManager.IsInRoleAsync(user, Role.AdminRole))
                        {
                            return RedirectToAction("Index", "Department", new {area="Admin"});
                        }
                        else if (await userManager.IsInRoleAsync(user, Role.UserRole))
                        {
                            return RedirectToAction("Details", "Home", new { area = "Employee" });
                        }
                        
                    }
                }

                ModelState.AddModelError("", "UserName or Password is wrong");
            }
            return View("Login", loginUser);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

       
    }
}
