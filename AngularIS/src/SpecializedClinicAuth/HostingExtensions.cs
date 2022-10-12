using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SpecializedClinicAuth.Data;
using SpecializedClinicAuth.Extensibility;
using SpecializedClinicAuth.Middleware;
using SpecializedClinicAuth.Models;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace SpecializedClinicAuth;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();

        var migrationsAssembly = typeof(Program).Assembly.GetName().Name;

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddLocalApiAuthentication();

        builder.Services
            .AddIdentityServer(options =>
            {
                options.LicenseKey = "eyJhbGciOiJQUzI1NiIsImtpZCI6IklkZW50aXR5U2VydmVyTGljZW5zZWtleS83Y2VhZGJiNzgxMzA0NjllODgwNjg5MTAyNTQxNGYxNiIsInR5cCI6ImxpY2Vuc2Urand0In0.eyJpc3MiOiJodHRwczovL2R1ZW5kZXNvZnR3YXJlLmNvbSIsImF1ZCI6IklkZW50aXR5U2VydmVyIiwiaWF0IjoxNjYwNDc5ODc4LCJleHAiOjE2OTIwMTU4NzgsImNvbXBhbnlfbmFtZSI6IkRlbnRhbCBFc2NvYmFyIiwiY29udGFjdF9pbmZvIjoiamVnbnpjQGdtYWlsLmNvbSIsImVkaXRpb24iOiJDb21tdW5pdHkifQ.ViQpxTylWcKbvenTieDhU3utG33VfRjrb9oTShb-VV3hdn1JsWEyArTpou_PuetxUvKmj9LGs-WAybO3jGbPTvOjTA3Ayq5k9E-XMnKPqDEYSQbHloYuAkjM7lE8LKXBX3H9K2rQJsOFjVlhDisJdciKCfxZov1RuPFZ31SfeA2kna2jcxq_k7v4dzYWdmwf_gHSu3NJIq7IQPdltkbJR6o1gCnIkkJyNFUqu9aPxQNwHvbJ99BVSyVKMNvoIeNbC4HBBw87tNBgxin8PlJzjBcUGIJA-DIj-BZ3jiBsYOMZc3nXaGpBuzCDZI-6wfEywEv8CbBsE558oQCA2OYOsg";
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddAspNetIdentity<ApplicationUser>()
            .AddProfileService<CustomProfileService>();


        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        InitializeDatabase(app);

        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();
        //app.UseMiddleware<ClaimsMiddleware>();

        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }

    private static void InitializeDatabase(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            var persistedGrantContext = serviceScope.ServiceProvider.GetService<PersistedGrantDbContext>();
            var configContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            context.Database.EnsureDeleted();
            persistedGrantContext.Database.EnsureDeleted();
            configContext.Database.EnsureDeleted();

            context.Database.Migrate();
            persistedGrantContext.Database.Migrate();
            configContext.Database.Migrate();

            if (!configContext.Clients.Any())
                foreach (var client in Config.Clients)
                    configContext.Clients.Add(client.ToEntity());

            if (!configContext.IdentityResources.Any())
                foreach (var resource in Config.IdentityResources)
                    configContext.IdentityResources.Add(resource.ToEntity());

            //if (!configContext.ApiResources.Any())
            //    foreach (var apiResource in Config.ApiResources)
            //        configContext.ApiResources.Add(apiResource.ToEntity());

            if (!configContext.ApiScopes.Any())
                foreach (var resource in Config.ApiScopes)
                    configContext.ApiScopes.Add(resource.ToEntity());

            if (!context.Users.Any())
                foreach (var user in Config.Users)
                {
                    var result = userManager.CreateAsync(user, "Pass123$").Result;
                    result = userManager.AddClaimsAsync(user, new Claim[]{
                            new Claim(JwtClaimTypes.Name, user.UserName + " " + "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, user.UserName + " " + "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, user.UserName + " " + "Smith"),
                            new Claim(JwtClaimTypes.WebSite, user.UserName + " " + "http://bob.com"),
                            new Claim("location", user.UserName + " " + "somewhere")
                        }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }

            if (!context.Roles.Any())
                foreach (var role in Config.Roles)
                    context.Roles.Add(role);

            if (!context.UserRoles.Any())
                foreach (var userRole in Config.UserRoles)
                    context.UserRoles.Add(userRole);

            configContext.SaveChanges();
            context.SaveChanges();
        }
    }
}