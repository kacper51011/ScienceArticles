using MediatR;
using ScienceArticles.Application.Dtos.CreateSavedUserArticle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Application.Commands.CreateSavedUserArticle
{
    public record CreateSavedUserArticleCommand(CreateSavedUserRequestDto dto): IRequest
    {
    }
}
