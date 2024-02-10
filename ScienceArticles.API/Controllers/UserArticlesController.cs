using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScienceArticles.Application.Commands.CreateSavedUserArticle;
using ScienceArticles.Application.Commands.DeleteSavedUserArticle;
using ScienceArticles.Application.Dtos.CreateSavedUserArticle;
using ScienceArticles.Application.Dtos.GetUserArticles;
using ScienceArticles.Application.Queries.GetSavedUserArticles;

namespace ScienceArticles.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserArticlesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserArticlesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetUserArticlesResponseDto>>> GetUserArticles()
        {
            try
            {
                var query = new GetSavedUserArticlesQuery();
                var response = await _mediator.Send(query);

                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public async Task<ActionResult> SaveUserArticle(CreateSavedUserRequestDto dto)
        {
            try
            {
                var command = new CreateSavedUserArticleCommand(dto);
                await _mediator.Send(command);

                return Ok();
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpDelete]

        public async Task<ActionResult> DeleteUserArticle(string articleId)
        {
            try
            {
                try
                {
                    var command = new DeleteSavedUserArticleCommand(articleId);
                    await _mediator.Send(command);

                    return Ok();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
