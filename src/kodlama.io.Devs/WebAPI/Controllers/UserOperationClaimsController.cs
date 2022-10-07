using Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim;
using Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Models;
using Application.Features.UserOperationClaims.Queries.GetListByUserIdUserOperationClaim;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]CreateUserOperationClaimCommand createUserOperationClaimCommand)
        {
            CreatedUserOperationClaimDto result = await Mediator.Send(createUserOperationClaimCommand);

            return Created("", result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromQuery] DeleteUserOperationClaimCommand deleteUserOperationClaimCommand)
        {
            DeletedUserOperationClaimDto result = await Mediator.Send(deleteUserOperationClaimCommand);

            return Ok(result);
        }

        [HttpGet("getlist/byuserid")]
        public async Task<IActionResult> GetByUserId([FromQuery] int userId, [FromQuery]PageRequest pageRequest)
        {
            GetListByUserIdUserOperationClaimQuery getListByUserIdUserOperationClaimQuery = new() {  UserId=userId, PageRequest = pageRequest };
            UserOperationClaimListModel result = await Mediator.Send(getListByUserIdUserOperationClaimQuery);

            return Ok(result);
        }
    }
}
