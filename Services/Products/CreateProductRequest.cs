﻿namespace CleanEx.Services.Products;

public record CreateProductRequest(string Name, string Description, decimal Price, int Stock);
