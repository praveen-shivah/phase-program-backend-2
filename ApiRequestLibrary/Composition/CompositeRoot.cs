namespace MobileRequestApi
{
    using ApiRequestLibrary;

    using ApplicationLifeCycle;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IApiURLFactory, ApiURLFactory>();
            this.GlobalContainer.Register<IResellerBalance, ResellerBalance>();
            this.GlobalContainer.Register<IWebRequestClient, WebRequestClient>();

            return true;
        }
    }
}
