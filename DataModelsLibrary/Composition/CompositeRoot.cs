namespace DatabaseContext
{
    using ApplicationLifeCycle;

    using SimpleInjector;

    using UnitOfWorkClassLibrary;

    using UnitOfWorkTypesLibrary;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IUnitOfWorkFactory<DataContext>, UnitOfWorkFactory<DataContext>>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IUnitOfWorkContextContainerFactory<DataContext>, UnitOfWorkContextContainerFactory<DataContext>>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IWorkItemFactory<DataContext>, WorkItemFactory<DataContext>>(Lifestyle.Singleton);

            return true;
        }
    }
}
