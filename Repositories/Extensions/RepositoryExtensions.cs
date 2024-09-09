using CleanEx.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanEx.Repositories.Extensions
{
    public static class RepositoryExtensions
    {

        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CleanExDbContext>(options =>
            {
                var connectionStringOption = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
                options.UseSqlServer(connectionStringOption!.SqlServer, sqlServerOptionsAction =>
                {
                    sqlServerOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);
                });
            });
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
