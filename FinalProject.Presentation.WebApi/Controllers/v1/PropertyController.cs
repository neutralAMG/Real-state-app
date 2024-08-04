using Asp.Versioning;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Features.Properties.Queries.GetAllProperties;
using FinalProject.Core.Application.Features.Properties.Queries.GetPropertyByCode;
using FinalProject.Core.Application.Features.Properties.Queries.GetPropertyById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProject.Presentation.WebApi.Controllers.v1
{
    [Authorize(Roles = "Admin,Developer")]
    [ApiVersion(1.0)]
    [ApiController]

    public class PropertyController : BaseController
    {
        // GET: api/<PropertyController>
        [HttpGet("GetAllProperties")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<PropertyDto>>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                Result<List<PropertyDto>> result = await mediator.Send(new GetAllPropertiesQuery());

                if (result.Data.Count == 0)
                {
                    return NoContent();
                }

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // GET api/<PropertyController>/5
        [HttpGet("GetPropertyById{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<PropertyDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                Result<PropertyDto> result = await mediator.Send(new GetPropertyByIdQuery { Id = id });

                if(result.Data is null)
                {
                    return NoContent();
                }

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET api/<PropertyController>/5
        [HttpGet("GetByPropertyCode")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<PropertyDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByPropertyCode(string code)
        {
            try
            {
                Result<PropertyDto> result = await mediator.Send(new GetPropertyByCodeQuery { code = code});

                if(result.Data is null)
                {
                    return NoContent();
                }
                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
     
    }
}
