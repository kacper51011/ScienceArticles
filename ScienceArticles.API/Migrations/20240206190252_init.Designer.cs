﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScienceArticles.Infrastructure.Db;

#nullable disable

namespace ScienceArticles.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240206190252_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ScienceArticles.Domain.Aggregates.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ScienceArticles.Domain.Entities.Article", b =>
                {
                    b.Property<Guid>("ArticleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Abstract")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PublicationId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublicationYear")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TextLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ArticleId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("ScienceArticles.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = new Guid("5e380505-a5bb-41bd-bf13-fb3c21cba2fe"),
                            Name = "Biology"
                        },
                        new
                        {
                            CategoryId = new Guid("17588dac-8db4-4311-be43-b601ddd3959b"),
                            Name = "Chemistry"
                        },
                        new
                        {
                            CategoryId = new Guid("09b043cf-f022-4a75-9de5-3b4a8cd6a774"),
                            Name = "Physics"
                        },
                        new
                        {
                            CategoryId = new Guid("905daa6c-d66a-41bf-bb59-e543c297c252"),
                            Name = "Mathematics"
                        },
                        new
                        {
                            CategoryId = new Guid("7688cc19-bb60-413b-ae59-dcb7ce1753dc"),
                            Name = "Computer Science"
                        },
                        new
                        {
                            CategoryId = new Guid("e0e6c57a-4be5-4b2a-9c1b-630c1628e69a"),
                            Name = "Medicine"
                        },
                        new
                        {
                            CategoryId = new Guid("237e8cd2-a96b-4007-9ef0-82ef619e9c21"),
                            Name = "Economics"
                        },
                        new
                        {
                            CategoryId = new Guid("fe040ac8-1630-4cae-bc56-0aabe4629307"),
                            Name = "Sociology"
                        });
                });

            modelBuilder.Entity("ScienceArticles.Domain.Entities.Article", b =>
                {
                    b.HasOne("ScienceArticles.Domain.Entities.Category", "Category")
                        .WithMany("Articles")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScienceArticles.Domain.Aggregates.User", "User")
                        .WithMany("Articles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ScienceArticles.Domain.Aggregates.User", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("ScienceArticles.Domain.Entities.Category", b =>
                {
                    b.Navigation("Articles");
                });
#pragma warning restore 612, 618
        }
    }
}
