namespace TransferRepository
{
    using ApplicationLifeCycle;
    using SimpleInjector;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<ITransferPointsQueueGetOutstandingItemsRepository, TransferPointsQueueGetOutstandingItemsRepository>(Lifestyle.Transient);

            this.GlobalContainer.Register<ITransferPointsQueueGetOutstandingItems, TransferPointsQueueGetOutstandingItemsStart>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<ITransferPointsQueueGetOutstandingItems, TransferPointsQueueGetOutstandingItemsProcess>(Lifestyle.Transient);

            return true;
        }
    }
}
