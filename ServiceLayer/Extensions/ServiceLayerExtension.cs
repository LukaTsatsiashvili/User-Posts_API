using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.FluentValidation.UserValidator;
using System.Reflection;

namespace ServiceLayer.Extensions
{
    public static class ServiceLayerExtension
    {
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services, IConfiguration config)
        {
            // AutoMapper configuration
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Fluent Validation configuration
            services.AddFluentValidationAutoValidation(opt =>
            {
                opt.DisableDataAnnotationsValidation = true;
            });

            services.AddValidatorsFromAssemblyContaining<UserCreateValidation>();

            var types = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.Name.EndsWith("Service"));

            foreach (var serviceType in  types)
            {
                var iServiceType = serviceType.GetInterfaces().FirstOrDefault(x => x.Name == $"I{serviceType.Name}");
                if (iServiceType != null)
                {
                    services.AddScoped(iServiceType, serviceType);
                }
            }

            return services;
        }
    }
}
