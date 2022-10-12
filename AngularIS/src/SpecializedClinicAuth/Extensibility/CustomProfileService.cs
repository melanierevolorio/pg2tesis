using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Models;
using IdentityModel;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Identity;
using SpecializedClinicAuth.Models;
using System.Security.Claims;

namespace SpecializedClinicAuth.Extensibility;

public class CustomProfileService : ProfileService<ApplicationUser>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public CustomProfileService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory
        )
        : base(userManager, claimsFactory)
    {
        this._userManager = userManager;
        this._roleManager = roleManager;
    }
    public async Task<List<Claim>> GetClaimsFromUserAsync(ApplicationUser user)
    {
        var claims = new List<Claim> {
                new Claim(JwtClaimTypes.Subject,user.Id.ToString()),
                new Claim(JwtClaimTypes.PreferredUserName,user.UserName)
            };

        var role = await _userManager.GetRolesAsync(user);
        role.ToList().ForEach(f =>
        {
            claims.Add(new Claim(JwtClaimTypes.Role, f));
        });


        return claims;
    }


    protected override async Task GetProfileDataAsync(ProfileDataRequestContext context, ApplicationUser user)
    {
        var principal = await GetUserClaimsAsync(user);
        var id = (ClaimsIdentity)principal.Identity;
        if (!string.IsNullOrEmpty(user.FavoriteColor))
        {
            id.AddClaim(new Claim("favorite_color", user.FavoriteColor));
        }

        //context.IssuedClaims = await GetClaimsFromUserAsync(user);

        context.AddRequestedClaims(principal.Claims);
    }
}