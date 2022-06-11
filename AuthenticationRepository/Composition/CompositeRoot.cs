namespace AuthenticationRepository
{
    using ApplicationLifeCycle;

    using SimpleInjector;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IAuthenticationRepository, AuthenticationRepository>(Lifestyle.Transient);
            this.GlobalContainer.Register<IJwtService, JwtService>(Lifestyle.Transient);
            
            this.GlobalContainer.Register<IAuthenticateUser, AuthenticateUserStart>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IAuthenticateUser, AuthenticateUserCreateAdminIfNecessary>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IAuthenticateUser, AuthenticateUserRetrieve>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IAuthenticateUser, AuthenticateUserGenerateJwt>(Lifestyle.Transient);

            this.GlobalContainer.Register<IRefreshToken, RefreshTokenStart>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IRefreshToken, RefreshTokenRetrieveUser>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IRefreshToken, RefreshTokenRetrieveRefreshToken>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IRefreshToken, RefreshTokenValidateExpiration>(Lifestyle.Transient);

            this.GlobalContainer.Register<ILogout, LogoutStart>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<ILogout, LogoutRetrieveUser>(Lifestyle.Transient);

            return true;
        }
    }
}
