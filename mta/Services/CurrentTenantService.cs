using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mta.Models;

namespace Services
{
    public class CurrentTenantService : ICurrentTenantService
    {
        private readonly TenantDbContext _context;

        public CurrentTenantService(TenantDbContext context)
        {
            _context = context;
        }

        // Phải có getter và setter công khai
        public string? TenantId { get; set; }

        public async Task<bool> SetTenant(string tenantId)
        {
            var tenantInfo = await _context.Tenants
                                            .Where(x => x.Id == tenantId)
                                            .FirstOrDefaultAsync();
            if (tenantInfo != null)
            {
                TenantId = tenantInfo.Id;
                return true;
            }
            else
            {
                // Tạo và lưu tenant mới nếu không tồn tại
                tenantInfo = new Tenant
                {
                    Id = tenantId,
                    Name = tenantId // hoặc tên khác nếu bạn muốn
                };
                _context.Tenants.Add(tenantInfo);
                await _context.SaveChangesAsync();

                TenantId = tenantInfo.Id;
                return true;
            }
        }
    }
}
