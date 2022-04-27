
using AutomaticTaskLibrary;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NServiceBus;

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
            // Configure the NServiceBus endpoint
            var endpointConfiguration = new EndpointConfiguration(EndpointConfigurationConstants.AutomaticTaskMessageQueueEndpointHandler);

            var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
            var connectionString = context.Configuration.GetConnectionString("AzureServiceBusConnectionString");
            transport.ConnectionString(connectionString);

            endpointConfiguration.EnableInstallers();
            endpointConfiguration.AuditProcessedMessagesTo("audit");

            return endpointConfiguration;
        })
    .Build();

await host.RunAsync();