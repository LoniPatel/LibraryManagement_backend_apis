using LibraryManagement.Application.Interfaces;
//using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Authors;
using LibraryManagement.Domain.Books;
using LibraryManagement.Domain.Categories;
using LibraryManagement.Domain.Users;
using LibraryManagement.Domain.Notifications;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Data;

public class AppDbContext : DbContext, IApplicationDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    
    public DbSet<Author> Authors { get; set; }

    public DbSet<Book> Books { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<User> Users {get; set;}

    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Book>()
        //     .HasOne(b => b.Author)
        //     .WithMany(a => a.Books)
        //     .HasForeignKey(b => b.AuthorId);
        modelBuilder.Entity<Book>()
        .HasMany(b => b.Authors)
        .WithMany(a => a.Books)
        .UsingEntity<Dictionary<string, object>>(   
        "BookAuthors",
        j => j
            .HasOne<Author>()
            .WithMany()
            .HasForeignKey("AuthorId")
            .OnDelete(DeleteBehavior.Cascade),
        j => j
            .HasOne<Book>()
            .WithMany()
            .HasForeignKey("BookId")
            .OnDelete(DeleteBehavior.Cascade));

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Category)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId);

        modelBuilder.Entity<Book>()
            .Property(b => b.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Author>()
        .HasIndex(a => a.Email)
        .IsUnique();    

       

        modelBuilder.Entity<Notification>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Notification>()
            .HasOne<Book>()
            .WithMany()
            .HasForeignKey(n => n.BookId)
            .OnDelete(DeleteBehavior.Restrict);

         base.OnModelCreating(modelBuilder);
    }
}