using Dbh.ServiceLayer.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MVCWebClient.Models.AccountViewModels;
using MVCWebClient.Services;
using NToastNotify;
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
        private readonly IToastNotification _toaster;

        public UsersController(IQuestionServices service,
            ApplicationUserManager userManager,
           ApplicationSignInManager signInManager,
           MapperService mapper, RoleManager<IdentityRole> roleManager,
           IToastNotification toaster)
        {
            _questionService = service;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _toaster = toaster;
        }

        [Authorize(Policy = "RequireAtLeastAdminRole")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new ChangeUsersViewModel();

            var users = _userManager.Users.OrderBy(x=>x.UserName).ToList();
            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var userRole = userRoles.FirstOrDefault();
                var userVM = new ChangeUserRoleViewModel
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    CurrentRole = userRole,
                    IsUser = userRole == "User",
                    IsModerator = userRole == "Moderator",
                    IsAdmin = userRole == "Admin",
                };
                switch (userRole)
                {
                    case "Admin":
                        {
                            model.Administrators.Add(userVM);
                            break;
                        }
                    case "Moderator":
                        {
                            model.Moderators.Add(userVM);
                            break;
                        }
                    default:
                        {
                            model.Users.Add(userVM);
                            break;
                        }
                }
            }
            
            var roles = _roleManager.Roles.Select(r=>r.Name).ToList();
            //var theusers = _roleManager.Roles.Where(r=>r.Name=="Admin").Select(r => r.Users);
            model.Roles = roles;
            //_roleManager.
            return View(model);
        }

        [Authorize(Policy = "RequireAtLeastAdminRole")]
        [HttpPost]
        public async Task<IActionResult> MakeUser(string userName, string emailAddress, string currentRole)
        {
            var result = ChangeRole(emailAddress, currentRole, "User");
            if (result)
            {
                _toaster.AddToastMessage("'"+ userName+"'" +" role changed to 'User'", "", Enums.ToastType.Success);
            }
            else
            {
                _toaster.AddToastMessage("Role change did not succeeded!", "", Enums.ToastType.Error);
            }

            return RedirectToAction("Index", "Users");
        }

        [Authorize(Policy = "RequireAtLeastAdminRole")]
        [HttpPost]
        public async Task<IActionResult> MakeModerator(string userName, string emailAddress, string currentRole)
        {
            var result = ChangeRole(emailAddress, currentRole, "Moderator");
            if (result)
            {
                _toaster.AddToastMessage("'" + userName + "'" + " role changed to 'Moderator'", "", Enums.ToastType.Success);
            }
            else
            {
                _toaster.AddToastMessage("Role change did not succeeded!", "", Enums.ToastType.Error);
            }

            return RedirectToAction("Index", "Users");
        }

        [Authorize(Policy = "RequireAtLeastAdminRole")]
        [HttpPost]
        public async Task<IActionResult> MakeAdmin(string userName, string emailAddress, string currentRole)
        {
            var result = ChangeRole(emailAddress, currentRole, "Admin");
            if (result)
            {
                _toaster.AddToastMessage("'" + userName + "'" + " role changed to 'Administrator'", "", Enums.ToastType.Success);
            }
            else
            {
                _toaster.AddToastMessage("Role change did not succeeded!", "", Enums.ToastType.Error);
            }

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
            if (!result2.Result.Succeeded)
            {
                return false;
            }
            return true;
        }
    }
}
