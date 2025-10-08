using Microsoft.AspNetCore.Identity;
using Project.Domain.Entities;

namespace Project.Domain.IRepositories;

public interface IUserReopsitory
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUserNameAsync(string username);
    Task<User?> GetByIdAsync(string id);
    Task<IdentityResult> AddAsync(User user, string password);
    Task<IdentityResult> AddToRoleAsync(User user , string role);
    Task<bool> CheckPassword(User user, string password);
    Task<IList<string>> GetRolesAsync(User user);
    Task<bool> RoleExist(string role);
    Task<bool> IsInRoleAsync(User user, string role);
    
}
