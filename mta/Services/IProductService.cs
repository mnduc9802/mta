using mta.Models;
using mta.Services.DTOs;

namespace mta.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product CreateProduct(CreateProductRequest request);
        bool DeleteProduct(int id);
    }
}
