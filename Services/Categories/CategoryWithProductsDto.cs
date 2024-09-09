using CleanEx.Services.Products;

namespace CleanEx.Services.Categories
{
    public record CategoryWithProductsDto(Guid Id, string Name, string Description, ICollection<ProductDto> Products);



}
