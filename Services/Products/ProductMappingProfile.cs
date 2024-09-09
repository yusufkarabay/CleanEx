using AutoMapper;
using CleanEx.Repositories.Products;
using CleanEx.Services.Products.Create;

namespace CleanEx.Services.Products
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductDto, Product>();
            CreateMap<CreateProductRequest, Product>();

        }
    }

}
