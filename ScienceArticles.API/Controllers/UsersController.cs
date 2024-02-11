using MediatR;
using Microsoft.AspNetCore.Mvc;
using ScienceArticles.Application.Commands.RegisterUser;
using ScienceArticles.Application.Dtos.Login;
using ScienceArticles.Application.Dtos.Register;
using ScienceArticles.Application.Exceptions;
using ScienceArticles.Application.Queries.GetUserCredentials;

namespace ScienceArticles.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Endpoint for authorizing a user.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Returns JWT Token</returns>
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequestDto dto)
        {
            try
            {
                var query = new GetUserCredentialsQuery(dto);

                var response = await _mediator.Send(query);

                return Ok(response);
            }
            catch (ArgumentNotValidException ex)
            {

                return StatusCode(401, ex.Message);
            }
            catch (Exception)
            {

                throw;
            }


        }

        /// <summary>
        /// Responsible for creating user in database.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            try
            {
                await _mediator.Send(new RegisterUserCommand(dto));

                return Ok();
            }
            catch (ArgumentNotValidException ex)
            {

                return StatusCode(401, ex.Message);
            }
            catch (Exception)
            {

                throw;
            }



        }
    }
}
