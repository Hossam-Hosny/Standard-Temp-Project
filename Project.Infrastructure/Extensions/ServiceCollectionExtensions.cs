using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Project.Domain.Entities;
using Project.Domain.IRepositories;
using Project.Infrastructure.Context;
using Project.Infrastructure.Repositories;
using Project.Infrastructure.Sittings;
using System.Configuration;
using System.Text;

namespace Project.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services,IConfiguration config)
    {
        var connectionString = config.GetConnectionString("LocalConnectionString");
        services.Configure<JwtOptions>(config.GetSection("JWT"));



        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging();
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = true;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidIssuer = config["JWT:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = config["JWT:Audience"],

                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"])),

                    ClockSkew = TimeSpan.Zero

                };

            });

        services.AddScoped<IUserReopsitory, UserRepository>();
        services.AddScoped<IJwtGenerator, JwtGenerator>();

    }
}
