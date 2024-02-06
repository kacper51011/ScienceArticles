using ScienceArticles.Domain.Entities;
using ScienceArticles.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Domain.Interfaces
{
    public interface IArticleRepository
    {
        public Task<List<Article>> GetUserSavedArticles(UserId userId);
        public Task DeleteSavedArticle(Article article);

        public Task SaveArticle(Article article);
    }
}
