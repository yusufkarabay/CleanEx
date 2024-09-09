using Microsoft.EntityFrameworkCore;

namespace CleanEx.Repositories.Categories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category?> GetCategoryWithProductsAsync(Guid id);
        Task<IQueryable<Category?>> GetCategoryWithProductsAsync();
    }
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(CleanExDbContext context) : base(context)
        {
        }

        public async Task<Category?> GetCategoryWithProductsAsync(Guid id)
        {
            return await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IQueryable<Category?>> GetCategoryWithProductsAsync()
        {
            return _context.Categories.Include(c => c.Products).AsQueryable();
        }
    }
}
