using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanEx.Repositories.Interceptors
{
    public class AuditDbContextInterceptors : SaveChangesInterceptor
    {
        private static readonly Dictionary<EntityState, Action<DbContext, BaseEntity>> Behaviors = new()
    {
        { EntityState.Added, AddBehaivor },
        { EntityState.Modified, ModifyBehaivor }
    };

        private static void AddBehaivor(DbContext context, BaseEntity entity)
        {
            entity.CreatedAt = DateTime.Now;
            context.Entry(entity).Property(x => x.LastModifiedAt).IsModified = false;
        }

        private static void ModifyBehaivor(DbContext context, BaseEntity entity)
        {
            entity.LastModifiedAt = DateTime.Now;
            context.Entry(entity).Property(x => x.CreatedAt).IsModified = false;
            entity.Version++;
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            foreach (var entry in eventData.Context!.ChangeTracker.Entries().ToList())
            {
                if (entry.Entity is not BaseEntity entity) continue;

                if (Behaviors.ContainsKey(entry.State))
                {
                    Behaviors[entry.State](eventData.Context, entity);
                }
            }
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }

}
