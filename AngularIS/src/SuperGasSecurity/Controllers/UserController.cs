using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperGasSecurity.Data;
using SuperGasSecurity.Models;
using System.Security.Claims;
using System.Text.RegularExpressions;
using static Duende.IdentityServer.IdentityServerConstants;

namespace SuperGasSecurity.Controllers
{
    [ApiController]
    [Route("user")]
    [Authorize(LocalApi.PolicyName)]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<GetUserBasicDataResponse> Get(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var roles = await _userManager.GetRolesAsync(user);

            return new GetUserBasicDataResponse()
            {
                Email = user.Email,
                Role = roles.FirstOrDefault(),
                UserName = user.UserName,
            };
        }

        [HttpGet("roles")]
        public async Task<List<IdentityRole>> GetRoles()
        {
            return _roleManager.Roles.ToList();
        }

        [HttpGet]
        public async Task<List<UserModel>> Get()
        {
            var users = _userManager.Users.ToList();

            var userModels = new List<UserModel>();

            foreach (var user in users)
            {
                var currentUser = user.Adapt<UserModel>();
                var roles = await _userManager.GetRolesAsync(user);
                currentUser.Role = roles.FirstOrDefault();
                userModels.Add(currentUser);
            }
            return userModels;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(string id, [FromBody] UpdateUser request)
        {
            var user = await _userManager.FindByIdAsync(id);

            var roles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRoleAsync(user, roles.FirstOrDefault());
            await _userManager.AddToRoleAsync(user, request.Role);
            var test = await _userManager.UpdateAsync(user);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddUser request)
        {

            var user = request.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
                result = await _userManager.AddToRoleAsync(user, request.Role);
            else
                return BadRequest(result.Errors);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);

            return Ok();
        }
    }
}