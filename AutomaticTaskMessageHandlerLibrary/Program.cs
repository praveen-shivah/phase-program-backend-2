
using System.Reflection;

using AutomaticTaskLibrary;

using AutomaticTaskMessageLibrary;

using log4net;
using log4net.Config;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NServiceBus;

var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
Console.WriteLine("Starting");

var applicationLifeCycle = new ApplicationLifeCycle.ApplicationLifeCycle("HostingRestService");
applicationLifeCycle.Initialize();
var response = applicationLifeCycle.StartRequest();

var loggerFactory = applicationLifeCycle.Resolve<LoggingLibrary.ILoggerFactory>();
var logger = loggerFactory.Create("HostingApplicationService");

var endpointConfigurationFactory = applicationLifeCycle.Resolve<IEndpointConfigurationFactory>();

var host = Host.CreateDefaultBuilder(args)
    // Configure a host for the endpoint
    .ConfigureLogging((context, logging) =>
        {
            logging.AddConfiguration(context.Configuration.GetSection("Logging"));

            logging.AddConsole();
        })
    .UseConsoleLifetime()
    .UseNServiceBus(context =>
        {
            return endpointConfigurationFactory.CreateEndpointConfiguration();
        })
    .Build();

await host.RunAsync();