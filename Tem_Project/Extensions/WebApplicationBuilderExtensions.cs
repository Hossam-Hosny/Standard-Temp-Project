
using Project.API.Sittings;

namespace Project.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {

        builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT"));

    }
}
