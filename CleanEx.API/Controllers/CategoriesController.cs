using CleanEx.Services.Categories;
using CleanEx.Services.Categories.Create;
using CleanEx.Services.Categories.Update;
using Microsoft.AspNetCore.Mvc;

namespace CleanEx.API.Controllers
{

    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService=categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories() =>
             await CreateActionResult(await _categoryService.GetAllCategories());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id) =>
            await CreateActionResult(await _categoryService.GetCategoryById(id));
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request) =>
            await CreateActionResult(await _categoryService.Create(request));
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryRequest request) =>
            await CreateActionResult(await _categoryService.Update(id, request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id) =>
            await CreateActionResult(await _categoryService.Delete(id));
        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetCategoryWithProducts(Guid id) =>
            await CreateActionResult(await _categoryService.GetCategoryWithProducts(id));

    }
}
