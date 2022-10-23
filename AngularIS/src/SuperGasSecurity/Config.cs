using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using SuperGasSecurity.Models;
using System.Security.Claims;

namespace SuperGasSecurity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("offline_access", new[] {"offline_access"}),
            new IdentityResource("color", new [] { "favorite_color" }),
            new IdentityResource("custom.profile", userClaims: new []
            {
                JwtClaimTypes.Name,
                JwtClaimTypes.Email,
                JwtClaimTypes.Role,
                JwtClaimTypes.PreferredUserName
            }),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("api1", "My API"),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
        };

    //public static IEnumerable<ApiResource> ApiResources =>
    //    new List<ApiResource>
    //    {
    //        new ApiResource(IdentityServerConstants.LocalApi.ScopeName, "Local API")
    //{
    //    // additional claims to put into access token
    //    UserClaims =
    //    {
    //        "role"
    //    }
    //}
    //    };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "bff",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                //AllowedCorsOrigins = new string[] {"https://localhost:5003", "https://localhost:5004"},
                AllowedCorsOrigins = new string[] {"https://supergaserp.azurewebsites.net"},

                // where to redirect to after login
                //RedirectUris = { "https://localhost:5004/signin-oidc" },
                RedirectUris = { "https://supergaserp.azurewebsites.net/signin-oidc" },
                // where to redirect to after logout
                //PostLogoutRedirectUris = { "https://localhost:5004/signout-callback-oidc" },
                PostLogoutRedirectUris = { "https://supergaserp.azurewebsites.net/signout-callback-oidc" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "api1",
                    "custom.profile",
                    "color",
                    IdentityServerConstants.LocalApi.ScopeName
                },
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowOfflineAccess = true
            }
        };

    public static IEnumerable<ApplicationUser> Users =>
        new List<ApplicationUser>
        {
            new ApplicationUser(){
                Id = "4687114b-bf9a-4f03-85c2-7c4b04484956",
                UserName = "alice",
                Email = "AliceSmith@email.com",
                EmailConfirmed = true,
                FavoriteColor = "green",
            },
            new ApplicationUser(){
                Id = "7cf2693f-f61d-4c57-ac61-90e51010f281",
                UserName = "bob",
                Email = "BobSmith@email.com",
                EmailConfirmed = true,
                FavoriteColor = "red",
            }
        };

    public static IEnumerable<IdentityRole> Roles =>
        new List<IdentityRole>
        {
            new IdentityRole(){
                Id = "1",
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR"
            },
            new IdentityRole(){
                Id = "2",
                Name = "Secretaria",
                NormalizedName = "SECRETARIA"
            }
        };

    public static IEnumerable<IdentityUserRole<string>> UserRoles =>
        new List<IdentityUserRole<string>>
        {
            new IdentityUserRole<string>()
            {
                RoleId = "1",
                UserId = "4687114b-bf9a-4f03-85c2-7c4b04484956"
            },
            new IdentityUserRole<string>()
            {
                RoleId = "1",
                UserId = "7cf2693f-f61d-4c57-ac61-90e51010f281"
            }
        };
}