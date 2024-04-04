using DataLayer.DTOs.Role;
using DataLayer.Interfaces;
using DataLayer.Response;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static DataLayer.Response.EServiceResponseTypes;

namespace ExceptionHandler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController:ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRoleServices _roleServices;
        public RoleController(ILogger<RoleController> logger, IRoleServices roleServices) { 
            _logger = logger;
            _roleServices = roleServices;   
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(IList<RolesDTO>), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(UnauthorizedResult), 403)]
        [ProducesResponseType(typeof(UnauthorizedResult), 401)]
        [ProducesResponseType(typeof(ServiceResponse<>), 500)]
        [ProducesResponseType(typeof(ServiceResponse<>), 404)]
        [ProducesResponseType(typeof(ServiceResponse<>), 409)]
        public async Task<ActionResult> getBaseRoles()
        {
            var respoinse = await _roleServices.getAllJobByLevel(1);

            return respoinse.ResponseType switch
            {
                EResponseType.Success => Ok(respoinse.Data),
                EResponseType.CannotUpdate => BadRequest(respoinse.Message),
                EResponseType.Forbid => Forbid(respoinse.Message),
                _ => throw new NotImplementedException()
            };
        }
    }
}
