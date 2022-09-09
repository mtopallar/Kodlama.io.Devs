using Application.Features.SubTechnologies.Commands.CreateSubTechnology;
using Application.Features.SubTechnologies.Dtos;
using Application.Features.SubTechnologies.Models;
using Application.Features.SubTechnologies.Queries.GetListSubTechnology;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubTechnologiesController : BaseController
    {
        [HttpGet("getlist")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListSubTechnologyQuery getListSubTechnologyQuery = new GetListSubTechnologyQuery { PageRequest = pageRequest };
            SubTechnologyListModel result = await Mediator.Send(getListSubTechnologyQuery);

            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateSubTechnologyCommand createSubTechnologyCommand)
        {
           CreatedSubTechnologyDto result = await Mediator.Send(createSubTechnologyCommand);
            return Created("", result);
        }
    }
}
