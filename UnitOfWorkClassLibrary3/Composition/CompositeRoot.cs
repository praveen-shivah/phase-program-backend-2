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
            this.GlobalContainer.Register<IUnitOfWorkContextContainerFactoryGeneric<DbContext>, UnitOfWorkContextContainerFactoryGeneric<DbContext>>(Lifestyle.Singleton);
            this.GlobalContainer.Register<IWorkItemFactory, WorkItemFactoryGeneric>(Lifestyle.Singleton);

            return true;
        }
    }
}
