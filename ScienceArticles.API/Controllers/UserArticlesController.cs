using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScienceArticles.Application.Dtos.GetUserArticles;
using ScienceArticles.Application.Dtos.SaveUserArticle;

namespace ScienceArticles.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserArticlesController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<GetUserArticlesResponseDto>>> GetUserArticles()
        {
            try
            {
              
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]

        public async Task<ActionResult> SaveUserArticle(SaveUserArticleRequestDto dto)
        {

        }
    }
}
