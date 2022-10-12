using Duende.Bff;
using Duende.Bff.Yarp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using SpecializedClinicApplication;
using SpecializedClinicApplication.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var migrationsAssembly = typeof(Program).Assembly.GetName().Name;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthorization();

builder.Services.AddBff(options =>
    options.LicenseKey = "eyJhbGciOiJQUzI1NiIsImtpZCI6IklkZW50aXR5U2VydmVyTGljZW5zZWtleS83Y2VhZGJiNzgxMzA0NjllODgwNjg5MTAyNTQxNGYxNiIsInR5cCI6ImxpY2Vuc2Urand0In0.eyJpc3MiOiJodHRwczovL2R1ZW5kZXNvZnR3YXJlLmNvbSIsImF1ZCI6IklkZW50aXR5U2VydmVyIiwiaWF0IjoxNjYwNDc5ODc4LCJleHAiOjE2OTIwMTU4NzgsImNvbXBhbnlfbmFtZSI6IkRlbnRhbCBFc2NvYmFyIiwiY29udGFjdF9pbmZvIjoiamVnbnpjQGdtYWlsLmNvbSIsImVkaXRpb24iOiJDb21tdW5pdHkifQ.ViQpxTylWcKbvenTieDhU3utG33VfRjrb9oTShb-VV3hdn1JsWEyArTpou_PuetxUvKmj9LGs-WAybO3jGbPTvOjTA3Ayq5k9E-XMnKPqDEYSQbHloYuAkjM7lE8LKXBX3H9K2rQJsOFjVlhDisJdciKCfxZov1RuPFZ31SfeA2kna2jcxq_k7v4dzYWdmwf_gHSu3NJIq7IQPdltkbJR6o1gCnIkkJyNFUqu9aPxQNwHvbJ99BVSyVKMNvoIeNbC4HBBw87tNBgxin8PlJzjBcUGIJA-DIj-BZ3jiBsYOMZc3nXaGpBuzCDZI-6wfEywEv8CbBsE558oQCA2OYOsg"
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
        options.Authority = "https://localhost:5001";
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

    endpoints.MapRemoteBffApiEndpoint("/remote", "https://localhost:5001", false)
        .RequireAccessToken(TokenType.User);

    endpoints.MapFallbackToFile("/index.html");
});

app.ConfigurePipeline();
app.Run();