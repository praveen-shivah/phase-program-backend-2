namespace AutomaticTaskMessageLibrary
{
    using NServiceBus;

    public interface IEndpointConfigurationFactory
    {
        EndpointConfiguration CreateEndpointConfiguration();
    }
}
