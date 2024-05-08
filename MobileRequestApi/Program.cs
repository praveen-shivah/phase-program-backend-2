using ApiHost.Middleware;

using AuthenticationRepository;

using AutomaticTaskQueueLibrary;

using CommonServices;

using DatabaseContext;

using InvoiceRepository;

using InvoiceRepositoryTypes;

using log4net;
using log4net.Config;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using MobileOMaticBackgroundServicesLibrary;

using OrganizationRepositoryTypes;

using ResellerRepository;

using ResellerRepositoryTypes;

using SecurityUtilitiesTypes;

using SimpleInjector;
using SimpleInjector.Lifestyles;

using System;
using System.IO;
using System.Reflection;
using System.Text;

using ApiHost;

using TransferRepository;

using VendorRepositoryTypes;
using PlayersRepositoryTypes;
using Microsoft.AspNetCore.Hosting;

var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
Console.WriteLine("Starting");

var applicationLifeCycle = new ApplicationLifeCycle.ApplicationLifeCycle("HostingRestService");
applicationLifeCycle.GlobalContainer.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
applicationLifeCycle.Initialize();
var response = await applicationLifeCycle.StartRequestAsync();

var loggerFactory = applicationLifeCycle.Resolve<LoggingLibrary.ILoggerFactory>();
var logger = loggerFactory.Create("HostingApplicationService");


var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.ConfigureKestrel(
//    serverOptions =>
//    {
//        serverOptions.ListenAnyIP(5000);
//        serverOptions.ListenAnyIP(5001);
//    });
var allowLocalHostOrigins = "allowLocalHostOrigins";
builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: allowLocalHostOrigins,
            policy =>
                {
                    policy.WithOrigins("http://localhost:8080", "http://mobileomatic.us-east-1.elasticbeanstalk.com").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
    });

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromSeconds(10);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
        {
            options.OperationFilter<JwtTokenHeaderFilter>();
        });

// Register dependencies here
builder.Services.AddSingleton(logger);
builder.Services.AddDbContext<DataContext>();
builder.Services.AddSingleton(applicationLifeCycle.Resolve<IDateTimeService>());
builder.Services.AddSingleton(applicationLifeCycle.Resolve<IJwtValidate>());

builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<IVendorRepository>());
builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<IOrganizationRepository>());
builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<IInvoiceRepository>());
builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<IResellerBalanceService>());
builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<IAuthenticationRepository>());
builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<ISecretKeyRetrieval>());
builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<IResellerRepository>());
builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<IUpdateResellerSiteRepository>());
builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<IAutomaticTaskQueueServiceProcessorRepository>());
builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<IJwtService>());
builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<IInvoiceListRetrieveRepository>());
builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<IInvoiceListResellerRetrieveRepository>());
builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<ITransferPointsQueueGetOutstandingItemsRepository>());
builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<IIdentityServer>());
builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<IUpdateResellerBalanceRepository>());
builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<IPlayersRepository>());

var connectionString = builder.Configuration.GetConnectionString("MobileOMatic");

builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseNpgsql(connectionString);
    });

// Register background services here
builder.Services.AddHostedService<DataHostedService>();
builder.Services.AddHostedService<AutomaticTaskQueueService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            };
        });

var app = builder.Build();

// Configure the HTTP requestDto pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseRequestResponseLogging();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseSession();
app.UseValidateAPICall();
app.UseCors(allowLocalHostOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
