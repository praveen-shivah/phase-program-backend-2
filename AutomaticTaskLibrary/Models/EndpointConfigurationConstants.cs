namespace AutomaticTaskLibrary
{
    public enum SoftwareType
    {
        notSet,
        riverSweeps
    }

    public static class EndpointConfigurationConstants
    {
        public static readonly string AutomaticTaskMessageQueueEndpointQueue = "AUTOMATIC_TASK_MESSAGE_QUEUE_ENDPOINT";

        public static readonly string AutomaticTaskMessageQueueEndpointHandler = "AUTOMATIC_TASK_MESSAGE_HANDLER_ENDPOINT";

        public static readonly string AzureServiceBusConnectionString = "AzureServiceBusConnectionString";
    }
}
