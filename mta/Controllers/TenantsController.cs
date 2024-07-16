using Microsoft.AspNetCore.Mvc;
using mta.Services.TenantService;
using mta.Services.TenantService.DTOs;

namespace mta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantsController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpPost]
        public IActionResult Post(CreateTenantRequest request)
        {
            try
            {
                var tenant = _tenantService.CreateTenant(request);
                return CreatedAtAction(nameof(Post), new { id = tenant.Id }, tenant);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
