using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using Project.Domain.IRepositories;
using Project.Infrastructure.Context;

namespace Project.Infrastructure.Repositories;

internal class UserRepository(AppDbContext _db, UserManager<User> _userManager) : IUserReopsitory
{
    public async Task<IdentityResult> AddAsync(User user ,string password)
    {
      var result = await _userManager.CreateAsync(user, password) ;
            await _db.SaveChangesAsync() ;
        return result ;

    }

    public async Task AddToRoleAsync(User user, string role)
    {
       await _userManager.AddToRoleAsync(user, role);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByUserNameAsync(string username)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.UserName == username);

    }

  
}
