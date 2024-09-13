using AutoMapper;
using CleanEx.Repositories;
using CleanEx.Repositories.Categories;
using CleanEx.Services.Categories.Create;
using CleanEx.Services.Categories.Update;

namespace CleanEx.Services.Categories
{
    public interface ICategoryService
    {
        Task<ServiceResult<CreateCategoryResponse>> Create(CreateCategoryRequest request);
        Task<ServiceResult<CategoryDto>> Update(Guid id, UpdateCategoryRequest request);
        Task<ServiceResult<CategoryDto>> Delete(Guid id);
        Task<ServiceResult<CategoryDto>> GetCategoryById(Guid id);
        Task<ServiceResult<List<CategoryDto>>> GetAllCategories();
        Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProducts(Guid id);
    }
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _categoryRepository=categoryRepository;
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }

        public async Task<ServiceResult<CreateCategoryResponse>> Create(CreateCategoryRequest request)
        {
            var existingCategory = await _categoryRepository.FindAsync(x => x.Name==request.Name, false);
            if (existingCategory != null)
            {
                return ServiceResult<CreateCategoryResponse>.Fail("Category already exists", true, System.Net.HttpStatusCode.BadRequest);
            }
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description
            };
            await _categoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id), $"api/categories/{category.Id}");
        }

        public async Task<ServiceResult<CategoryDto>> Delete(Guid id)
        {
            var category = await _categoryRepository.FindAsync(x => x.Id==id, true);
            if (category == null)
            {
                return ServiceResult<CategoryDto>.Fail("Category not found", true);
            }
            await _categoryRepository.DeleteAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<CategoryDto>.Success(new CategoryDto(category.Id, category.Name, category.Description, new List<CategoryWithProductsDto>()));
        }

        public async Task<ServiceResult<List<CategoryDto>>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync(false);
            var mappedCategories = _mapper.Map<List<CategoryDto>>(categories);
            return ServiceResult<List<CategoryDto>>.Success(mappedCategories);
        }

        public async Task<ServiceResult<CategoryDto>> GetCategoryById(Guid id)
        {
            var category = await _categoryRepository.FindAsync(x => x.Id==id, true);
            if (category == null)
            {
                return ServiceResult<CategoryDto>.Fail("Category not found", true);
            }
            var mappedCategory = _mapper.Map<CategoryDto>(category);
            return ServiceResult<CategoryDto>.Success(mappedCategory);
        }

        public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProducts(Guid id)
        {
            var category = await _categoryRepository.GetCategoryWithProductsAsync(id);
            if (category == null)
            {
                return ServiceResult<CategoryWithProductsDto>.Fail("Category not found", true);
            }
            var mappedCategory = _mapper.Map<CategoryWithProductsDto>(category);
            return ServiceResult<CategoryWithProductsDto>.Success(mappedCategory);

        }
        public async Task<ServiceResult<CategoryDto>> Update(Guid id, UpdateCategoryRequest request)
        {
            var category = await _categoryRepository.FindAsync(x => x.Id == id, true);
            if (category == null)
            {
                return ServiceResult<CategoryDto>.Fail("Category not found", true);
            }

            var isCategoryNameExist = await _categoryRepository.FindAsync(x => x.Name == request.Name && x.Id != id, true);
            if (isCategoryNameExist != null)
            {
                return ServiceResult<CategoryDto>.Fail("Category already exists", true);
            }

            var mappedCategory = _mapper.Map(request, category);

            await _categoryRepository.UpdateAsync(mappedCategory);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<CategoryDto>.Success(new CategoryDto(category.Id, category.Name, category.Description, new List<CategoryWithProductsDto>()));
        }


    }
}
