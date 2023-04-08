using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TheMonarchs.API.Common.Behaviours;
using TheMonarchs.Core.Interfaces;
using TheMonarchs.Infrastructure.Repositories;
namespace TheMonarchs.API.Extensions
{
    public static class IServicesCollectionExtension
    {
        public static IServiceCollection AddRepositoriesRegistrations(this IServiceCollection services, IMonarchDataProvider monarchDataProvider)
        {
            services.AddScoped(opt => monarchDataProvider);
            services.AddScoped<IMonarchDataRepository, MonarchDataRepository>();
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAppServicesRegistration(this IServiceCollection services)
        {

            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblyContaining(typeof(Startup)); });
            services.AddValidatorsFromAssemblyContaining(typeof(Startup));
            services.AddAutoMapper(typeof(Startup));


            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            return services;
        }
    }
}
