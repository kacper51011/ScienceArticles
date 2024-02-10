using Microsoft.EntityFrameworkCore;
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
            await _context.SaveChangesAsync();

        }

        public async Task<List<Article>> GetUserSavedArticles(UserId userId)
        {
            var savedArticles = await _context.Articles.Where(a => a.UserId == userId).ToListAsync();

            return savedArticles;
        }

        public async Task SaveArticle(Article article)
        {
            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
        }
    }
}
