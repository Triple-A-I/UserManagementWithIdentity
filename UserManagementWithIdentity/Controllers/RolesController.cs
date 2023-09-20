using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementWithIdentity.ViewModels;

namespace UserManagementWithIdentity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RoleFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", await _roleManager.Roles.ToListAsync());
            var roleIsExist = await _roleManager.RoleExistsAsync(model.Name);
            if (roleIsExist)
            {
                ModelState.AddModelError("Name", "Role Already exists");
                return View("Index", await _roleManager.Roles.ToListAsync());

            }

            await _roleManager.CreateAsync(new IdentityRole(model.Name.Trim()));
            return RedirectToAction(nameof(Index));

        }
    }
}
