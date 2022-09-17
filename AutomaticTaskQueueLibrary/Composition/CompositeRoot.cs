namespace AutomaticTaskQueueLibrary
{
    using ApplicationLifeCycle;

    using SimpleInjector;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IAutomaticTaskQueueServiceProcessorRepository, AutomaticTaskQueueServiceProcessorRepository>();
 
            this.GlobalContainer.Register<IAutomaticTaskQueueServiceProcessor, AutomaticTaskQueueServiceProcessorStart>();
            this.GlobalContainer.RegisterDecorator<IAutomaticTaskQueueServiceProcessor, AutomaticTaskQueueServiceProcessorPullRecord>();
            this.GlobalContainer.RegisterDecorator<IAutomaticTaskQueueServiceProcessor, AutomaticTaskQueueServiceProcessorProcess>();

            return true;
        }
    }
}
