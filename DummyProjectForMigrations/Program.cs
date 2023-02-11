using DatabaseContext;

using Microsoft.EntityFrameworkCore;

using SharedUtilities;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
IConnectionFactory connectionFactory = new ConnectionFactoryNormal(configuration);
var connection = connectionFactory.Create();
var connectionString = connection.ConnectionString;

builder.Services.AddDbContext<DataContext>((s, options) => options.UseNpgsql(connectionString));
//builder.Services.AddSingleton<IDateTimeService, DateTimeServiceWithOffset>();

var app = builder.Build();

var migrateTask = app.MigrateDatabaseAsync();
migrateTask.Wait();