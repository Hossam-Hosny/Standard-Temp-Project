using Microsoft.AspNetCore.Identity;

namespace Project.Domain.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public List<RefreshToken>? RefreshTokens { get; set; }
}
