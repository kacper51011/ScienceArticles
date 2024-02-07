using MediatR;
using Microsoft.AspNetCore.Http;
using ScienceArticles.Application.Dtos.GetUserArticles;
using ScienceArticles.Domain.Interfaces;
using ScienceArticles.Domain.ValueObjects;

namespace ScienceArticles.Application.Queries.GetSavedUserArticles
{
    public class GetSavedUserArticlesQueryHandler : IRequestHandler<GetSavedUserArticlesQuery, List<GetUserArticlesResponseDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetSavedUserArticlesQueryHandler(IArticleRepository articleRepository, IHttpContextAccessor httpContextAccessor)
        {
            _articleRepository = articleRepository;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<List<GetUserArticlesResponseDto>> Handle(GetSavedUserArticlesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
                if (userId == null)
                {
                    throw new ArgumentNullException("UserId not specified");
                }

                var articlesDto = new List<GetUserArticlesResponseDto>();

                var response = await _articleRepository.GetUserSavedArticles(new UserId(Guid.Parse(userId)));

                if (response != null && response.Count != 0)
                {
                    foreach (var article in response)
                    {
                        var articleDto = new GetUserArticlesResponseDto();
                        articleDto.Abstract = article.Abstract;
                        articleDto.PublicationId = article.PublicationId;
                        articleDto.TextLink = article.TextLink;
                        articleDto.PublicationYear = article.PublicationYear;
                        articleDto.UserId = article.UserId.Value;
                        articleDto.CategoryId = article.CategoryId.Value;
                        articleDto.ArticleId = article.ArticleId.Value;
                        articleDto.Title = article.Title;

                        articlesDto.Add(articleDto);

                    }

                }



                return articlesDto;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
