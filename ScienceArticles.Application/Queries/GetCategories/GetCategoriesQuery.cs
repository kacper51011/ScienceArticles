using MediatR;
using ScienceArticles.Application.Dtos.GetCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Application.Queries.GetCategories
{
    public record GetCategoriesQuery() : IRequest<List<CategoryResponeItem>>
    {
    }
}
