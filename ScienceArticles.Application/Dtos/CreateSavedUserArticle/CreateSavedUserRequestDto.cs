using ScienceArticles.Domain.Aggregates;
using ScienceArticles.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Application.Dtos.CreateSavedUserArticle
{
    public class CreateSavedUserRequestDto
    {
        public string PublicationId { get; set; }
        public string CategoryId { get; set; }
    }
}
