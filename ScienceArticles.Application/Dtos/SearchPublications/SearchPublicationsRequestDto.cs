using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Application.Dtos.SearchPublications
{
    public class SearchPublicationsRequestDto
    {
        /// <summary>
        /// The phrase we want to search for in EuropePMCService
        /// </summary>
        [Required]
        public string Query { get; set; }
        /// <summary>
        /// Number of items in response list, default value equals 50
        /// </summary>
        public int PageSize { get; set; } = 50;
        /// <summary>
        /// Do User want to include synonyms of entered phrase, default value equals true
        /// </summary>
        public bool SynonymsIncluded { get; set; } = true;
    }
}
