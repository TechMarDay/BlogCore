using BlogCore.Entities;
using BlogCore.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.Data
{
    public class BlogCoreContext : DbContext
    {
        public BlogCoreContext(DbContextOptions<BlogCoreContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasIndex(e => e.CategoryId);

                entity.Property(e => e.Title).IsRequired();

                entity.Property(e => e.Content).IsRequired();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CategoryId);
            });
        }

        public DbSet<BlogCore.Entities.User> User { get; set; }
    }
}