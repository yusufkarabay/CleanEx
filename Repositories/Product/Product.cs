namespace CleanEx.Repositories.Product
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; } = default!;
    }
}
