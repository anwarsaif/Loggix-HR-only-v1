using Logix.MVC.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Logix.MVC.HealthEndpoints
{
    public static class HealthEndpoints
    {
        public static void MapHealthEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet($"api/{ApiConfig.ApiVersion}/CheckServerHealth", [AllowAnonymous] () =>
            {
                return Results.Ok(new { success = true });
            });
        }
    }
}
