﻿namespace AuthenticationRepository
{
    using APISupportTypes;

    using ApplicationLifeCycle;

    using RestServicesSupport;

    using SimpleInjector;

    using ISAccountRequestDto = APISupportTypes.ISAccountRequestDto;
    using ISAccountResponseDto = APISupportTypes.ISAccountResponseDto;
    using ISAccountUpdateRequestDto = APISupportTypes.ISAccountUpdateRequestDto;
    using ISAccountUpdateResponseDto = APISupportTypes.ISAccountUpdateResponseDto;
    using ISAuthenticateRequestDto = APISupportTypes.ISAuthenticateRequestDto;
    using ISAuthenticateResponseDto = APISupportTypes.ISAuthenticateResponseDto;
    using ISLogoutRequestDto = APISupportTypes.ISLogoutRequestDto;
    using ISLogoutResponseDto = APISupportTypes.ISLogoutResponseDto;
    using ISRefreshTokenRequestDto = APISupportTypes.ISRefreshTokenRequestDto;
    using ISRefreshTokenResponseDto = APISupportTypes.ISRefreshTokenResponseDto;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            this.GlobalContainer.Register<IAuthenticationRepository, AuthenticationRepository>(Lifestyle.Transient);
            this.GlobalContainer.Register<IJwtService, JwtService>(Lifestyle.Transient);
            this.GlobalContainer.Register<IJwtValidate, JwtValidateCheckIssuer>(Lifestyle.Singleton);
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

            this.GlobalContainer.Register<IUpdateUser, UpdateUserStart>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IUpdateUser, UpdateUserRetrieveUser>(Lifestyle.Transient);
            this.GlobalContainer.RegisterDecorator<IUpdateUser, UpdateUserUpdateLocalInformation>(Lifestyle.Transient);

            return true;
        }
    }
}
