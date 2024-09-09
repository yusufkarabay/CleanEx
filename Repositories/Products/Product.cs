using CleanEx.Repositories.Categories;

namespace CleanEx.Repositories.Products
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; } = default!;
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; } = default!;
    }
}
