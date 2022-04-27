namespace AutomaticTaskMessageLibrary
{
    using AutomaticTaskLibrary;

    using NServiceBus;

    public class EndpointConfigurationFactory : IEndpointConfigurationFactory
    {
        EndpointConfiguration IEndpointConfigurationFactory.CreateEndpointConfiguration()
        {
            var endpointConfiguration = new EndpointConfiguration(EndpointConfigurationConstants.AutomaticTaskMessageQueueEndpointQueue);
            var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
            transport.ConnectionString(EndpointConfigurationConstants.AzureServiceBusConnectionString);
            transport.Routing().RouteToEndpoint(typeof(IAutomaticTask), EndpointConfigurationConstants.AutomaticTaskMessageQueueEndpointHandler);
            endpointConfiguration.EnableInstallers();
            return endpointConfiguration;
        }
    }
}
