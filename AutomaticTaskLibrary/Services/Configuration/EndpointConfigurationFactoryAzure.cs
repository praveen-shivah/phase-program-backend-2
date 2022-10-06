namespace AutomaticTaskSharedLibrary
{
    using NServiceBus;

    public class EndpointConfigurationFactoryAzure : IEndpointConfigurationFactory
    {
        EndpointConfiguration IEndpointConfigurationFactory.CreateEndpointConfiguration(string endpointName, string destinationName)
        {
            var endpointConfiguration = new EndpointConfiguration(endpointName);
            var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
            transport.ConnectionString(EndpointConfigurationConstants.AzureServiceBusConnectionString);
            transport.Routing().RouteToEndpoint(typeof(IAutomaticTask), destinationName);
            endpointConfiguration.EnableInstallers();
            return endpointConfiguration;
        }
    }
}
