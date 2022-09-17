
In the Package Manager Console run:
reference: https://docs.microsoft.com/en-us/ef/core/managing-schemas/scaffolding
reference: https://blog.tonysneed.com/2018/05/27/customize-ef-core-scaffolding-with-handlebars-templates/
reference: https://github.com/TrackableEntities/EntityFrameworkCore.Scaffolding.Handlebars

have to install this: dotnet tool install --global dotnet-ef

This project uses code first - no scaffolding
Make changes to the database models and then create a migration by running below

dotnet ef migrations add "Added username to vendor" -s DummyProjectForMigrations --context DPContext --project "DataPostgresqlLibrary"
