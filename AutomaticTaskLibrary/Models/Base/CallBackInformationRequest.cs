namespace AutomaticTaskSharedLibrary
{
    public abstract class CallBackInformationRequest : IAutomaticTask
    {
        public string OrganizationId { get; set; }

        public string APIKey { get; set; }

        AutomaticTaskType IAutomaticTask.AutomaticTaskType => this.getAutomaticTaskType();

        protected abstract AutomaticTaskType getAutomaticTaskType();
    }
}
