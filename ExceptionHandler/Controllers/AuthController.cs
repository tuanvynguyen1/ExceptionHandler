using DataLayer.DTOs.Users;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using static DataLayer.Response.EServiceResponseTypes;

namespace ExceptionHandler.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        private readonly IUserServices _userServices;
        private readonly IAuthenticationServices _authService;
        public AuthController(ILogger<UserController> logger, IUserServices userService, IAuthenticationServices authService)
        {
            _logger = logger;
            _userServices = userService;
            _authService = authService;
        }
        
        [Route("/register")]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult> Register(UserRegisterDTO userdata)
        {
            var serviceResponse = await _userServices.RegisterAsync(userdata);
            return serviceResponse.ResponseType switch
            {
                EResponseType.Success => CreatedAtAction(nameof(Register), new { version = "1" }, serviceResponse.Data),
                EResponseType.CannotCreate => BadRequest(serviceResponse.Message),
                _ => throw new NotImplementedException()
            };
        }

        [Route("/login")]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult> Login(UserLoginDTO userdata)
        {
            var (token, refreshToken) = await _authService.LoginAsync(userdata);
            return new ObjectResult(new
            {
                token = "Bearer " + token,
                refresh = "Bearer " + refreshToken,
                data = userdata
            })
            {
                StatusCode = 200,
                ContentTypes = new MediaTypeCollection { "application/json" },
            };
        }
    }
}
