
In the Package Manager Console run:
reference: https://docs.microsoft.com/en-us/ef/core/managing-schemas/scaffolding
reference: https://blog.tonysneed.com/2018/05/27/customize-ef-core-scaffolding-with-handlebars-templates/
reference: https://github.com/TrackableEntities/EntityFrameworkCore.Scaffolding.Handlebars

have to install this: dotnet tool install --global dotnet-ef


Make changes to the database models and then create a migration by running below

// This line scaffolds/creates/updates the models and the datacontext
dotnet ef dbcontext scaffold "host=localhost;database=MobileOMaticTemplate;User Id=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL --project "DataModelsLibrary" --context-dir Data --output-dir Models --data-annotations --context DataContext --force --no-pluralize

dotnet ef migrations add "Added username to vendor" -s DummyProjectForMigrations --context DataContext --project "DataModelsLibrary"
