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
        /// <summary>
        /// Publication Id, coming from external service - EuropePMC
        /// </summary>
        public string PublicationId { get; set; }
        /// <summary>
        /// Category Id, needed for binding the UserArticle with specific Category
        /// </summary>
        public string CategoryId { get; set; }
    }
}
