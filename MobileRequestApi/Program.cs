using System;
using System.Configuration;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Text;

using DataPostgresqlLibrary;

using InvoiceRepositoryTypes;

using log4net;
using log4net.Config;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

using MobileOMaticBackgroundServicesLibrary;

using MobileRequestApi.Middleware;

using OrganizationRepositoryTypes;

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
builder.Services.AddSingleton<IOrganizationRepository>(applicationLifeCycle.Resolve<IOrganizationRepository>());
builder.Services.AddSingleton<IInvoiceRepository>(applicationLifeCycle.Resolve<IInvoiceRepository>());

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
    app.UseRequestResponseLogging();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseValidateAPICall();

app.MapControllers();

app.Run();
