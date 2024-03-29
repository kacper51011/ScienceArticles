﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.WsTrust;
using ScienceArticles.Application.Dtos.GetUserArticles;
using ScienceArticles.Application.Exceptions;
using ScienceArticles.Domain.Interfaces;
using ScienceArticles.Domain.ValueObjects;
using System.Security.Claims;

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
                var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    throw new UnauthorizedException("UserId not specified");
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
                        articleDto.UserId = article.UserId.Value.ToString();
                        articleDto.CategoryId = article.CategoryId.Value.ToString();
                        articleDto.ArticleId = article.ArticleId.Value.ToString();
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
