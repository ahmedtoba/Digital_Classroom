using Digital_Classroom.Models;
using Digital_Classroom.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Digital_Classroom.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(loginVM.UserLogin);
                user = (user != null) ? user : await userManager.FindByEmailAsync(loginVM.UserLogin);
                if (user != null)
                {
                    var passwordChecked = await userManager.CheckPasswordAsync(user, loginVM.Password);
                    if (passwordChecked)
                    {
                        var response = await signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                        if (response.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            TempData["Error"] = "Wrong Login. Please try again";
            return View(loginVM);
        }



        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (ModelState.IsValid)
            {
                var userByEmail = await userManager.FindByEmailAsync(registerVM.Email);
                var userByUsername = await userManager.FindByNameAsync(registerVM.UserName);
                
                if (userByEmail == null && userByUsername == null)
                {
                    var newUser = new AppUser()
                    {
                        Email = registerVM.Email,
                        UserName = registerVM.UserName,
                        FullName = registerVM.FullName
                    };
                    var response = await userManager.CreateAsync(newUser, registerVM.Password);
                    if (response.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newUser, registerVM.Role);
                    }
                    return RedirectToAction("Index", "Home");
                }
                if (userByUsername != null)
                    TempData["Error"] = "UserName Already Exists";
                if (userByEmail != null)
                    TempData["Error"] = "Email Already Exists";
                return View(registerVM);
            }
            TempData["Error"] = "Login Error, Please Try again, Ensure to enclue complex password";
            return View(registerVM);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
