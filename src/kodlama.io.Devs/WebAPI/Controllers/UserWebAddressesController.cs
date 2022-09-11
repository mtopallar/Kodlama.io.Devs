using Application.Features.UserWebAddresses.Commands.CreateUserWebAddress;
using Application.Features.UserWebAddresses.Commands.DeleteUserWebAddress;
using Application.Features.UserWebAddresses.Commands.UpdateUserWebAddress;
using Application.Features.UserWebAddresses.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWebAddressesController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateUserWebAddressCommand createUserWebAddressCommand)
        {
            CreatedUserWebAddressDto result = await Mediator.Send(createUserWebAddressCommand);

            return Created("", result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(UpdateUserWebAddressCommand updateUserWebAddressCommand)
        {
            UpdatedUserWebAdressDto result = await Mediator.Send(updateUserWebAddressCommand);

            return Ok(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromQuery] DeleteUserWebAddressCommand deleteUserWebAddressCommand)
        {
            DeletedUserWebAddressDto result = await Mediator.Send(deleteUserWebAddressCommand);

            return Ok(result);
        }
    }
}
