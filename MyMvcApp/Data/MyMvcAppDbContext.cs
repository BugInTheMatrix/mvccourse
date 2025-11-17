using System;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Models.Domain;

namespace MyMvcApp.Data
{
    public class MyMvcAppDbContext : DbContext
    {
        public MyMvcAppDbContext(DbContextOptions<MyMvcAppDbContext> options) : base(options)
        {

        }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPostLike> BlogPostLikes { get; set; }
        public DbSet<BlogPostComment> BlogPostComments { get; set; }
    }
}


