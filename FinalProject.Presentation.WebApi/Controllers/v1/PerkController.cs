using Asp.Versioning;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Features.Perks.Commands.CreatePerk;
using FinalProject.Core.Application.Features.Perks.Commands.DeletePerk;
using FinalProject.Core.Application.Features.Perks.Commands.UpdatePerk;
using FinalProject.Core.Application.Features.Perks.Querries.GetAllPerks;
using FinalProject.Core.Application.Features.Perks.Querries.GetPerkById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProject.Presentation.WebApi.Controllers.v1
{
  
    [ApiVersion(1.0)]
    [ApiController]
	[SwaggerTag("Perks mangements")]
	public class PerkController : BaseController
    {
        [Authorize(Roles = "Admin,Developer")]
        // GET: api/<PerkController>
        [HttpGet("GetPerks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<PerkDto>>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[SwaggerOperation(
			Summary = "Get's List of perk's types",
			Description = "Obtains a list of perk's registered in  the application"
		)]
		public async Task<IActionResult> GetAllPerk()
        {
            try
            {
                Result<List<PerkDto>> result = await mediator.Send(new GetAllPerksQuery());

                if (result.Data.Count == 0) return NoContent();

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Roles = "Admin,Developer")]
        // GET api/<PropertyTypeController>/5
        [HttpGet("GetPerkById{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<PerkDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[SwaggerOperation(
			Summary = "Get's Perk type by id",
			Description = "Get's an specific perk by it's id"
		)]
		public async Task<IActionResult> GetPerkById(int id)
        {
            try
            {
                Result<PerkDto> result = await mediator.Send(new GetPerkByIdQuery { Id = id });

                if (result.Data is null) return NoContent();

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [Authorize(Roles = "Admin")]
        // POST api/<PropertyTypeController>
        [HttpPost("SavePerk")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<SavePerkDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[SwaggerOperation(
			Summary = "Saves a perk",
			Description = "Saves a new perk type to the database with the provided data"
		)]
		public async Task<IActionResult> PostPerk([FromBody] CreatePerkCommand command)
        {
            try
            {
                Result<SavePerkDto> result = await mediator.Send(command);

                if (result.Data is null) return BadRequest();

                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Roles = "Admin")]
        // PUT api/<PropertyTypeController>/5
        [HttpPut("UpdatePerk{id}")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<UpdatePerkDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[SwaggerOperation(
			Summary = "Updates a perk",
			Description = "Updates a specific perk by it's id with the provided data"
		)]
		public async Task<IActionResult> PutPerk(int id, [FromBody] UpdatePerkCommand command)
        {
            try
            {
                if (id != command.Id) return BadRequest();

                Result<UpdatePerkDto> result = await mediator.Send(command);

                if (result.Data is null) return BadRequest();

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Authorize(Roles = "Admin")]
        // DELETE api/<PerkController>/5
        [HttpDelete("DeletePerk{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[SwaggerOperation(
			Summary = "Deletes a perk",
			Description = "Deletes a specific perk by it's id"
		)]
		public async Task<IActionResult> DeletePerk(int id)
        {
            try
            {
                Result result = await mediator.Send(new DeletePerkCommand { Id = id });

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
