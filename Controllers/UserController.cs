using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AnimalCatalogSqLite.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AnimalCatalogSqLite.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace AnimalCatalogSqLite.Controllers
{
    [Route("User")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Route("Index")]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {   
            List<ViewUserList> users = _userRepository.GetUsers();
            return View(users);
        }

        [Route("Edit")]
        [Authorize]
        public IActionResult Edit()
        {
            IdentityUser user = new IdentityUser();
            if (User != null)
            {
                _userRepository.GetUser(User);
            }
            return View(user);
        }

        [HttpPost("Edit")]
        [Authorize]
        public IActionResult Edit(ViewUserList user)
        {
            bool updated = _userRepository.UpdateCommonUserData(user);
            if (updated != true)
            {
                return RedirectToAction("Edit");
            }
            return RedirectToAction("Index", "Home");
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [HttpPost("UpdateRoles")]
        public IActionResult UpdateRoles(ViewUserRoles user)
        {
            if (ModelState.IsValid)
            {
                user.Email = Request.Form["Email"];
                user.Admin = Request.Form["Admin"].Contains("true");
                user.Manager = Request.Form["Manager"].Contains("true");
                user.User = Request.Form["User"].Contains("true");

                bool roleAdded = _userRepository.UpdateUserRoles(user);
                if (roleAdded == true)
                {
                    Debug.WriteLine("New role added!");
                    return RedirectToAction("Index", "User");
                }
            }
            return View("EditRoles");
        }

        [HttpGet("EditRoles/{email}")]
        [Authorize(Roles = "Admin")]
        public IActionResult EditRoles(string email)
        {
            IdentityUser user = _userRepository.GetUserByEmail(email);
            List<string> userRoles = _userRepository.GetRolesByUser(user);
            ViewUserRoles userWithRoles = new ViewUserRoles()
            {
                Name = user.UserName,
                Email = user.Email,
                User = userRoles.Contains("User"),
                Admin = userRoles.Contains("Admin"),
                Manager = userRoles.Contains("Manager")
            };
            return View(userWithRoles);
        }

        [HttpGet("Remove/{email}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Remove(string email)
        {
            IdentityUser user = _userRepository.GetUserByEmail(email);
            return View("Remove", user);
        }

        [HttpPost("RemoveUser")]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveUser(string email)
        {
            bool userRemoved = _userRepository.RemoveUser(email);
            if (userRemoved)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Unable to remove user!");
            return RedirectToAction("Remove");
        }
    }
}