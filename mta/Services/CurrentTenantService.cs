using Microsoft.EntityFrameworkCore;
using mta.Models;
using Services;
using System.Threading.Tasks;

namespace mta.Services
{
    public class CurrentTenantService : ICurrentTenantService
    {
        private readonly TenantDbContext _context;

        public CurrentTenantService(TenantDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SetTenant(string tenantName)
        {
            var tenantInfo = await _context.Tenants.FirstOrDefaultAsync(x => x.Name == tenantName);
            if (tenantInfo != null)
            {
                TenantId = tenantInfo.Id.ToString();
                return true;
            }
            else
            {
                // Tạo mới tenant với GUID cho Id
                var newTenantId = Guid.NewGuid().ToString();
                TenantId = newTenantId;

                // Thêm tenant vào cơ sở dữ liệu chung
                var newTenant = new Tenant
                {
                    Id = newTenantId,
                    Name = tenantName,
                };
                await AddTenantIfNotExists(newTenant);
                return true;
            }
        }

        private async Task AddTenantIfNotExists(Tenant newTenant)
        {
            _context.Tenants.Add(newTenant);
            await _context.SaveChangesAsync();
        }

        public string? TenantId { get; set; }
    }
}
