namespace AutomaticTaskMessageHandlerHost
{
    using ApplicationLifeCycle;

    using LoggingLibrary;

    using SharedUtilities;

    using SimpleInjector;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<ILoggerAdapterFactory, LoggerAdapterFactory>(Lifestyle.Singleton);
            this.GlobalContainer.Register<ILoggerFactory, LoggerFactory>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IGuidFactory, GuidFactory>(Lifestyle.Singleton);

            return true;
        }
    }
}
