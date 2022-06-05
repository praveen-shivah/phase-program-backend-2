namespace AuthenticationRepository
{
    using ApplicationLifeCycle;

    using AuthenticationRepositoryTypes;

    using SimpleInjector;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IAuthenticationRepository, AuthenticationRepository>(Lifestyle.Transient);
            
            this.GlobalContainer.Register<IAuthenticateUser, AuthenticateUserStart>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IAuthenticateUser, AuthenticateUserCreateAdminIfNecessary>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IAuthenticateUser, AuthenticateUserRetrieve>(Lifestyle.Transient);

            this.GlobalContainer.Register<IStoreRefreshToken, StoreRefreshTokenStart>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IStoreRefreshToken, StoreRefreshTokenSave>(Lifestyle.Transient);

            this.GlobalContainer.Register<ICheckRefreshToken, CheckRefreshTokenStart>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<ICheckRefreshToken, CheckRefreshTokenRetrieve>(Lifestyle.Transient);

            return true;
        }
    }
}
