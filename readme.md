## Database postgres

docker-compose up --force-recreate --build -d

## Migration database

dotnet ef dbcontext scaffold "Host=localhost;Port=5432;Database=postgres;User Id=postgres;Password=example;" Npgsql.EntityFrameworkCore.PostgreSQL -o Entities --context-dir Infrastructures/Database/ -c ManageEmployeeDbContext -d -f
