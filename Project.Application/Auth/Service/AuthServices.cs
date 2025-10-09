using Project.Application.AppUser.Dtos;
using Project.Application.Auth.Interface;
using Project.Domain.Constants;
using Project.Domain.Dtos;
using Project.Domain.Entities;
using Project.Domain.Exceptions;
using Project.Domain.IRepositories;
using System.IdentityModel.Tokens.Jwt;

namespace Project.Application.Auth.Service;

internal class AuthServices
    (IUserReopsitory _repo, IJwtGenerator _jwtGenerator) : IAuthServices
{
    public async Task<string> AddRoleAsync(AddRoleDto dto)
    {
        var user = await _repo.GetByIdAsync(dto.UserId);
        if (user is null || !await _repo.RoleExist(dto.role))
            return "Invalid user Id or Role!";

        if (await _repo.IsInRoleAsync(user, dto.role))
            return "User already assigned to this role";

       var result = await _repo.AddToRoleAsync(user, dto.role);

        return result.Succeeded ? string.Empty : throw new Exception();

     

      


    }

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
        var refreshToken = _jwtGenerator.GenerateRefreshToken();

        var authModel =  new AuthModel
        {
            UserName = user.UserName,
            Email = user.Email,
           // ExpiresOn = jwtSecurityToken.ValidTo,
            IsAuthenticated = true,
            Roles = new List<string> { "User" },
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            RefreshToken = refreshToken.Token,
            RefreshTokenExpiration = refreshToken.ExpiresOn,

            

        };
        user.RefreshTokens.Add(refreshToken);
        await _jwtGenerator.updateRefreshToken(user);

        return authModel;
    }

    public async Task<AuthModel> GetTokenAsync(LoginRequestDto dto)
    {
        var authModel = new AuthModel();


        var user = await _repo.GetByEmailAsync(dto.Email);
        var correctPassword = await _repo.CheckPassword(user, dto.Password);

        if (user is null || !correctPassword)
        {
            authModel.Message = "Email or Password is incorrect!";
            return authModel;
        }

        var jwtSecurityToken = await _jwtGenerator.CreateJwtToken(user);
        var rolesList = await _repo.GetRolesAsync(user);


        authModel.IsAuthenticated = true;
        authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        authModel.Email = user.Email;
        authModel.UserName = user.UserName;
      //  authModel.ExpiresOn = jwtSecurityToken.ValidTo;
        authModel.Roles = rolesList.ToList() ;

        if (user.RefreshTokens.Any(t => t.IsActive))
        {
            var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
            authModel.RefreshToken = activeRefreshToken.Token;
            authModel.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
        }
        else
        {
            var refreshToken = _jwtGenerator.GenerateRefreshToken();
            authModel.RefreshToken = refreshToken.Token;
            authModel.RefreshTokenExpiration = refreshToken.ExpiresOn;

            user.RefreshTokens.Add(refreshToken);
            await _jwtGenerator.updateRefreshToken(user);
        }



        return authModel;


    }

    public async Task<AuthModel> RefreshTokenAsync(string token)
    {
        var authModel = new AuthModel();

        var user = await _repo.GetByRefreshTokenAsync(token);
        if (user is null)
        {
            authModel.Message = "Invalid token";
            return authModel;
        }

        var refreshToken = user.RefreshTokens.Single(t => t.Token == token);
        if (!refreshToken.IsActive)
        {
            authModel.Message = "Inactive token";
            return authModel;
        }

        refreshToken.RevokedOn = DateTime.UtcNow;
        var newRefreshToken = _jwtGenerator.GenerateRefreshToken();

        user.RefreshTokens.Add(newRefreshToken);
        await _jwtGenerator.updateRefreshToken(user);

        var jwtToken = await _jwtGenerator.CreateJwtToken(user);
        var rolesList = await _repo.GetRolesAsync(user);

        authModel.IsAuthenticated = true;
        authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        authModel.Email = user.Email;
        authModel.UserName = user.UserName;
        authModel.Roles = rolesList.ToList();
        authModel.RefreshToken = newRefreshToken.Token;
        authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;



        return authModel;
    }

    public async Task<bool> RevokeTokenAsync(string token)
    {
        var user = await _repo.GetByRefreshTokenAsync(token);
        if (user is null)
            return false;

        var refreshToken = user.RefreshTokens.Single(t => t.Token == token);
        if (!refreshToken.IsActive)
            return false;

        refreshToken.RevokedOn = DateTime.UtcNow;
        await _jwtGenerator.updateRefreshToken(user);

        return true;


    }
}
