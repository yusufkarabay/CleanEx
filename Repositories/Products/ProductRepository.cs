using Microsoft.EntityFrameworkCore;

namespace CleanEx.Repositories.Products
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<List<Product>> GetTopPriceProductAsyn(int count);
        public Task<Product> GetByIdAsync(Guid id);
    }

    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(CleanExDbContext context) : base(context)
        {
        }

        public Task<Product> GetByIdAsync(Guid id)
        {
            return _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<List<Product>> GetTopPriceProductAsyn(int count)
        {
            return _context.Products.OrderByDescending(p => p.Price).Take(count).ToListAsync();
        }
    }
}
