﻿// <auto-generated />
using System;
using BlogManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlogManagement.Data.Migrations
{
    [DbContext(typeof(BlogContext))]
    [Migration("20200416142120_migration2")]
    partial class migration2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlogManagement.Core.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bio");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<byte[]>("ProfileImage");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("BlogManagement.Core.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AuthorId");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BlogManagement.Core.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AuthorId");

                    b.Property<int?>("CategoryId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("BlogManagement.Core.Models.SocialNetworkDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AuthorId");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("SocialNetworkDetails");
                });

            modelBuilder.Entity("BlogManagement.Core.Models.Category", b =>
                {
                    b.HasOne("BlogManagement.Core.Models.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("BlogManagement.Core.Models.Post", b =>
                {
                    b.HasOne("BlogManagement.Core.Models.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("BlogManagement.Core.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("BlogManagement.Core.Models.SocialNetworkDetail", b =>
                {
                    b.HasOne("BlogManagement.Core.Models.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");
                });
#pragma warning restore 612, 618
        }
    }
}
