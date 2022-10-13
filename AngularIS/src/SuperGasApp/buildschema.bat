rmdir /S /Q "Migrations"

dotnet ef migrations add Negocio -c ApplicationDbContext -o Migrations/Context
