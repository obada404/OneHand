using Microsoft.EntityFrameworkCore;
using OneHandTraining.model;
namespace OneHandTraining.Models;

public class   oneHandContext : DbContext
{
    public DbSet<UserOld> UserOldDBs { get; set; } = null;
    public DbSet<Comment> Comments { get; set; } = null;
    public DbSet<Article> Articles { get; set; } = null;
    public oneHandContext(DbContextOptions<oneHandContext> options) : base(options)
    {
      
         
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)    
    {
        modelBuilder.Entity<UserOld>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id);
            entity.Property(e => e.Email);
            entity.Property(e => e.Password);
            entity.Property(e => e.Username);
            entity.Property(e => e.Bio);
            entity.Property(e => e.Image);

        } );
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id);
            entity.Property(e => e.body);
            entity.Property(e => e.createdAt);

        } );
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id);
            entity.Property(e => e.body);
            entity.Property(e => e.description);
            entity.Property(e => e.favorited);
            entity.Property(e => e.slug);
            entity.Property(e => e.title);
            entity.Property(e => e.createdAt);
            entity.Property(e => e.favoritesCount);
            entity.Property(e => e.updatedAt);

        } );
    }


}