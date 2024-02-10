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
        /// <summary>
        /// Saved User Article Id
        /// </summary>
        public string ArticleId { get; set; }
        /// <summary>
        /// Publication Id, coming from external service
        /// </summary>
        public string PublicationId { get; set; }
        public string Title { get; set; }
        public string PublicationYear { get; set; }
        public string Abstract { get; set; }
        public string TextLink { get; set; }
        /// <summary>
        /// UserId, coming from User who created the saved Article
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Category Id, associated with specific Category
        /// </summary>
        public string CategoryId { get; set; }
    }
}
