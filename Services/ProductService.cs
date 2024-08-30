using CleanEx.Repositories;
using CleanEx.Repositories.Product;

namespace CleanEx.Services
{
    public class ProductService(IGenericRepository<Product> productRepository)
    {
        private readonly IGenericRepository<Product> _productRepository = productRepository;

    }
}
