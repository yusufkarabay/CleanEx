using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanEx.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(bool trackChanges);

        Task<IQueryable<T>> SearchAsync(Expression<Func<T, bool>> conditions, bool trackChanges);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<T> FindAsync(Expression<Func<T, bool>> conditions, bool trackChanges);

        Task<bool> ExistsAsync(Guid id);

        Task AddRangeAsync(IEnumerable<T> entities);

        public class GenericRepository<T>(CleanExDbContext context) : IGenericRepository<T>
            where T : class
        {

            protected readonly CleanExDbContext _context = context;

            public async Task<T> AddAsync(T entity)
            {
                var result = await _context.AddAsync(entity);
                return result.Entity;
            }

            public async Task AddRangeAsync(IEnumerable<T> entities)
            {
                await _context.AddRangeAsync(entities);
            }

            public async Task DeleteAsync(T entity) => _context.Set<T>().Remove(entity);

            public async Task<bool> ExistsAsync(Guid id) => await _context.Set<T>().AnyAsync(x => x.GetType().GetProperty("Id").GetValue(x).Equals(id));

            public async Task<T> FindAsync(Expression<Func<T, bool>> conditions, bool trackChanges)
            {
                return !trackChanges ? await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(conditions) : await _context.Set<T>().FirstOrDefaultAsync(conditions);
            }

            public async Task<IEnumerable<T>> GetAllAsync(bool trackChanges)
            {
                return !trackChanges ? await _context.Set<T>().AsNoTracking().ToListAsync() : await _context.Set<T>().ToListAsync();
            }

            public async Task<IQueryable<T>> SearchAsync(Expression<Func<T, bool>> conditions, bool trackChanges)
                => !trackChanges ? _context.Set<T>().AsNoTracking().Where(conditions) : _context.Set<T>().Where(conditions);

            public async Task UpdateAsync(T entity)
            {
                _context.Set<T>().Update(entity);
            }

        }
    }
}
