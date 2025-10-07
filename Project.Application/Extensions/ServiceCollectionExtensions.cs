using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Auth.Interface;
using Project.Application.Auth.Service;

namespace Project.Application.Extensions;

public static  class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssempley = typeof(ServiceCollectionExtensions).Assembly;


        services.AddValidatorsFromAssembly(applicationAssempley)
            .AddFluentValidationAutoValidation();
        services.AddScoped<IAuthServices, AuthServices>();

    }
}
