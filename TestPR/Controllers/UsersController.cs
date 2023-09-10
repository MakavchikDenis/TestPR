using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TestPR.Models;

namespace TestPR.Controllers
{
    [Authorize(Roles = "Администратор")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;
        public UsersController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        /// <summary>
        /// List users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await _userManager.Users.ToListAsync();
        }

        /// <summary>
        /// New user
        /// </summary>
        /// <param name="user">User form</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IdentityResult> NewUser(string email, string password)
        {
            User us = new User { Email = email, UserName = email };          
            return await _userManager.CreateAsync(us, password);

        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="iduser">User Id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IdentityResult> DelUser(string iduser)
        {
            User user = await _userManager.FindByIdAsync(iduser);
            return await _userManager.DeleteAsync(user);
        }


        /// <summary>
        /// Change Password user
        /// </summary>
        /// <param name="iduser">UserId</param>
        /// <param name="oldpassword"></param>
        /// <param name="newpassword"></param>
        /// <returns></returns>
        [HttpPut("changepas")]
        public async Task<IdentityResult> ChangePas(string iduser, string oldpassword, string newpassword)
        {
            User user = await _userManager.FindByIdAsync(iduser);
            return await _userManager.ChangePasswordAsync(user, oldpassword, newpassword);
        }

        /// <summary>
        /// Block User
        /// </summary>
        /// <param name="iduser"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        [HttpPut("blockuser")]
        public async Task<User> BlockUser(string iduser)
        {
            User user = await _userManager.FindByIdAsync(iduser);
            user.LockoutEnabled = true;
            return user;
        }


        /// <summary>
        /// Set role
        /// </summary>
        /// <param name="iduser">User id</param>
        /// <param name="roleMame">Role Name</param>
        /// <returns></returns>
        [HttpPost("setrole")]
        public async Task<IActionResult> SetRole(string iduser, string roleName)
        {
            User user = await _userManager.FindByIdAsync(iduser);
            return (IActionResult)await _userManager.AddToRoleAsync(user, roleName);
        }
    }
}
