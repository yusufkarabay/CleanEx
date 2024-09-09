using CleanEx.Repositories;
using CleanEx.Repositories.Products;
using System.Net;

namespace CleanEx.Services.Products
{
    public interface IProductService
    {
        public Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsyn(int count);
        public Task<ServiceResult<CreateProductResponse>> CreateProductAsyn(CreateProductRequest request);
        public Task<ServiceResult<NoContentDto>> UpdateProductAsyn(Guid id, UpdateProductRequest request);
        public Task<ServiceResult<NoContentDto>> UpdateStockAsyn(UpdateProductStockRequest updateProductStockRequest);
        public Task<ServiceResult<NoContentDto>> DeleteProductAsyn(Guid id);
        public Task<ServiceResult<List<ProductDto>>> GetAllAsync();
        public Task<ServiceResult<ProductDto>> GetProductByIdAsync(Guid id);
        public Task<ServiceResult<List<ProductDto>>> GetPagedAllAsync(int page, int size);
    }
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsyn(int count)
        {
            var products = await _productRepository.GetTopPriceProductAsyn(count);
            if (products.Count == 0)
            {
                return ServiceResult<List<ProductDto>>.FailMessage("Products not found", true, HttpStatusCode.NotFound);
            }
            var productDtos = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Description, p.Stock)).ToList();
            return ServiceResult<List<ProductDto>>.Success(productDtos);
        }

        public async Task<ServiceResult<ProductDto>> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null)
            {
                return ServiceResult<ProductDto>.FailMessage("Product not found", true, HttpStatusCode.NotFound);
            }
            var productDto = new ProductDto(product.Id, product.Name, product.Price, product.Description, product.Stock);
            return ServiceResult<ProductDto>.Success(productDto);
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateProductAsyn(CreateProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                Stock = request.Stock
            };
            await _productRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id), $"api/prodcuts/{product.Id}");
        }

        public async Task<ServiceResult<NoContentDto>> UpdateProductAsyn(Guid id, UpdateProductRequest request)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null)
            {
                return ServiceResult<NoContentDto>.FailMessage("Product not found", true, HttpStatusCode.NotFound);
            }
            product.Name = request.Name;
            product.Price = request.Price;
            product.Description = request.Description;
            product.Stock = request.Stock;
            await _productRepository.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<NoContentDto>.Success();
        }

        public async Task<ServiceResult<NoContentDto>> DeleteProductAsyn(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null)
            {
                return ServiceResult<NoContentDto>.FailMessage("Product not found", true, HttpStatusCode.NotFound);
            }
            await _productRepository.DeleteAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<NoContentDto>.Success();
        }

        public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
        {
            var products = _productRepository.GetAllAsync(false).Result.ToList();
            if (products.Count == 0)
            {
                return ServiceResult<List<ProductDto>>.FailMessage("Products not found", true, HttpStatusCode.NotFound);
            }
            var productDtos = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Description, p.Stock)).ToList();
            return ServiceResult<List<ProductDto>>.Success(productDtos);
        }

        public async Task<ServiceResult<List<ProductDto>>> GetPagedAllAsync(int page, int size)
        {
            var products = _productRepository.GetAllAsync(false).Result.Skip((page - 1) * size).Take(size).ToList();
            if (products.Count == 0)
            {
                return ServiceResult<List<ProductDto>>.FailMessage("Products not found", true, HttpStatusCode.NotFound);
            }
            var productDtos = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Description, p.Stock)).ToList();
            return ServiceResult<List<ProductDto>>.Success(productDtos);
        }

        public async Task<ServiceResult<NoContentDto>> UpdateStockAsyn(UpdateProductStockRequest updateProductStockRequest)
        {
            var product = _productRepository.GetByIdAsync(updateProductStockRequest.Id).Result;
            if (product is null)
            {
                return ServiceResult<NoContentDto>.FailMessage("Product not found", true, HttpStatusCode.NotFound);
            }
            product.Stock = updateProductStockRequest.Quantity;
            await _productRepository.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<NoContentDto>.Success();
        }
    }



}
