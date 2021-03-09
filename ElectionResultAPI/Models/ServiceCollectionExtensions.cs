using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using ElectionResultAPI.Service;
using ElectionResultAPI.Repository;
using ElectionResultAPI;

namespace ElectionAPI.Models
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCofig(this IServiceCollection services, IConfiguration config)
        {
            return services;
        }

        public static void RegisterAllTypes<T>(this IServiceCollection services, Assembly[] assemblies,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
            foreach (var type in typesFromAssemblies)
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));

        }
        public static void RegisterServices(this IServiceCollection services)
        {
            services.RegisterAllTypes<ISignatureService>(new[] { typeof(Startup).Assembly });
            services.RegisterAllTypes<IVoteService>(new[] { typeof(Startup).Assembly });
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.RegisterAllTypes<ISignatureRepository>(new[] { typeof(Startup).Assembly });
            services.RegisterAllTypes<IVoteRepository>(new[] { typeof(Startup).Assembly });
        }
    }
}
