namespace OrganizationRepository
{
    using ApplicationLifeCycle;

    using AuthenticationRepository;

    using OrganizationRepositoryTypes;

    using SimpleInjector;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IOrganizationRepository, OrganizationRepository>(Lifestyle.Transient);

            this.GlobalContainer.Register<IUpdateOrganization, UpdateOrganizationStart>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IUpdateOrganization, UpdateOrganizationRetrieveOrganization>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IUpdateOrganization, UpdateOrganizationUpdate>(Lifestyle.Transient);

            return true;
        }
    }
}
