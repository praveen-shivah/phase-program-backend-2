namespace AutomaticTaskMessageLibrary
{
    using AutomaticTaskLibrary;

    using NServiceBus;

    public class EndpointConfigurationFactoryAzureTestingLocal : IEndpointConfigurationFactory
    {
        EndpointConfiguration IEndpointConfigurationFactory.CreateEndpointConfiguration()
        {
            var endpointConfiguration = new EndpointConfiguration(EndpointConfigurationConstants.AutomaticTaskMessageQueueEndpointQueue);
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            transport.Routing().RouteToEndpoint(typeof(IAutomaticTask), EndpointConfigurationConstants.AutomaticTaskMessageQueueEndpointHandler);
            endpointConfiguration.EnableInstallers();
            return endpointConfiguration;
        }
    }
}
