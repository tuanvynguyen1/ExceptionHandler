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
using System.Security.Claims;
namespace ExceptionHandler.Controllers
{
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        private readonly IUserServices _userServices;
        public UserController(ILogger<UserController> logger, IUserServices userService)
        {
            _logger = logger;
            _userServices = userService;

        }
        
        [Route("/update")]
        [Produces("application/json")]
        [HttpPost]
        
        public async Task<ActionResult> Update(UserInfoDTO userdata)
        {
            var claims = User.Claims.Where(x => x.Type == "UserId").FirstOrDefault();
            string? userid = claims == null ? null : claims.Value.ToString();
            var serviceResponse = await _userServices.updateUserInfo(userdata, userid);
            return serviceResponse.ResponseType switch
            {
                EResponseType.Success => Ok(serviceResponse.Data),
                EResponseType.CannotUpdate => BadRequest(serviceResponse.Message),
                EResponseType.Forbid => Forbid(serviceResponse.Message),
                _ => throw new NotImplementedException()
            };
        }
        [Route("/me")]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult> get()
        {
            var claims = User.Claims.Where(x => x.Type == "UserId").FirstOrDefault();
            string? userid = claims == null ? null : claims.Value.ToString();
            var serviceResponse = await _userServices.GetUserInfoAsync(userid);
            return serviceResponse.ResponseType switch
            {
                EResponseType.Success => Ok(serviceResponse.Data),
                EResponseType.NotFound => BadRequest(serviceResponse.Message),
                EResponseType.Forbid => Forbid(serviceResponse.Message),
                _ => throw new NotImplementedException()
            };
        }
    }
}
