using BlogManagement.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogManagement.Data
{
    public partial class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
        }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=CIPL-8PC164\SQLEXPRESS;initial catalog=Blogs;integrated security=True;MultipleActiveResultSets=True;");
        }
    }
}
