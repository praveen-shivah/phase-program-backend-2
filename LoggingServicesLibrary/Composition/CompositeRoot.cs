namespace LoggingServicesLibrary
{
    using ApplicationLifeCycle;

    using SimpleInjector;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IErrorLogDbPosting, ErrorLogDbPostingStart>(Lifestyle.Singleton);
            this.GlobalContainer.RegisterDecorator<IErrorLogDbPosting, ErrorLogDbPostingBuildHash>(Lifestyle.Singleton);
            this.GlobalContainer.RegisterDecorator<IErrorLogDbPosting, ErrorLogDbPostingCheckForExcessive>(Lifestyle.Singleton);
            this.GlobalContainer.RegisterDecorator<IErrorLogDbPosting, ErrorLogDbPostingSave>(Lifestyle.Singleton);

            this.GlobalContainer.Register<ISignificantEventLogDbPosting, SignificantEventLogDbPostingStart>(Lifestyle.Singleton);
            this.GlobalContainer.RegisterDecorator<ISignificantEventLogDbPosting, SignificantEventLogDbPostingSave>(Lifestyle.Singleton);

            return true;
        }
    }
}