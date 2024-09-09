namespace CleanEx.Services.Products.Update;

public record UpdateProductRequest(Guid Id, string Name, decimal Price, string Description, int Stock);
