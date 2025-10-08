using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using Project.Domain.IRepositories;
using Project.Infrastructure.Context;

namespace Project.Infrastructure.Repositories;

internal class UserRepository
    (AppDbContext _db, UserManager<User> _userManager, RoleManager<IdentityRole> _roleManager) : IUserReopsitory
{
    public async Task<IdentityResult> AddAsync(User user ,string password)
    {
      var result = await _userManager.CreateAsync(user, password) ;
            await _db.SaveChangesAsync() ;
        return result ;

    }

    public async Task<IdentityResult> AddToRoleAsync(User user, string role)
    {
     return  await _userManager.AddToRoleAsync(user, role);
    }

    public async Task<bool> CheckPassword(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user,password);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<User?> GetByRefreshTokenAsync(string token)
    {
        return await _db.Users.FirstOrDefaultAsync(u=>u.RefreshTokens.Any(t=>t.Token==token));
    }

    public async Task<User?> GetByUserNameAsync(string username)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.UserName == username);

    }

    public async Task<IList<string>> GetRolesAsync(User user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    public async Task<bool> IsInRoleAsync(User user, string role)
    {
        return await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> RoleExist(string role)
    {
        return await _roleManager.RoleExistsAsync(role);
    }
}
