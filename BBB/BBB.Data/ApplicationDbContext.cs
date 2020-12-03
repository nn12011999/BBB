using BBB.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBB.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostTag>().HasKey(sc => new { sc.TagId, sc.PostId });

            modelBuilder.Entity<PostTag>()
                .HasOne<Post>(sc => sc.post)
                .WithMany(s => s.PostTags)
                .HasForeignKey(sc => sc.PostId);


            modelBuilder.Entity<PostTag>()
                .HasOne<Tag>(sc => sc.tag)
                .WithMany(s => s.PostTags)
                .HasForeignKey(sc => sc.TagId);
        }
    }
}
