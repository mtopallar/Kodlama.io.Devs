using Application.Features.SubTechnologies.Commands.CreateSubTechnology;
using Application.Features.SubTechnologies.Commands.DeleteSubTechnology;
using Application.Features.SubTechnologies.Commands.UpdateSubTechnology;
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

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateSubTechnologyCommand updateSubTechnologyCommand)
        {
            UpdatedSubTechnologyDto result = await Mediator.Send(updateSubTechnologyCommand);

            return Ok(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromQuery] DeleteSubTechnologyCommand deleteSubTechnologyCommand)
        {
            DeletedSubTechnologyDto result = await Mediator.Send(deleteSubTechnologyCommand);

            return Ok(result);
        }
    }
}
