namespace CleanEx.Services.Products;

public record ProductDto(Guid Id, string Name, decimal Price, string Description, int stock);

