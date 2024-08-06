using Asp.Versioning;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Features.SellTypes.Comands.CreateSellType;
using FinalProject.Core.Application.Features.SellTypes.Comands.DeleteSellType;
using FinalProject.Core.Application.Features.SellTypes.Comands.UpdateSellType;
using FinalProject.Core.Application.Features.SellTypes.Queries.GetAllSellTypes;
using FinalProject.Core.Application.Features.SellTypes.Queries.GetSellTypeById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProject.Presentation.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiVersion(1.0)]
    [ApiController]
    [SwaggerTag("Sales types mangement")]
    public class SellTypeController : BaseController
    {
        [Authorize(Roles = "Admin,Developer")]
        // GET: api/<SellTypeController>
        [HttpGet("GetAllSellTypes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<SellTypeDto>>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "List of Sales type's",
            Description = "Obtains a list of Sale types registered in the application"
		)]
        public async Task<IActionResult> GetAllPropertyType()
        {
            try
            {
                Result<List<SellTypeDto>> result = await mediator.Send(new GetAllSellTypesQuery());

                if (result.Data.Count == 0) return NoContent();

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Roles = "Admin,Developer")]
        // GET api/<SellTypeController>/5
        [HttpGet("GetSellTypeById{id}")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<SellTypeDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[SwaggerOperation(
			Summary = "Get's a Sale type by id",
			Description = "Get's an specific Sale type by its id"
		)]
		public async Task<IActionResult> GetPropertyTypeById(int id)
        {
            try
            {
                Result<SellTypeDto> result = await mediator.Send(new GetSellTypeByIdQuery { Id = id });

                if (result.Data is null) return NoContent();

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [Authorize(Roles = "Admin")]
        // POST api/<SellTypeController>
        [HttpPost("SaveSellType")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<SaveSellTypeDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[SwaggerOperation(
			Summary = "Saves a Sale type",
			Description = "Saves a new Sale type to the database with the provided data"
		)]
		public async Task<IActionResult> PostPropertyType([FromBody] CreateSellTypeCommand command)
        {
            try
            {
                Result<SaveSellTypeDto> result = await mediator.Send(command);

                if (result.Data is null) return BadRequest();

                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Roles = "Admin")]
        // PUT api/<SellTypeController>/5
        [HttpPut("UpdateSellType{id}")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<UpdateSellTypeDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[SwaggerOperation(
			Summary = "Updates a Sale type",
			Description = "Updates a specific Sale type by it's id with the provided data"
		)]
		public async Task<IActionResult> PutPropertyType(int id, [FromBody] UpdateSellTypeCommand command)
        {
            try
            {
                if (id != command.Id) return BadRequest();

                Result<UpdateSellTypeDto> result = await mediator.Send(command);

                if (result.Data is null) return BadRequest();

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Roles = "Admin")]
        // DELETE api/<SellTypeController>/5
        [HttpDelete("DeleteSellTypeType{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[SwaggerOperation(
			Summary = "Deletes a Sale type",
			Description = "Deletes a specific Sale type by it's id"
		)]
		public async Task<IActionResult> DeletePropertyType(int id)
        {
            try
            {
                Result result = await mediator.Send(new DeleteSellTypeCommand { Id = id });

                if (!result.ISuccess) return BadRequest();

                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
