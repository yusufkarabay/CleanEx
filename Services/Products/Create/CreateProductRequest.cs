﻿namespace CleanEx.Services.Products.Create;

public record CreateProductRequest(string Name, string Description, decimal Price, int Stock);
