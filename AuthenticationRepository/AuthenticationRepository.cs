namespace AuthenticationRepository
{
    using ApiDTO;

    using APISupportTypes;

    using AuthenticationRepositoryTypes;

    using DatabaseContext;

    using LoggingLibrary;

    using Microsoft.EntityFrameworkCore;

    using System.Net;

    using UnitOfWorkTypesLibrary;

    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IAuthenticateUser authenticateUser;

        private readonly IUpdateUser updateUser;

        private readonly ILogger logger;

        private readonly IUnitOfWorkFactory<DataContext> unitOfWorkFactory;

        public AuthenticationRepository(
            ILogger logger,
            IUnitOfWorkFactory<DataContext> unitOfWorkFactory,
            IAuthenticateUser authenticateUser,
            IUpdateUser updateUser)
        {
            this.logger = logger;
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.authenticateUser = authenticateUser;
            this.updateUser = updateUser;
        }

        async Task<AuthenticationResponse> IAuthenticationRepository.Authenticate(AuthenticationRequest authenticationRequest)
        {
            AuthenticateUserResponse authenticateUserResponse = new AuthenticateUserResponse();
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        this.logger.Info(LogClass.CommRest, "Calling authenticate user");
                        var response = await this.authenticateUser.AuthenticateUserAsync(
                                                       context,
                                                       new AuthenticateUserRequest
                                                       {
                                                           OrganizationId = authenticationRequest.OrganizationId,
                                                           UserName = authenticationRequest.UserId,
                                                           Password = authenticationRequest.Password,
                                                           IpAddress = authenticationRequest.IpAddress,
                                                           Audience = authenticationRequest.Audience
                                                       });

                        authenticateUserResponse.IsSuccessful = response.IsSuccessful;
                        authenticateUserResponse.IsAuthenticated = response.IsAuthenticated;
                        authenticateUserResponse.RefreshToken = response.RefreshToken;
                        authenticateUserResponse.Roles = response.Roles;
                        authenticateUserResponse.UserName = response.UserName;
                        authenticateUserResponse.Claims = response.Claims;
                        authenticateUserResponse.AccessToken = response.AccessToken;
                        authenticateUserResponse.User = response.User;
                        authenticateUserResponse.UserId = response.UserId;
                        authenticateUserResponse.ErrorMessage = response.ErrorMessage;
                        authenticateUserResponse.HttpStatusCode = response.HttpStatusCode;
                        authenticateUserResponse.ResponseTypeEnum = response.ResponseTypeEnum;
                        this.logger.Info(LogClass.CommRest, "Calling authenticate user done");

                        return WorkItemResultEnum.doneContinue;
                    });

            try
            {
                this.logger.Info(LogClass.CommRest, "Calling authenticate user done before executeAsync()");
                var result = await uow.ExecuteAsync();
                this.logger.Info(LogClass.CommRest, "Calling authenticate user done after executeAsync()");

                if (result != WorkItemResultEnum.commitSuccessfullyCompleted)
                {
                    this.logger.Info(LogClass.CommRest, "authenticate user repository failed");
                    return new AuthenticationResponse
                    {
                        IsSuccessful = false,
                        IsAuthenticated = false,
                        ErrorMessage = authenticateUserResponse.ErrorMessage,
                        HttpStatusCode = authenticateUserResponse.HttpStatusCode,
                        ResponseTypeEnum = authenticateUserResponse.ResponseTypeEnum
                    };
                }

                if (authenticateUserResponse.IsSuccessful && authenticateUserResponse.IsAuthenticated)
                {
                    this.logger.Info(LogClass.CommRest, "authenticate user repository success - building response");
                    return new AuthenticationResponse
                    {
                        OrganizationId = authenticationRequest.OrganizationId,
                        UserId = authenticateUserResponse.UserId,
                        UserName = authenticateUserResponse.UserName,
                        IsAuthenticated = authenticateUserResponse.IsAuthenticated,
                        IsSuccessful = true,
                        Roles = authenticateUserResponse.Roles,
                        AccessToken = authenticateUserResponse.AccessToken,
                        RefreshToken = authenticateUserResponse.RefreshToken,
                        ErrorMessage = authenticateUserResponse.ErrorMessage,
                        HttpStatusCode = authenticateUserResponse.HttpStatusCode,
                        ResponseTypeEnum = authenticateUserResponse.ResponseTypeEnum
                    };
                }

                this.logger.Info(LogClass.CommRest, "authenticate user repository not authenticated - building response");
                return new AuthenticationResponse
                {
                    OrganizationId = authenticationRequest.OrganizationId,
                    UserId = authenticateUserResponse.UserId,
                    UserName = authenticateUserResponse.UserName,
                    IsAuthenticated = authenticateUserResponse.IsAuthenticated,
                    IsSuccessful = true,
                    Roles = new List<int>(),
                    ErrorMessage = authenticateUserResponse.ErrorMessage,
                    HttpStatusCode = authenticateUserResponse.HttpStatusCode,
                    ResponseTypeEnum = authenticateUserResponse.ResponseTypeEnum
                };
            }
            catch (Exception e)
            {
                var innerMessage = string.Empty;
                if (e.InnerException != null)
                    innerMessage = e.InnerException.Message;

                this.logger.Info(LogClass.CommRest, $"authenticate user repository error: {e.Message} {innerMessage}");
                return new AuthenticationResponse
                {
                    OrganizationId = authenticationRequest.OrganizationId,
                    UserId = 0,
                    UserName = string.Empty,
                    IsAuthenticated = false,
                    IsSuccessful = false,
                    Roles = new List<int>(),
                    ErrorMessage = $"Error {e.Message}",
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    ResponseTypeEnum = ResponseTypeEnum.exceptionOccurred
                };
            }
        }

        async Task<UpdateUserResponse> IAuthenticationRepository.UpdateUser(string jwtTokenString, int organizationId, UpdateUserRequestDto updateUserRequestDto)
        {
            var result = new UpdateUserResponse() { IsSuccessful = true };
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        var response = await this.updateUser.Update(context, new UpdateUserRequest(jwtTokenString, organizationId, updateUserRequestDto));
                        result.IsSuccessful = response.IsSuccessful;
                        result.ErrorMessage = response.ErrorMessage;
                        result.HttpStatusCode = response.HttpStatusCode;
                        result.ResponseTypeEnum = response.ResponseTypeEnum;

                        return WorkItemResultEnum.doneContinue;
                    });
            var uowResult = await uow.ExecuteAsync();

            if (uowResult != WorkItemResultEnum.commitSuccessfullyCompleted)
            {
                return new UpdateUserResponse()
                {
                    IsSuccessful = false,
                    ResponseTypeEnum = ResponseTypeEnum.databaseError,
                    ErrorMessage = "database error",
                    HttpStatusCode = HttpStatusCode.InternalServerError
                };
            }

            return result;
        }

        async Task<List<UpdateUserRequestDto>> IAuthenticationRepository.GetUsers()
        {
            var userList = new List<UpdateUserRequestDto>();
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        var list = await context.User.ToListAsync();
                        userList.Add(new UpdateUserRequestDto() { IsPlaceHolder = true });
                        foreach (var user in list)
                        {
                            userList.Add(new UpdateUserRequestDto() { Id = user.Id, Email = user.Email, UserName = user.UserName, IsActive = user.IsActive });
                        }

                        return WorkItemResultEnum.doneContinue;
                    });
            var result = await uow.ExecuteAsync();
            if (result != WorkItemResultEnum.commitSuccessfullyCompleted)
            {
                return new List<UpdateUserRequestDto>();
            }

            return userList.OrderBy(x => x.UserName).ToList();
        }

        async Task<AuthenticationResponse> IAuthenticationRepository.GetUserById(int id)
        {
            AuthenticationResponse? authenticationResponse = null;
            var uow = this.unitOfWorkFactory.Create(
                async context =>
                    {
                        var user = await context.User.SingleOrDefaultAsync(x => x.Id == id);
                        if (user == null)
                        {
                            authenticationResponse = new AuthenticationResponse
                            {
                                IsAuthenticated = false,
                                IsSuccessful = false
                            };
                        }
                        else
                        {
                            authenticationResponse = new AuthenticationResponse
                            {
                                IsAuthenticated = true,
                                IsSuccessful = true,
                                UserId = user.Id,
                            };
                        }

                        return WorkItemResultEnum.doneContinue;
                    });

            var result = await uow.ExecuteAsync();
            if (result != WorkItemResultEnum.commitSuccessfullyCompleted || authenticationResponse == null)
            {
                return new AuthenticationResponse
                {
                    IsSuccessful = false,
                    IsAuthenticated = false
                };
            }

            return authenticationResponse;
        }
    }
}