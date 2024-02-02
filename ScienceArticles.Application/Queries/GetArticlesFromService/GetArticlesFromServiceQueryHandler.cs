using MediatR;
using ScienceArticles.Application.Dtos.SearchPublications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Application.Queries.GetArticlesFromService
{
    public class GetArticlesFromServiceQueryHandler : IRequestHandler<GetArticlesFromServiceQuery, List<SearchPublicationsResponseItemDto>>
    {
        public async Task<List<SearchPublicationsResponseItemDto>> Handle(GetArticlesFromServiceQuery request, CancellationToken cancellationToken)
        {

            return new List<SearchPublicationsResponseItemDto>();
        }
    }
}
