using Microsoft.AspNetCore.Mvc;
using DataLayer.Interfaces;
using DataLayer.Services;
using AutoMapper;
using DataLayer.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using static DataLayer.Response.EServiceResponseTypes;
using DataLayer.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using NuGet.Common;
namespace ExceptionHandler.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        private readonly IUserServices _userServices;
        private readonly IAuthenticationServices _authService;
        public UserController(ILogger<UserController> logger, IUserServices userService, IAuthenticationServices authService)
        {
            _logger = logger;
            _userServices = userService;
            _authService = authService;
        }
        [AllowAnonymous]
        [Route("/register")]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult> Register(UserRegisterDTO userdata)
        {
            var serviceResponse = await _userServices.RegisterAsync(userdata);
            return new ObjectResult(new
            {
                message = "Register Successful",
            })
            {
                StatusCode = 200,
                ContentTypes = new MediaTypeCollection { "application/json" },
            };
        }
        [AllowAnonymous]
        [Route("/login")]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult> Login(UserLoginDTO userdata)
        {
            var (token, refreshToken) = await _authService.LoginAsync(userdata);
             return new ObjectResult(new
            {
                token = token,
                refresh = refreshToken
            })
            {
                StatusCode = 200,
                ContentTypes = new MediaTypeCollection { "application/json" },
            };
        }
    }
}
