using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpecializedClinicAuth.Data;
using SpecializedClinicAuth.Models;
using System.Security.Claims;
using System.Text.RegularExpressions;
using static Duende.IdentityServer.IdentityServerConstants;

namespace SpecializedClinicAuth.Controllers
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
        public async Task<GetUserBasicDataResponse> Get(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            var roles = await _userManager.GetRolesAsync(user);

            return new GetUserBasicDataResponse()
            {
                Email = user.Email,
                Role = roles.FirstOrDefault(),
                UserName = user.UserName,
            };
        }

        [HttpGet]
        public async Task<List<UserModel>> Get()
        {
            var users = _userManager.Users.ToList();

            var userModels = new List<UserModel>();

            users.ForEach(async user =>
            {
                var currentUser = user.Adapt<UserModel>();
                var roles = await _userManager.GetRolesAsync(user);
                currentUser.Role = roles.FirstOrDefault();
                userModels.Add(currentUser);
            });

            return userModels;
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(UpdateUser request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            //var roleIdentity = User.Identities.ElementAt(1);
            //var role = roleIdentity.Claims
            //                .Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
            //                .Select(c => c.Value).FirstOrDefault();
            var roles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRoleAsync(user, roles.FirstOrDefault());
            await _userManager.AddToRoleAsync(user, request.Role);
            await _userManager.UpdateAsync(user);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            await _userManager.DeleteAsync(user);

            return Ok();
        }
    }
}