using Microsoft.AspNetCore.Mvc;
using DataLayer.Interfaces;
using DataLayer.Services;
using AutoMapper;
using DataLayer.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using static DataLayer.Response.EServiceResponseTypes;
namespace ExceptionHandler.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IUserServices _userServices;

        public UserController(ILogger<UserController> logger, IUserServices userService)
        {
            _logger = logger;
            _userServices = userService;
        }
        [AllowAnonymous]
        [Route("/register")]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult> Register(UserRegisterDTO userdata)
        {
            var serviceResponse = await _userServices.RegisterAsync(userdata);
            return serviceResponse.ResponseType switch
            {
                EResponseType.Success => CreatedAtAction(nameof(Profile), new { version = "1" }, serviceResponse.Data),
                EResponseType.CannotCreate => BadRequest(serviceResponse.Message),
                _ => throw new NotImplementedException()
            };
        }

    }
}
