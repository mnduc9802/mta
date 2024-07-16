using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mta.Models;
using mta.Services.DTOs;

namespace Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentTenantService _currentTenantService;

        public ProductService(ApplicationDbContext context, ICurrentTenantService currentTenantService)
        {
            _context = context;
            _currentTenantService = currentTenantService;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            if (string.IsNullOrEmpty(_currentTenantService.TenantId))
            {
                throw new InvalidOperationException("Current tenant ID is not set.");
            }

            return _context.Products
                            .Where(p => p.TenantId == _currentTenantService.TenantId)
                            .ToList();
        }

        public Product CreateProduct(CreateProductRequest request)
        {
            if (string.IsNullOrEmpty(_currentTenantService.TenantId))
            {
                throw new InvalidOperationException("Current tenant ID is not set.");
            }

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                TenantId = _currentTenantService.TenantId
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return product;
        }

        public bool DeleteProduct(int id)
        {
            if (string.IsNullOrEmpty(_currentTenantService.TenantId))
            {
                throw new InvalidOperationException("Current tenant ID is not set.");
            }

            var product = _context.Products
                                   .Where(p => p.Id == id && p.TenantId == _currentTenantService.TenantId)
                                   .FirstOrDefault();

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
