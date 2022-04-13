namespace UnitOfWorkClassLibrary
{
    using ApplicationLifeCycle;

    using Microsoft.EntityFrameworkCore;

    using SimpleInjector;

    using UnitOfWorkTypesLibrary;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IUnitOfWorkContextContainerFactory<DbContext>, UnitOfWorkContextContainerFactory<DbContext>>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IWorkItemFactory<DbContext>, WorkItemFactory<DbContext>>(Lifestyle.Singleton);

            return true;
        }
    }
}
