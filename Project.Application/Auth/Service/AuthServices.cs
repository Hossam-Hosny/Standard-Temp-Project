using Microsoft.JSInterop.Infrastructure;
using Project.Application.AppUser.Dtos;
using Project.Application.Auth.Interface;
using Project.Domain.Constants;
using Project.Domain.Dtos;
using Project.Domain.Entities;
using Project.Domain.IRepositories;

namespace Project.Application.Auth.Service;

internal class AuthServices(IUserReopsitory _repo) : IAuthServices
{
    public async Task<AuthModel> CreateUserAsync(CreateUserDto dto)
    {
        var userEmailexist = await _repo.GetByEmailAsync(dto.Email);
        if (userEmailexist is not null)
            throw new Exception("UserEmail already exist");

        var userNameexist = await _repo.GetByUserNameAsync(dto.UserName);
        if (userNameexist is not null)
            throw new Exception("UserName already exist");

        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserName = dto.UserName,
            Email = dto.Email
            
        };
        var result = await _repo.AddAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            var errors = string.Empty;
            foreach (var error in result.Errors)
            {
                errors += $"{error.Description}, ";
            }
            return new AuthModel { Message = errors };

        }
      await  _repo.AddToRoleAsync(user, UserRoles.User);




    }
}
