using mta.Models;
using mta.Services.TenantService.DTOs;
using System.Linq;

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
            var existingTenant = _context.Tenants.FirstOrDefault(t => t.Id == request.Id);
            if (existingTenant != null)
            {
                throw new InvalidOperationException("Tenant already exists.");
            }

            var newTenant = new Tenant
            {
                Id = request.Id,
                Name = request.Name
            };

            _context.Tenants.Add(newTenant);
            _context.SaveChanges();

            return newTenant;
        }
    }
}
