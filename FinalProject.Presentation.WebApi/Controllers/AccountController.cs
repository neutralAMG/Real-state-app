using FinalProject.Core.Application.Features.Account.Commands.RegisterAdminTypeUser;
using FinalProject.Core.Application.Features.Account.Commands.RegisterDeveloperTypeUser;
using FinalProject.Core.Application.Features.Account.Queries.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProject.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

       

        // GET api/<AccountController>/5
        [HttpPost("Authenticate")]
        public async Task<IActionResult> LogIn([FromBody] LoginQuery loginQuery )
        {
            try
            {
             return Ok( await mediator.Send(loginQuery));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<AccountController>
        [HttpPost("RegisterDeveloper")]
        public async Task<IActionResult> RegisterDeveloper([FromBody] RegisterDeveloperTypeUserCommand registerDeveloperTypeUserCommand)
        {
            try
            {
                return Ok(await mediator.Send(registerDeveloperTypeUserCommand));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize(Roles = "Admin")]
        // PUT api/<AccountController>/5
        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin( [FromBody] RegisterAdminTypeUserCommand registerAdminTypeUserCommand)
        {
            try
            {
              return  Ok(await mediator.Send(registerAdminTypeUserCommand));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

     
    }
}
