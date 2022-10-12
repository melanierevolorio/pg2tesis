rmdir /S /Q "Data/Migrations"

dotnet ef migrations add InitialUsers -c ApplicationDbContext -o Data/Migrations/Business/ApplicationDb
