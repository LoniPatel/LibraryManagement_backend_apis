//using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Authors;

namespace LibraryManagement.Application.Interfaces;

public interface IAuthorRepository
{

    Task<IEnumerable<Author>> GetAllAsync();//Get All

    Task<Author?> GetByIdAsync(int id);//Get By Id

    Task<bool> IsEmailExistsAsync(String email);//Check duplicate email create time

    Task<bool> IsEmailExistsForOtherAuthorAsync(string email,int authorId);//Check duplicate email for update time

    Task<bool> IsNameExistsAsync(string name);//Check duplicate name create time

     Task<bool> IsNameExistsForOtherAuthorAsync(string name,int authorId);//Check duplicate name for update time 

     Task AddAsync(Author author);//Add author

    Task UpdateAsync(Author author);//Update author

    Task SaveChangesAsync();//Save changes to database

}