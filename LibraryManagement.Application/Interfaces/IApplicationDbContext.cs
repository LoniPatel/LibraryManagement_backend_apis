//using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Authors;
using LibraryManagement.Domain.Books;
using LibraryManagement.Domain.Categories;
using LibraryManagement.Domain.Users;
using LibraryManagement.Domain.Notifications;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Author> Authors { get; }

    DbSet<Book> Books { get; }

    DbSet<Category> Categories { get; }

    DbSet<User> Users { get; }

    DbSet<Notification> Notifications { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}