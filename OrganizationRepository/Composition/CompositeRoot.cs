namespace OrganizationRepository
{
    using ApplicationLifeCycle;

    using DataPostgresqlLibrary;

    using OrganizationRepositoryTypes;

    using SimpleInjector;

    using UnitOfWorkClassLibrary;

    using UnitOfWorkTypesLibrary;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IOrganizationRepository, OrganizationRepository>(Lifestyle.Singleton);

            return true;
        }
    }
}
