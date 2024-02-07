using MediatR;
using ScienceArticles.Application.Dtos.GetUserArticles;
using ScienceArticles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Application.Queries.GetSavedUserArticles
{
    public record GetSavedUserArticlesQuery: IRequest<List<GetUserArticlesResponseDto>>
    {
    }
}
