using Microsoft.EntityFrameworkCore;

namespace Project.Infrastructure.Context;

internal class AppDbContext(DbContextOptions<AppDbContext>options):DbContext(options)
{


}
