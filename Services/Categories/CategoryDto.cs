namespace CleanEx.Services.Categories
{
    public record CategoryDto(Guid Id, string Name, string Description, ICollection<CategoryWithProductsDto> products);


}
