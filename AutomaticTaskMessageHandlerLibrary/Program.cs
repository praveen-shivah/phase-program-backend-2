﻿
using System;
using System.IO;
using System.Reflection;

using AutomaticTaskBrowserCommandProcessingLibrary;
using log4net;
using log4net.Config;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PlayersRepositoryTypes;
using SimpleInjector.Lifestyles;
using CommonServices;
using DatabaseContext;
using Microsoft.Extensions.Configuration;
using SharedUtilities;
using Microsoft.EntityFrameworkCore;

var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
Console.WriteLine("Starting");

var applicationLifeCycle = new ApplicationLifeCycle.ApplicationLifeCycle("HostingRestService");
applicationLifeCycle.GlobalContainer.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
applicationLifeCycle.GlobalContainer.Options.AllowOverridingRegistrations = true;
applicationLifeCycle.Initialize();
var response = await applicationLifeCycle.StartRequestAsync();

var loggerFactory = applicationLifeCycle.Resolve<LoggingLibrary.ILoggerFactory>();
var logger = loggerFactory.Create("HostingApplicationService");

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(
    serverOptions =>
        {
            serverOptions.ListenAnyIP(5000);
            serverOptions.ListenAnyIP(5001);
        });
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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register dependencies here
builder.Services.AddSingleton(logger);
builder.Services.AddDbContext<DataContext>();
builder.Services.AddSingleton(applicationLifeCycle.Resolve<IDateTimeService>());
builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<IDistributorToResellerSendPointsTransferProcessor>());
builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<IResellerBalanceRetrieveProcessor>());
builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<IResellerTransactionRetrieveProcessor>());
builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<IResellerPlayersRetrieveProcessor>());
builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<IPlayersRepository>());

var connectionString = builder.Configuration.GetConnectionString("MobileOMatic");

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(connectionString);
});

//builder.Services.AddTransient<IPlayersInformationRepository, PlayersRepository.Services.PlayersRepository>();
//builder.Services.AddTransient<IPlayersRepository, PlayersInformationUpdate>();
//builder.Services.AddTransient<IPlayersInformationRepository, PlayersRepository.Services.PlayersRepository>();


// builder.Services.AddTransient(_ => applicationLifeCycle.Resolve<IAutomaticTaskQueueServiceProcessorRepository>());

// Register background services here
//builder.Services.AddHostedService<DataHostedService>();
//builder.Services.AddHostedService<AutomaticTaskQueueService>();


var app = builder.Build();

// Configure the HTTP requestDto pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors(allowLocalHostOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
