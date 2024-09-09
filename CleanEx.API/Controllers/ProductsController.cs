using CleanEx.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace CleanEx.API.Controllers
{

    public class ProductsController(IProductService productService) : CustomBaseController
    {
        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts() =>
             await CreateActionResult(await productService.GetAllAsync());
        [HttpGet]
        [Route("GetProductById")]
        public async Task<IActionResult> GetById(Guid id) =>
            await CreateActionResult(await productService.GetProductByIdAsync(id));
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductRequest request) =>
            await CreateActionResult(await productService.CreateProductAsyn(request));
        [HttpPost]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductRequest request) =>
            await CreateActionResult(await productService.UpdateProductAsyn(id, request));
        [HttpPost]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(Guid id) =>
            await CreateActionResult(await productService.DeleteProductAsyn(id));
    }
}