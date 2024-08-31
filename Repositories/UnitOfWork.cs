namespace CleanEx.Repositories
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        Task<int> SaveChangesAsync();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CleanExDbContext _context;
        public UnitOfWork(CleanExDbContext context)
        {
            _context = context;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }

}
