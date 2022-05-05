namespace AutomaticTaskLibrary
{
    public enum SoftwareType
    {
        riverSweeps
    }

    public static class EndpointConfigurationConstants
    {
        public static readonly string HandlerEndpoint = "HANDLER";

        public static readonly string QueueEndpoint = "QUEUE";

        public static readonly string AzureServiceBusConnectionString = "AzureServiceBusConnectionString";
    }
}
