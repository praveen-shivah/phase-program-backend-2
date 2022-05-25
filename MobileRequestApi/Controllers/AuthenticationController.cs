using Microsoft.AspNetCore.Mvc;

namespace ApiHost.Controllers
{
    using System.Threading.Tasks;

    using ApiDTO;

    using AuthenticationRepositoryTypes;

    using LoggingLibrary;

    using Microsoft.AspNetCore.Cors;

    [ApiController]
    [Route("api/authentication")]
    [EnableCors]
    public class AuthenticationController : Controller
    {
        private readonly ILogger logger;

        private readonly IAuthenticationRepository authenticationRepository;

        public AuthenticationController(ILogger logger, IAuthenticationRepository authenticationRepository)
        {
            this.logger = logger;
            this.authenticationRepository = authenticationRepository;
        }

        [HttpPost("admin-login")]
        public async Task<ActionResult<AuthenticateResponseDto>> AdminLogin(AuthenticateRequestDto authenticateDto)
        {
            this.logger.Debug(LogClass.General, "AdminLogin received");
            var result = await this.authenticationRepository.Authenticate(new AuthenticationRequest(authenticateDto.user, authenticateDto.pwd));
            return result.IsSuccessful ? this.Ok(result) : this.StatusCode(500, 0);
        }
    }
}
