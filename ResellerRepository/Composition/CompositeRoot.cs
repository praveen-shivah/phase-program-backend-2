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
            this.GlobalContainer.Register<IUpdateResellerSiteRepository, UpdateResellerSiteRepository>(Lifestyle.Transient);
            this.GlobalContainer.Register<IUpdateResellerBalanceRepository, UpdateResellerBalanceRepository>(Lifestyle.Transient);

            this.GlobalContainer.Register<IUpdateReseller, UpdateResellerStart>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IUpdateReseller, UpdateResellerRetrieveReseller>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IUpdateReseller, UpdateResellerAddSitesForEachVendor>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IUpdateReseller, UpdateResellerUpdate>(Lifestyle.Transient);

            this.GlobalContainer.Register<IUpdateResellerSite, UpdateResellerSiteStart>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IUpdateResellerSite, UpdateResellerSiteValidateNewInformation>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IUpdateResellerSite, UpdateResellerSiteProcess>(Lifestyle.Transient);

            this.GlobalContainer.Register<IUpdateResellerBalance, UpdateResellerBalanceStart>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IUpdateResellerBalance, UpdateResellerBalanceRetrieveReseller>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IUpdateResellerBalance, UpdateResellerBalanceProcess>(Lifestyle.Transient);

            return true;
        }
    }
}
