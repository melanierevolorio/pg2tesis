rmdir /S /Q "Data/Migrations"

dotnet ef migrations add InitialUsers -c ApplicationDbContext -o Data/Migrations/IdentityServer/ApplicationDb
dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb