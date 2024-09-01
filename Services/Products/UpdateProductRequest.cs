namespace CleanEx.Services.Products;

public record UpdateProductRequest(Guid Id, string Name, decimal Price, string Description, int Stock);
