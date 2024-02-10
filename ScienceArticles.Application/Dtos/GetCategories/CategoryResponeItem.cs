using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Application.Dtos.GetCategories
{
    public class CategoryResponeItem
    {
        /// <summary>
        /// Category Id, which can be used when creating User Article
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Name of the category
        /// </summary>
        public string Name { get; set; }
    }
}
