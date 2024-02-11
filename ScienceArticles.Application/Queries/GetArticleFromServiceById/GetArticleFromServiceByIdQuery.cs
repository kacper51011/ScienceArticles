using MediatR;
using ScienceArticles.Application.Dtos.SearchPublications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Application.Queries.GetArticleFromServiceById
{
    public record GetArticleFromServiceByIdQuery(string publicationId): IRequest<SearchPublicationsResponseItemDto>
    {
    }
}
