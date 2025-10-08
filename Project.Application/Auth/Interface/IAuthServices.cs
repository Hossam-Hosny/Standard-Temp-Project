using Project.Application.AppUser.Dtos;
using Project.Domain.Dtos;

namespace Project.Application.Auth.Interface;

public interface IAuthServices
{
    Task<AuthModel> CreateUserAsync(CreateUserDto dto);
    Task<AuthModel> GetTokenAsync(LoginRequestDto dto);
    Task<string> AddRoleAsync(AddRoleDto dto);
    Task<AuthModel> RefreshTokenAsync(string token);
}
