using ScienceArticles.Domain.Entities;
using ScienceArticles.Domain.Interfaces;
using ScienceArticles.Domain.ValueObjects;
using ScienceArticles.Infrastructure.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Infrastructure.Repositories
{
    public class ArticleRepository: IArticleRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task DeleteSavedArticle(Article article)
        {
            _context.Articles.Remove(article);
        }

        public Task<List<Article>> GetUserSavedArticles(UserId userId)
        {
            throw new NotImplementedException();
        }

        public Task SaveArticle(Article article)
        {
            throw new NotImplementedException();
        }
    }
}
