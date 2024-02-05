using ScienceArticles.Domain.Entities;
using ScienceArticles.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Domain.Aggregates
{
    public class User
    {
        public UserId UserId { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public List<Article> Articles { get; private set; } = new List<Article>();


    }
}
