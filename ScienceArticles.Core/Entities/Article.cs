﻿using ScienceArticles.Domain.Aggregates;
using ScienceArticles.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Domain.Entities
{
    public class Article
    {
        public ArticleId ArticleId { get; private set; }
        public string PublicationId { get; set; }
        public string Title { get; set; }
        public string PublicationYear { get; set; }
        public string Abstract { get; set; }
        public string TextLink { get; set; }
        public UserId UserId { get; private set; }
        public User User {  get; private set; }
        public CategoryId CategoryId { get; private set; }
        public Category Category { get; private set; }
    }
}
