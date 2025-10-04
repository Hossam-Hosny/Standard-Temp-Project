using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Domain.Entities;
using Project.Infrastructure.Context;

namespace Project.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services,IConfiguration config)
    {
        var connectionString = config.GetConnectionString("LocalConnectionString");

        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging();
        });



    }
}
