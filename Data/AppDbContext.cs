using cStudy.Models;
using Microsoft.EntityFrameworkCore;

namespace cStudy.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Post> Posts => Set<Post>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>(entity =>
        {
            entity.ToTable("posts");
            entity.HasKey(post => post.Id);

            entity.Property(post => post.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(post => post.Title)
                .HasColumnName("title")
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(post => post.Content)
                .HasColumnName("content")
                .IsRequired();

            entity.Property(post => post.Author)
                .HasColumnName("author")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(post => post.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            entity.Property(post => post.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired();
        });
    }
}
