﻿namespace AuthenticationRepository
{
    using ApplicationLifeCycle;

    using RestServicesSupport;

    using RestServicesSupportTypes;

    using SimpleInjector;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IAuthenticationRepository, AuthenticationRepository>(Lifestyle.Transient);
            this.GlobalContainer.Register<IJwtService, JwtService>(Lifestyle.Transient);
            this.GlobalContainer.Register<IJwtValidate, JwtValidate>(Lifestyle.Singleton);
            this.GlobalContainer.Register<ICalculatePassword, CalculatePassword>(Lifestyle.Singleton);
            this.GlobalContainer.Register<ICreatePasswordSalt, CreatePasswordSalt>(Lifestyle.Singleton);

            this.GlobalContainer.Register<IIdentityServer, IdentityServer>();
            this.GlobalContainer.Register<IIdentityServerUrl, IdentityServerUrl>();

            this.GlobalContainer.Register<IRestServices<ISAuthenticateRequestDto, ISAuthenticateResponseDto>, RestServicesExternal<ISAuthenticateRequestDto, ISAuthenticateResponseDto>>();
            this.GlobalContainer.Register<IRestServices<ISLogoutRequestDto, ISLogoutResponseDto>, RestServicesExternal<ISLogoutRequestDto, ISLogoutResponseDto>>();
            this.GlobalContainer.Register<IRestServices<ISRefreshTokenRequestDto, ISRefreshTokenResponseDto>, RestServicesExternal<ISRefreshTokenRequestDto, ISRefreshTokenResponseDto>>();
            this.GlobalContainer.Register<IRestServices<ISAccountRequestDto, ISAccountResponseDto>, RestServicesExternal<ISAccountRequestDto, ISAccountResponseDto>>();
            this.GlobalContainer.Register<IRestServices<ISAccountUpdateRequestDto, ISAccountUpdateResponseDto>, RestServicesExternal<ISAccountUpdateRequestDto, ISAccountUpdateResponseDto>>();

            this.GlobalContainer.Register<IAuthenticateUser, AuthenticateUserStart>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IAuthenticateUser, AuthenticateUserIdentityServer>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IAuthenticateUser, AuthenticateUserParseClaims>(Lifestyle.Transient);

            this.GlobalContainer.Register<IRefreshToken, RefreshTokenStart>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IRefreshToken, RefreshTokenRetrieveUser>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IRefreshToken, RefreshTokenRetrieveRefreshToken>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IRefreshToken, RefreshTokenValidateExpiration>(Lifestyle.Transient);

            this.GlobalContainer.Register<ILogout, LogoutStart>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<ILogout, LogoutRetrieveUser>(Lifestyle.Transient);

            this.GlobalContainer.Register<IUpdateUser, UpdateUserStart>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IUpdateUser, UpdateUserRetrieveUser>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IUpdateUser, UpdateUserUpdate>(Lifestyle.Transient);

            return true;
        }
    }
}
