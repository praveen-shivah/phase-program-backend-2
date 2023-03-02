namespace DataSeedingLibrary
{
    using ApplicationLifeCycle;

    using SimpleInjector;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<ISeedData, SeedDataStart>(Lifestyle.Singleton);
            this.GlobalContainer.RegisterDecorator<ISeedData, SeedDataAddOrganizations>(Lifestyle.Singleton);
            this.GlobalContainer.RegisterDecorator<ISeedData, SeedDataAddSoftwareTypes>(Lifestyle.Singleton);
            this.GlobalContainer.RegisterDecorator<ISeedData, SeedDataAddVendors>(Lifestyle.Singleton);
            this.GlobalContainer.RegisterDecorator<ISeedData, SeedDataAddMissingSitesToResellers>(Lifestyle.Singleton);

            this.GlobalContainer.Collection.Append<IRequestLifeCycleStartupItem, DataSeedingSoftwareTypeAndVendorStartupItem>(Lifestyle.Singleton);
            this.GlobalContainer.Collection.Append<IRequestLifeCycleStartupItem, RequestLifeCycleStartupItemMigrations>(Lifestyle.Singleton);

            return true;
        }
    }
}
