
using System.Reflection;

using AutomaticTaskBrowserCommandProcessingLibrary;

using AutomaticTaskLibrary;

using AutomaticTaskMessageLibrary;

using log4net;
using log4net.Config;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NServiceBus;
using NServiceBus.SimpleInjector;

using SimpleInjector.Lifestyles;

var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
Console.WriteLine("Starting");

var applicationLifeCycle = new ApplicationLifeCycle.ApplicationLifeCycle("HostingRestService");
applicationLifeCycle.GlobalContainer.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
applicationLifeCycle.GlobalContainer.Options.AllowOverridingRegistrations = true;
applicationLifeCycle.GlobalContainer.Options.AutoWirePropertiesImplicitly();
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
    .ConfigureServices(
        services =>
            {
                services.AddTransient<IVendorToOperatorSendPointsTransferHandler, VendorToOperatorSendPointsTransferHandler>();
            })
    .UseNServiceBus(context =>
        {
            var endpointConfiguration = endpointConfigurationFactory.CreateEndpointConfiguration(EndpointConfigurationConstants.HandlerEndpoint, EndpointConfigurationConstants.QueueEndpoint);
            return endpointConfiguration;
        })
    .Build();

await host.RunAsync();