
In the Package Manager Console run:
reference: https://docs.microsoft.com/en-us/ef/core/managing-schemas/scaffolding
reference: https://blog.tonysneed.com/2018/05/27/customize-ef-core-scaffolding-with-handlebars-templates/
reference: https://github.com/TrackableEntities/EntityFrameworkCore.Scaffolding.Handlebars

have to install this: dotnet tool install --global dotnet-ef

NOTE:
  To start, a user login must be created by running this script:

  CREATE ROLE templatelogin WITH
  LOGIN
  NOSUPERUSER
  NOINHERIT
  CREATEDB
  NOCREATEROLE
  NOREPLICATION
  ENCRYPTED PASSWORD 'SCRAM-SHA-256$4096:FyOFuVfqzI9vrr/dL7Pm2A==$b9kjmjmHVndRfRPspUgG56cZSMm0ryKQ/7+ctKK+gjk=:x0WmjkES3g1td1B178/6u/T9+fzPVLVvBqKs4QZ9Kg8=';

/* ------------------------------------------------------------------------------------------------------*/
/*                                        Template database scripts                                      */
/* ------------------------------------------------------------------------------------------------------*/

// This creates/updates the local template database
dotnet ef database update -s "DummyProjectForMigrations" --connection "host=localhost;database=MobileOMaticTemplate_Local;User Id=templatelogin;Password=postgres" --project "DataModelsLibrary" 

// This line scaffolds/creates/updates the models and the datacontext
dotnet ef dbcontext scaffold "host=localhost;database=MobileOMaticTemplate_Local;User Id=templatelogin;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL --project "DataModelsLibrary" --context-dir Data --output-dir Models --data-annotations --context DataContext --force --no-pluralize

/* ------------------------------------------------------------------------------------------------------*/
/*                                        Migration Creation Scripts                                     */
/* ------------------------------------------------------------------------------------------------------*/

// This line creates a migration based on the models/context in the code
dotnet ef migrations add "REPLACE THIS" -s "DummyProjectForMigrations" --context DataContext --project "DataModelsLibrary"
