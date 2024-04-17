using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Context.API;
using RepositoryLayer.Context.Identity;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.Repositories.Concrete;
using RepositoryLayer.UnitOfWorks.Abstract;
using RepositoryLayer.UnitOfWorks.Concrete;

namespace RepositoryLayer.Extensions
{
    public static class RepositoryLayerExtension
    {
        public static IServiceCollection LoadRepositoryLayerExtensions(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("SqlConnection")));
            services.AddDbContext<ApplicationAuthDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("AuthConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
