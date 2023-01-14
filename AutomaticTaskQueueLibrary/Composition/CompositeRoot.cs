namespace AutomaticTaskQueueLibrary
{
    using ApplicationLifeCycle;

    using InvoiceRepository;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IAutomaticTaskQueueServiceProcessorRepository, AutomaticTaskQueueServiceProcessorRepository>();
            this.GlobalContainer.Register<IDistributorToOperatorSendPointsTransfer, DistributorToOperatorSendPointsTransfer>();
            this.GlobalContainer.Register<IResellerBalanceRetrieve, ResellerBalanceRetrieve>();
            this.GlobalContainer.Register<ISiteProcessorUrls, SiteProcessorUrls>();

            this.GlobalContainer.Register<IAutomaticTaskQueueServiceProcessor, AutomaticTaskQueueServiceProcessorStart>();
            this.GlobalContainer.RegisterDecorator<IAutomaticTaskQueueServiceProcessor, AutomaticTaskQueueServiceProcessorPullRecord>();
            this.GlobalContainer.RegisterDecorator<IAutomaticTaskQueueServiceProcessor, AutomaticTaskQueueServiceProcessorProcess>();

            return true;
        }
    }
}
