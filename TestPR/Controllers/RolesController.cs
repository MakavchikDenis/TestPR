using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestPR.Models;

namespace TestPR.Controllers
{

    [Authorize(Roles = "Администратор")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdentityRole>>> Get()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] IdentityRole role)
        {
            return (IActionResult)await _roleManager.CreateAsync(role);               
          
        }



    }
}
