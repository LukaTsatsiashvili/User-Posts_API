﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Context;
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

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}