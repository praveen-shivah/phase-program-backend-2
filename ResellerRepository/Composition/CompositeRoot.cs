namespace ResellerRepository
{
    using ApplicationLifeCycle;

    using SimpleInjector;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IResellerBalanceService, ResellerBalanceService>(Lifestyle.Transient);

            return true;
        }
    }
}
