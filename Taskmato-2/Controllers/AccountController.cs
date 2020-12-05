using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Taskmato_2.DTOs;
using Taskmato_2.Data.Services;
using Taskmato_2.Models;

namespace Taskmato_2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserService _userService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LoginConfirmed(LoginDTO loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, true, lockoutOnFailure: false);

            if(result.Succeeded)
            {
                return RedirectToAction("Index", "TaskList");
            }

            TempData["error"] = "Wrong username or password";

            return RedirectToAction(nameof(Login));
        }

        public async Task<ActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RegisterConfirmed(RegisterDTO registerDto)
        {
            var User = new IdentityUser { UserName = registerDto.Username, Email = registerDto.Email };

            try
            {
                await _userManager.CreateAsync(User, registerDto.Password);
                var newUser = new User
                {
                    Username = registerDto.Username,
                    Email = registerDto.Email
                };

                _userService.AddUser(newUser);
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;

                return RedirectToAction(nameof(Register));
            }

            var result = await _signInManager.PasswordSignInAsync(registerDto.Username, registerDto.Password, true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "TaskList");
            }

            TempData["error"] = "Something went wrong.";

            return RedirectToAction(nameof(Register));
        }
    }
}
