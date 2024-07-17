using Microsoft.AspNetCore.Http;
using Services;

namespace mta.Middleware
{
    public class TenantResolver
    {
        private readonly RequestDelegate _next;

        public TenantResolver(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICurrentTenantService currentTenantService)
        {
            if (context.Request.Headers.TryGetValue("X-Tenant", out var tenantFromHeader))
            {
                await currentTenantService.SetTenant(tenantFromHeader);
            }

            await _next(context);
        }
    }
}
