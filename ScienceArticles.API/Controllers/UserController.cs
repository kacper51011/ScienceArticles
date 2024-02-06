﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using ScienceArticles.Application.Commands.RegisterUser;
using ScienceArticles.Application.Dtos.Login;
using ScienceArticles.Application.Dtos.Register;
using ScienceArticles.Application.Queries.GetUserCredentials;

namespace ScienceArticles.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequestDto dto)
        {
            try
            {
                var query = new GetUserCredentialsQuery(dto);

                var response = await _mediator.Send(query);

                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }


        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            try
            {
                await _mediator.Send(new RegisterUserCommand(dto));

                return Ok();
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
