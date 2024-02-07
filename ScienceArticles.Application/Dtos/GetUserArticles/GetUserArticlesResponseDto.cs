using ScienceArticles.Domain.Aggregates;
using ScienceArticles.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Application.Dtos.GetUserArticles
{
    public class GetUserArticlesResponseDto
    {
        public Guid ArticleId { get; set; }
        public string PublicationId { get; set; }
        public string Title { get; set; }
        public string PublicationYear { get; set; }
        public string Abstract { get; set; }
        public string TextLink { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
