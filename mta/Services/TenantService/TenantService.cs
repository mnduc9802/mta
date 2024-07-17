using mta.Models;
using mta.Services.TenantService.DTOs;

namespace mta.Services.TenantService
{
    public class TenantService : ITenantService
    {
        private readonly TenantDbContext _context;

        public TenantService(TenantDbContext context)
        {
            _context = context;
        }

        public Tenant CreateTenant(CreateTenantRequest request)
        {
            var newTenant = new Tenant
            {
                Id = Guid.NewGuid().ToString(), // Tạo GUID mới cho tenant
                Name = request.Name
            };

            _context.Tenants.Add(newTenant);
            _context.SaveChanges();

            return newTenant;
        }
    }
}
