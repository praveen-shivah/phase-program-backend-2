namespace MobileRequestApi
{
    using ApplicationLifeCycle;

    using LoggingLibrary;

    using SimpleInjector;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<ILoggerAdapterFactory, LoggerAdapterFactory>(Lifestyle.Singleton);
            this.GlobalContainer.Register<ILoggerFactory, LoggerFactory>(Lifestyle.Singleton);

            return true;
        }
    }
}
