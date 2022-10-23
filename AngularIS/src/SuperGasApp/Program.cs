using Duende.Bff;
using Duende.Bff.Yarp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using SuperGasApp;
using SuperGasApp.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var migrationsAssembly = typeof(Program).Assembly.GetName().Name;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthorization();

builder.Services.AddBff(options =>
    options.LicenseKey = "eyJhbGciOiJQUzI1NiIsImtpZCI6IklkZW50aXR5U2VydmVyTGljZW5zZWtleS83Y2VhZGJiNzgxMzA0NjllODgwNjg5MTAyNTQxNGYxNiIsInR5cCI6ImxpY2Vuc2Urand0In0.eyJpc3MiOiJodHRwczovL2R1ZW5kZXNvZnR3YXJlLmNvbSIsImF1ZCI6IklkZW50aXR5U2VydmVyIiwiaWF0IjoxNjYyMzg2MDA1LCJleHAiOjE2OTM5MjIwMDUsImNvbXBhbnlfbmFtZSI6IlN1cGVyZ2FzIiwiY29udGFjdF9pbmZvIjoiZ2FicmllbGFuaWpAZ21haWwuY29tIiwiZWRpdGlvbiI6IkNvbW11bml0eSJ9.q6GP5wp7T6y3m-wGpYd047iBCN3Q6H4URkSL4AFwWr3w7zcLiX1qvB9xT1itjxkl1L-mQ6ZxiOLJNBQARX4EM5-lWjjh2vJ7zfVJZcoH-JD7YS7S537q1Fr2kAw0d5Zcd4X-dGwXG1zWjk27Ib6txrsAqbXOKgaMyubr5Wi0ZyVUqdW8XT5irgg9LyBpdP67sgJLUhSaSETF8ukVO5HxwK2jXYW9-7MhqRaZbu5I0F0DVMM3VQzZ8jKSj7aGPb3OqJBL_Y6xvdYe1vqaVOX-n7n2O4-IbT2_e4GaJ3xoW9WPASGP4kDJgfiSrZRaPYJ_fX4qDN5sCAh95dO3mDtPPQ"
    ).AddRemoteApis();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
        options.DefaultSignOutScheme = "oidc";
    })
    .AddCookie("Cookies", options =>
    {
        options.Cookie.Name = "__Host-bff";
        options.Cookie.SameSite = SameSiteMode.Strict;
    }).AddOpenIdConnect("oidc", options =>
    {
        //options.Authority = "https://localhost:5001";
        options.Authority = "https://seguridadsupergas.azurewebsites.net";
        options.ClientId = "bff";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        //options.ResponseMode = "query";

        options.GetClaimsFromUserInfoEndpoint = true;
        options.MapInboundClaims = false;
        options.SaveTokens = true;

        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("api1");
        options.Scope.Add("IdentityServerApi");
        options.Scope.Add("color");
        options.Scope.Add("offline_access");
        options.Scope.Add("custom.profile");

        options.Scope.Add("color");
        options.Scope.Add("custom.profile");

        options.GetClaimsFromUserInfoEndpoint = true;
        options.ClaimActions.MapUniqueJsonKey("favorite_color", "favorite_color");
        options.ClaimActions.MapUniqueJsonKey("offline_access", "offline_access");
        options.ClaimActions.MapUniqueJsonKey("role", "role");
        options.ClaimActions.MapUniqueJsonKey("preferred_username", "preferred_username");

        options.TokenValidationParameters = new()
        {
            NameClaimType = "name",
            RoleClaimType = "role"
        };
    });

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseRouting();

app.UseBff();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapBffManagementEndpoints();

    endpoints.MapControllers()
        .RequireAuthorization()
        .AsBffApiEndpoint(requireAntiForgeryCheck: false);

    //endpoints.MapRemoteBffApiEndpoint("/remote", "https://localhost:5001", false)
    endpoints.MapRemoteBffApiEndpoint("/remote", "https://seguridadsupergas.azurewebsites.net", false)
        .RequireAccessToken(TokenType.User);

    endpoints.MapFallbackToFile("/index.html");
});

app.ConfigurePipeline();
app.Run();