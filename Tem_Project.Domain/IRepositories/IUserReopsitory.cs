using Microsoft.AspNetCore.Identity;
using Project.Domain.Entities;

namespace Project.Domain.IRepositories;

public interface IUserReopsitory
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUserNameAsync(string username);
    Task<IdentityResult> AddAsync(User user, string password);
    Task AddToRoleAsync(User user , string role);
    
}
