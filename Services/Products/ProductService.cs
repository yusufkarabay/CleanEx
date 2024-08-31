using CleanEx.Repositories;
using CleanEx.Repositories.Products;
using System.Net;

namespace CleanEx.Services.Products
{
    public interface IProductService
    {
        public Task<ServiceResult<List<Product>>> GetTopPriceProductAsyn(int count);
    }
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ServiceResult<List<Product>>> GetTopPriceProductAsyn(int count)
        {
            var products = await _productRepository.GetTopPriceProductAsyn(count);
            if (products.Count == 0)
            {
                return ServiceResult<List<Product>>.FailMessage("Products not found", HttpStatusCode.NotFound, true);
            }
            return ServiceResult<List<Product>>.Success(products);
        }
        public async Task<ServiceResult<Product>> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null)
            {
                return ServiceResult<Product>.FailMessage("Product not found", HttpStatusCode.NotFound, true);
            }
            return ServiceResult<Product>.Success(product);
        }
    }


}
