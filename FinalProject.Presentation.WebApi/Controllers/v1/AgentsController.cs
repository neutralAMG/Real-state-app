using Asp.Versioning;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Features.Agents.Commands.ChangeAgentStatus;
using FinalProject.Core.Application.Features.Agents.Queries.GetAgentById;
using FinalProject.Core.Application.Features.Agents.Queries.GetAgentProperties;
using FinalProject.Core.Application.Features.Agents.Queries.GetAllAgents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProject.Presentation.WebApi.Controllers.v1
{
    [ApiVersion(1.0)]
    [ApiController]
	[SwaggerTag("Agent mangements")]
	public class AgentsController : BaseController
    {
        [Authorize(Roles = "Admin,Developer")]
        // GET: api/<AgentsController>
        [HttpGet("GetAllAgents")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<UserDto>>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[SwaggerOperation(
			Summary = "List of user agent's",
			Description = "Obtains a list of agent's registered in the the application"
		)]
		public async Task<IActionResult> GetAllAgents()
        {
            try
            {
                Result<List<UserDto>> result = await mediator.Send(new GetAllAgentsQuery());

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

        [Authorize(Roles = "Admin,Developer")]
        // GET api/<AgentsController>/5
        [HttpGet("GetAgentById{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<UserDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[SwaggerOperation(
			Summary = "Get's a user agent by id",
			Description = "Get's an user agent by it's id"
		)]
		public async Task<IActionResult> GetAgentById(string id)
        {
            
            try
            {
                Result<UserDto> result = await mediator.Send(new GetAgentByIdQuery { Id = id });

                if (result.Data is null)
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

        [Authorize(Roles = "Admin,Developer")]
        // GET api/<AgentsController>/5
        [HttpGet("GetAllAgentsPropertiesByAgentId{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<PropertyDto>>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[SwaggerOperation(
			Summary = "Get's a agent list of properties ",
			Description = "Get's a list of properties registered under a user agent, by it's id"
		)]
		public async Task<IActionResult> GetAllAgentsProperties(string id)
        {
            try
            {
                Result<List<PropertyDto>> result = await mediator.Send(new GetAgentsPropertiesQuery { Id = id });

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

        [Authorize(Roles = "Admin")]
        // PUT api/<AgentsController>/5
        [HttpPut("ChangeStatus{id}")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[SwaggerOperation(
			Summary = "Changes the state of an agent",
			Description = "Set's the user state to the specified state pass in the parameters"
		)]
		public async Task<IActionResult> ChangeStatus(string id, [FromBody] ChangeAgentStatusCommand command)
        {
            try
            {
                command.Id = id;    
                Result result = await mediator.Send(command);

                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

      
    }
}
