namespace AuthenticationRepository
{
    using ApplicationLifeCycle;

    using AuthenticationRepositoryTypes;

    using SimpleInjector;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IAuthenticationRepository, AuthenticationRepository>(Lifestyle.Singleton);
            
            return true;
        }
    }
}
