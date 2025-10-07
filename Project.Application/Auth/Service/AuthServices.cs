using Project.Application.AppUser.Dtos;
using Project.Application.Auth.Interface;
using Project.Domain.Constants;
using Project.Domain.Dtos;
using Project.Domain.Entities;
using Project.Domain.Exceptions;
using Project.Domain.IRepositories;
using System.IdentityModel.Tokens.Jwt;

namespace Project.Application.Auth.Service;

internal class AuthServices(IUserReopsitory _repo , IJwtGenerator _jwtGenerator) : IAuthServices
{
    public async Task<AuthModel> CreateUserAsync(CreateUserDto dto)
    {
        var userEmailexist = await _repo.GetByEmailAsync(dto.Email);
        if (userEmailexist is not null)
            throw new ResourceExistException("UserEmail already exist");

        var userNameexist = await _repo.GetByUserNameAsync(dto.UserName);
        if (userNameexist is not null)
            throw new ResourceExistException("UserName already exist");

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

        var jwtSecurityToken = await _jwtGenerator.CreateJwtToken(user);
        return new AuthModel
        {
            UserName = user.UserName,
            Email = user.Email,
            ExpiresOn = jwtSecurityToken.ValidTo,
            IsAuthenticated = true,
            Roles = new List<string> { "User" },
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
        };


    }
}
