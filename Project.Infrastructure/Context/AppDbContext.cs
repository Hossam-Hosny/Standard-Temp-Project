using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;

namespace Project.Infrastructure.Context;

internal class AppDbContext(DbContextOptions<AppDbContext>options):IdentityDbContext<User>(options)
{





}
