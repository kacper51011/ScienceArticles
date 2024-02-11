using ScienceArticles.Domain.Entities;
using ScienceArticles.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetCategories();

        public Task<Category?> GetCategoryById(CategoryId categoryId);
    }
}
