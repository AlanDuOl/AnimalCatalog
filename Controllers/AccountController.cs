using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnimalCatalogSqLite.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace AnimalCatalogSqLite.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Route("Login")]
        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(ViewUserLogin userLogin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IdentityUser user = await _userManager.FindByEmailAsync(userLogin.Email);
                    if (user != null)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user.UserName, userLogin.Password, userLogin.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "/");
                        }
                        ModelState.AddModelError("", "Invalid password!");
                    }
                    ModelState.AddModelError("", "User does not exit!");
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"Error: {err.Message}");
            }
            return View();
        }

        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(ViewUserRegister userRegister)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Check if passwords match
                    if (userRegister.Password == userRegister.PasswordConfirmation)
                    {
                        // Check if user already exists
                        var user = await _userManager.FindByEmailAsync(userRegister.Email);
                        if (user == null)
                        {
                            // Create a user and add a user role to it
                            IdentityUser newUser = new IdentityUser
                            {
                                UserName = userRegister.UserName,
                                Email = userRegister.Email
                            };
                            var result = await _userManager.CreateAsync(newUser, userRegister.Password);
                            if (result.Succeeded)
                            {
                                var createdUser = await _userManager.FindByEmailAsync(userRegister.Email);
                                var roleResult = await _userManager.AddToRoleAsync(createdUser, "User");
                                if (roleResult.Succeeded)
                                {
                                    return RedirectToAction("Login", "Account");
                                }
                                foreach (var error in roleResult.Errors)
                                {
                                    ModelState.AddModelError("", error.Description);
                                }
                            }
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                        ModelState.AddModelError("", "Email already has an account!");
                    }
                    ModelState.AddModelError("", "Passwords are different!");
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"Error: {err.Message}");
            }
            return View();
        }
    }
}