using AutoMapper;
using CleanEx.Repositories.Categories;
using CleanEx.Services.Categories.Create;
using CleanEx.Services.Categories.Update;

namespace CleanEx.Services.Categories
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryWithProductsDto>().ReverseMap();
            CreateMap<Category, CreateCategoryRequest>().ReverseMap();
            CreateMap<Category, UpdateCategoryRequest>().ReverseMap();
        }
    }
}
