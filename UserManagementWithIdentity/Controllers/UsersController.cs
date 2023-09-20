using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using UserManagementWithIdentity.Models;
using UserManagementWithIdentity.ViewModels;

namespace UserManagementWithIdentity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }


        public async Task<IActionResult> Index()
        {

            var users = await _userManager.Users.Select(user => new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,

                Roles = _userManager.GetRolesAsync(user).Result

            }).ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Add()
        {

            var roles = await _roleManager.Roles.Select(
                r => new RolesViewModel { RoleName = r.Name }
                ).ToListAsync();
            var viewModel = new AddUserViewModel
            {
                Roles = roles
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (!model.Roles.Any(r => r.IsSelected))
            {
                ModelState.AddModelError("Roles", "At least 1 role must be selected");
                return View(model);
            }
            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email", "Email Already in used");
                return View(model);
            }
            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                ModelState.AddModelError("UserName", "UserName Already in used");
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Roles", error.Description);
                }
                return View(model);
            }
            await _userManager.AddToRolesAsync(user, model.Roles.Where(r => r.IsSelected).Select(r => r.RoleName));

            return RedirectToAction(nameof(Index));

        }



        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            var roles = await _roleManager.Roles.ToListAsync();
            var viewModel = new UserRolesViewModel
            {
                UserId = user.Id,
                Username = user.UserName,
                Roles = roles.Select(role => new RolesViewModel
                {
                    RoleName = role.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList(),

            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in model.Roles)
            {
                if (userRoles.Any(r => r == role.RoleName) && !role.IsSelected)
                    ///remove
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                else if (!userRoles.Any(r => r == role.RoleName) && role.IsSelected)
                    ///add
                    await _userManager.AddToRoleAsync(user, role.RoleName);
            }

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            var viewModel = new UserProfileViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
                return NotFound();

            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userWithSameEmail != null && userWithSameEmail.Id != model.Id)
            {
                ModelState.AddModelError("Email", "Email Already exists");
                return View(model);

            }
            var userWithSameUsername = await _userManager.FindByNameAsync(model.UserName);
            if (userWithSameUsername != null && userWithSameUsername.Id != model.Id)
            {
                ModelState.AddModelError("Username", "Username Already exists");
                return View(model);

            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.UserName;
            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));

        }
        //[HttpGet]

        //public async Task<IActionResult> Delete(string userId)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //        return NotFound();
        //    var viewModel = new UserProfileViewModel
        //    {
        //        Id = user.Id,
        //        Email = user.Email,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        UserName = user.UserName
        //    };
        //    return View(viewModel);
        //}

        //[HttpDelete]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(UserProfileViewModel model)
        //{
        //    var user = await _userManager.FindByIdAsync(model.Id);


        //    if (user == null)
        //        return NotFound();


        //    await _userManager.DeleteAsync(user);

        //    return RedirectToAction(nameof(Index));

        //}
    }
}
