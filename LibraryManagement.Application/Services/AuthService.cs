using LibraryManagement.Application.DTOs.Auth;
using LibraryManagement.Application.Interfaces;
//using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagement.Application.Services;

public class AuthService : IAuthService
{
    private readonly IApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthService(
        IApplicationDbContext context,
        IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
        _passwordHasher = new PasswordHasher<User>();
    }
 
         public async Task<RegisterResponseDto> RegisterAsync(RegisterDto dto)
    {
         var emailExists = await _context.Users
    .AnyAsync(u => u.Email == dto.Email);

         if (emailExists)
        {
            throw new InvalidOperationException("Email already exists.");
        }
        if(!Enum.IsDefined(typeof(UserRole), dto.Role))
        {
            throw new InvalidOperationException("Invalid User Role.");
        }
        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Role = dto.Role,
            CreatedAt = DateTime.Now
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return new RegisterResponseDto
        {
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }   

    public async Task<LoginResponseDto?> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if(user == null)
        {
            return null;
        }
        var passwordResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if(passwordResult == PasswordVerificationResult.Failed)
        {
            return null;
        }
        var token = GenerateToken(user);
        return new LoginResponseDto
        {
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            Token = token
        };
    }


    private string GenerateToken(User user)
    {
        var key = _configuration["Jwt:Key"];

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key!));

        var credentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()),

            new Claim(
                ClaimTypes.Name,
                user.Name),

            new Claim(
                ClaimTypes.Email,
                user.Email),

            new Claim(
                ClaimTypes.Role,
                user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(
                    _configuration["Jwt:ExpiryMinutes"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}

