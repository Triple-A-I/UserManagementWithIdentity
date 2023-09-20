using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagementWithIdentity.Models;

namespace UserManagementWithIdentity.Controllers.Api
{
    [Route("api/{Controller}")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }
        [HttpDelete]
        public async Task<IActionResult> DelteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                throw new Exception();
            return Ok();


        }
    }
}
