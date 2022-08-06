namespace DataSeedingLibrary
{
    using ApplicationLifeCycle;

    using SimpleInjector;

    using UnitOfWorkClassLibrary;

    using UnitOfWorkTypesLibrary;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Collection.Append<IRequestLifeCycleStartupItem, DataSeedingSoftwareTypeAndVendorStartupItem>(Lifestyle.Singleton);

            return true;
        }
    }
}
