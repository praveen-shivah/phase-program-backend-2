using System;
using System.Configuration;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Text;

using AuthenticationRepositoryTypes;

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

using ResellerRepository;

var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
Console.WriteLine("Starting");

var applicationLifeCycle = new ApplicationLifeCycle.ApplicationLifeCycle("HostingRestService");
applicationLifeCycle.Initialize();
var response = applicationLifeCycle.StartRequest();


var loggerFactory = applicationLifeCycle.Resolve<LoggingLibrary.ILoggerFactory>();
var logger = loggerFactory.Create("HostingApplicationService");


var builder = WebApplication.CreateBuilder(args);

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: myAllowSpecificOrigins,
            policy =>
                {
                    policy.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
    });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register dependencies here
builder.Services.AddSingleton<LoggingLibrary.ILogger>(logger);
builder.Services.AddDbContext<DPContext>();
builder.Services.AddSingleton(applicationLifeCycle.Resolve<IOrganizationRepository>());
builder.Services.AddSingleton(applicationLifeCycle.Resolve<IInvoiceRepository>());
builder.Services.AddSingleton(applicationLifeCycle.Resolve<IResellerBalanceService>());
builder.Services.AddSingleton(applicationLifeCycle.Resolve<IAuthenticationRepository>());

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
connectionString = @"host=localhost;database=postgres2;user id=postgres;pwd=~!AmyLee~!0";

builder.Services.AddDbContext<DPContext>(options =>
    {
        options.UseNpgsql(connectionString);
    });

// Register background services here
builder.Services.AddHostedService<DataHostedService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

var app = builder.Build();

await app.MigrateDatabaseAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseRequestResponseLogging();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
// app.UseValidateAPICall();

app.UseCors(myAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
