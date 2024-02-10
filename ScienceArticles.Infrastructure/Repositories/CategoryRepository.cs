﻿using Microsoft.EntityFrameworkCore;
using ScienceArticles.Domain.Entities;
using ScienceArticles.Domain.Interfaces;
using ScienceArticles.Infrastructure.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }
    }
}
