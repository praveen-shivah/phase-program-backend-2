
In the Package Manager Console run:
reference: https://docs.microsoft.com/en-us/ef/core/managing-schemas/scaffolding
reference: https://blog.tonysneed.com/2018/05/27/customize-ef-core-scaffolding-with-handlebars-templates/

have to install this: dotnet tool install --global dotnet-ef


dotnet ef dbcontext scaffold "host=localhost;database=postgres;user id=postgres;password=~!AmyLee~!0" Npgsql.EntityFrameworkCore.PostgreSQL --project DataPostgresqlLibrary --context-dir Data --output-dir Models --data-annotations --context DPContext --force

dotnet ef migrations add "Initial migration" --context DPContext --project "DataPostgresqlLibrary"
