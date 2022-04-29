namespace AutomaticTaskMessageLibrary
{
    using AutomaticTaskLibrary;

    using NServiceBus;

    public class EndpointConfigurationFactoryTestingLocal : IEndpointConfigurationFactory
    {
        EndpointConfiguration IEndpointConfigurationFactory.CreateEndpointConfiguration(string endpointName, string destinationName)
        {
            var endpointConfiguration = new EndpointConfiguration(endpointName);
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            transport.ConnectionString(EndpointConfigurationConstants.AzureServiceBusConnectionString);
            transport.Routing().RouteToEndpoint(typeof(AutomaticTaskTransferPoints), destinationName);
            endpointConfiguration.EnableInstallers();
            return endpointConfiguration;
        }
    }
}
