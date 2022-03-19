using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WishList.Models;
using WishList.Models.AccountViewModels;

namespace WishList.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid==false)
            {
                return View("Register", model);
            }

            var appUser = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            var result = _userManager.CreateAsync(appUser, model.Password).Result;

            if (result.Succeeded==false)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("Password", err.Description);
                }
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
