using System;
using System.IO;
using System.Reflection;

using DataPostgresqlLibrary;

using log4net;
using log4net.Config;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MobileOMaticBackgroundServicesLibrary;

var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
Console.WriteLine("Starting");

var applicationLifeCycle = new ApplicationLifeCycle.ApplicationLifeCycle("HostingRestService");
applicationLifeCycle.Initialize();
var response = applicationLifeCycle.StartRequest();


var loggerFactory = applicationLifeCycle.Resolve<LoggingLibrary.ILoggerFactory>();
var logger = loggerFactory.Create("HostingApplicationService");


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register dependencies here
builder.Services.AddSingleton<LoggingLibrary.ILogger>(logger);
builder.Services.AddDbContext<DPContext>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
connectionString = @"host=localhost;database=postgres2;user id=postgres;password=~!AmyLee~!0";

builder.Services.AddDbContext<DPContext>(options =>
    {
        options.UseNpgsql(connectionString);
    });

// Register background services here
builder.Services.AddHostedService<DataHostedService>();

var app = builder.Build();

app.MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
