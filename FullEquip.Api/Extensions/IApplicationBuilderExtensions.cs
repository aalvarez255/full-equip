using FullEquip.Api.Middleware;
using Microsoft.AspNetCore.Builder;

namespace FullEquip.Api.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApiException(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ApiExceptionMiddleware>();
        }
    }
}
