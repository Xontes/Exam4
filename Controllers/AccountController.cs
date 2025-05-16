using Exam4.Models;
using Exam4.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Exam4.Controllers
{
    [Authorize(Roles ="User,Admin")]
    public class AccountController : Controller
    {
         UserManager<AppUser> _userManager;
         readonly RoleManager<IdentityRole> _roleManager;
         readonly SignInManager<AppUser> _signInManager;
        public AccountController(RoleManager<IdentityRole> roleManager,UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            AppUser appUser = new AppUser()
            {
                Name = registerVm.Name,
                Surname = registerVm.Surname,
                UserName = registerVm.Username,
                Email = registerVm.Email,
            };

            var result = await _userManager.CreateAsync(appUser,registerVm.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, "User");
                return RedirectToAction("Login");
            }

            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm,string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = 
             await _userManager.FindByEmailAsync(loginVm.EmailOrUsername)??
             await _userManager.FindByNameAsync(loginVm.EmailOrUsername);
            if (user == null)
            {
                ModelState.AddModelError("", "You don't exist");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginVm.Password,true);

            if(returnUrl != null)
            {
                return RedirectToAction(returnUrl);
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Try again");
                return View();
            }
            await _signInManager.PasswordSignInAsync(user,loginVm.Password,true,false);

            return RedirectToAction("index","home");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index");
        }
        
        public async Task<IActionResult> CreateRoles()
        {
            string[] roles = {"Admin","User","Manager"};

            foreach (var role in roles)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(role));
            }

            return View();
        }
    }
}
