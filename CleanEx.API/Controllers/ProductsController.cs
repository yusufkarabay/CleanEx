using CleanEx.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace CleanEx.API.Controllers
{

    public class ProductsController(IProductService productService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts() =>
             await CreateActionResult(await productService.GetAllAsync());
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id) =>
            await CreateActionResult(await productService.GetProductByIdAsync(id));
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductRequest request) =>
            await CreateActionResult(await productService.CreateProductAsyn(request));
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductRequest request) =>
            await CreateActionResult(await productService.UpdateProductAsyn(id, request));
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(Guid id) =>
            await CreateActionResult(await productService.DeleteProductAsyn(id));
        [HttpGet("{page:int}/{size:int}")]
        public async Task<IActionResult> GetPagedProducts(int page, int size) =>
            await CreateActionResult(await productService.GetPagedAllAsync(page, size));
        [HttpPatch]
        public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request) =>
            await CreateActionResult(await productService.UpdateStockAsyn(request));
    }
}