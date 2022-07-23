namespace ResellerRepository
{
    using ApplicationLifeCycle;

    using ResellerRepositoryTypes;

    using SimpleInjector;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IResellerBalanceService, ResellerBalanceService>(Lifestyle.Transient);
            this.GlobalContainer.Register<IResellerRepository, ResellerRepository>(Lifestyle.Transient);

            this.GlobalContainer.Register<IUpdateReseller, UpdateResellerStart>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IUpdateReseller, UpdateResellerRetrieveReseller>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IUpdateReseller, UpdateResellerUpdate>(Lifestyle.Transient);

            return true;
        }
    }
}
