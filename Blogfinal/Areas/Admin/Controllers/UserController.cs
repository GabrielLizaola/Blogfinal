﻿using AspNetCoreHero.ToastNotification.Abstractions;
using Domain;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogfinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly INotyfService _notification;
        public UserController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,INotyfService notyfService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _notification = notyfService;
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            if (!HttpContext.User.Identity!.IsAuthenticated) 
            {
                return View(new LoginVM());
            }
            return RedirectToAction("Index", "Post", new { area = "Admin" });
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if(!ModelState.IsValid) 
            {
                return View(vm);
            }
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == vm.Username);
            if (existingUser == null) 
            {
                _notification.Error("Username does not exist");
                return View(vm);
            }
            var verifyPassword = await _userManager.CheckPasswordAsync(existingUser, vm.Password);
            if(!verifyPassword) 
            {
                _notification.Error("Password does not match");
                return View(vm);
            }
            await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, vm.RememberMe, true);
            _notification.Success("Login Successful");
            return RedirectToAction("Index","Post",new {area="Admin"});
        }
        [HttpPost]
        [Authorize]
        public IActionResult Logout() 
        {
            _signInManager.SignOutAsync();
            _notification.Success("You are logged out successfully");
            return RedirectToAction("Index", "Home", new {area=""});
        }
    }
}
