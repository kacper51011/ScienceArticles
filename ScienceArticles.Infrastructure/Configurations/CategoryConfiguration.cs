using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScienceArticles.Domain.Entities;
using ScienceArticles.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.HasKey(c => c.CategoryId);

            entity.Property(x => x.CategoryId).HasConversion(id => id.Value, value => new CategoryId(value));


            entity.HasMany(x => x.Articles).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);

            entity.HasData(
                Category.Create("Biology"),
                Category.Create("Chemistry"),
                Category.Create("Physics"),
                Category.Create("Mathematics"),
                Category.Create("Computer Science"),
                Category.Create("Medicine"),
                Category.Create("Economics"),
                Category.Create("Sociology")
                );
        }
    }
}
