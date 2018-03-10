using Dbh.ServiceLayer.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MVCWebClient.Models.AccountViewModels;
using MVCWebClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebClient.Controllers
{
    public class UsersController : Controller
    {
        private IQuestionServices _questionService;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly MapperService _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(IQuestionServices service,
            ApplicationUserManager userManager,
           ApplicationSignInManager signInManager,
           MapperService mapper, RoleManager<IdentityRole> roleManager)
        {
            _questionService = service;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new ChangeUsersViewModel();

            var users = _userManager.Users.OrderBy(x=>x.UserName).ToList();
            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var userRole = userRoles.FirstOrDefault();
                userRole = userRole != null ? userRole : "User";

                model.Users.Add(new ChangeUserRoleViewModel
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    CurrentRole = userRole,
                    IsUser = userRole == "User",
                    IsModerator = userRole == "Moderator",
                    IsAdmin = userRole == "Admin",
                });
            }
            //var roles = await _userManager.GetRolesAsync(users[1]);
            var roles = _roleManager.Roles.Select(r=>r.Name).ToList();
            //var theusers = _roleManager.Roles.Where(r=>r.Name=="Admin").Select(r => r.Users);
            model.Roles = roles;
            //_roleManager.
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MakeUser(string emailAddress, string currentRole)
        {
            var result = ChangeRole(emailAddress, currentRole, "User");
            //TODO: add toast

            return RedirectToAction("Index", "Users");
        }

        [HttpPost]
        public async Task<IActionResult> MakeModerator(string emailAddress, string currentRole)
        {
            var result = ChangeRole(emailAddress, currentRole, "Moderator");
            //TODO: add toast

            return RedirectToAction("Index", "Users");
        }

        [HttpPost]
        public async Task<IActionResult> MakeAdmin(string emailAddress, string currentRole)
        {
            var result = ChangeRole(emailAddress, currentRole, "Admin");
            //TODO: add toast

            return RedirectToAction("Index", "Users");
        }

        private bool ChangeRole(string email, string currentRole, string targetRole)
        {
            var userToChange = _userManager.Users.FirstOrDefault(x => x.Email == email);
            var result = _userManager.RemoveFromRoleAsync(userToChange, currentRole);
            if (!result.Result.Succeeded) { 
                return false;
            }

            var result2 = _userManager.AddToRoleAsync(userToChange, targetRole);
            if (result2.Result.Succeeded)
            {
                return false;
            }
            return true;
        }
    }
}
