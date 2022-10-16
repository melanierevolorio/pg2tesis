rmdir /S /Q "Migrations"
dotnet ef migrations add Negocio -c ApplicationDbContext -o Migrations/Context
dotnet ef database update --context ApplicationDbContext