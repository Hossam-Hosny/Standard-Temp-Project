using Project.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Project.Domain.IRepositories;

public interface IJwtGenerator
{
    Task<JwtSecurityToken> CreateJwtToken(User user);
    RefreshToken GenerateRefreshToken();
    Task updateRefreshToken( User user );
}
