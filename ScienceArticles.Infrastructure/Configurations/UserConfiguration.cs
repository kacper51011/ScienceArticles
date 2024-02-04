using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScienceArticles.Domain.Aggregates;
using ScienceArticles.Domain.Entities;
using ScienceArticles.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceArticles.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(x => x.UserId);

            entity.Property(x => x.UserId).HasConversion(id => id.Value, value => new UserId(value));

            entity.HasMany(u => u.Articles)
                .WithOne( a => a.User).HasForeignKey(a => a.UserId);


        }
    }
}
