using Asp.Versioning;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Features.PropertyTypes.Commands.CreatePropertyType;
using FinalProject.Core.Application.Features.PropertyTypes.Commands.DeletePropertyType;
using FinalProject.Core.Application.Features.PropertyTypes.Commands.UpdatePropertyType;
using FinalProject.Core.Application.Features.PropertyTypes.Queries.GetAllPropertyTypes;
using FinalProject.Core.Application.Features.PropertyTypes.Queries.GetPropertyTypesById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProject.Presentation.WebApi.Controllers.v1
{

    [ApiVersion(1.0)]
    [ApiController]
    public class PropertyTypeController : BaseController
    {
        [Authorize(Roles = "Admin,Developer")]
        // GET: api/<PropertyTypeController>
        [HttpGet("GetAllProperties")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<PropertyTypeDto>>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult>  GetAllPropertyType()
        {
            try
            {
                Result<List<PropertyTypeDto>> result = await mediator.Send(new GetAllPropertyTypesQuery());

                if (result.Data.Count == 0)  return NoContent();

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Authorize(Roles = "Admin,Developer")]
        // GET api/<PropertyTypeController>/5
        [HttpGet("GetPropertyTypeById{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<PropertyTypeDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPropertyTypeById(int id)
        {
            try
            {
                Result<PropertyTypeDto> result = await mediator.Send(new GetPropertyTypeByIdQuery { Id = id });

                if(result.Data is null) return NoContent();
                
                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
      
        }
        [Authorize(Roles = "Admin")]
        // POST api/<PropertyTypeController>
        [HttpPost("SavePropertyType")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<SavePropertyTypeDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostPropertyType([FromBody] CreatePropertyTypeCommand command)
        {
            try
            {
                Result<SavePropertyTypeDto> result = await mediator.Send(command);

                if (result.Data is null) return BadRequest();

                return StatusCode(StatusCodes.Status201Created,result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Authorize(Roles = "Admin")]
        // PUT api/<PropertyTypeController>/5
        [HttpPut("UpdatePropertyType{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<UpdatePropertyTypeDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutPropertyType(int id, [FromBody] UpdatePropertyTypeCommand command)
        {
            try
            {
                if(id != command.Id) return BadRequest();

                Result<UpdatePropertyTypeDto> result = await mediator.Send(command);

                if (result.Data is null) return BadRequest();

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Authorize(Roles = "Admin")]
        // DELETE api/<PropertyTypeController>/5
        [HttpDelete("DeletePropertyType{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePropertyType(int id)
        {
            try
            {
                Result result = await mediator.Send(new DeletePropertyTypeCommand { Id = id});

                if(!result.ISuccess) return BadRequest();

                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
