rmdir /S /Q "Migrations"

dotnet ef migrations add Usuarios -c ApplicationDbContext -o Migrations/ApplicationDb
dotnet ef migrations add Permisos -c PersistedGrantDbContext -o Migrations/PersistedGrantDb
dotnet ef migrations add Seguridad -c ConfigurationDbContext -o Migrations/ConfigurationDb
dotnet ef database update --context ApplicationDbContext
dotnet ef database update --context PersistedGrantDbContext
dotnet ef database update --context ConfigurationDbContext