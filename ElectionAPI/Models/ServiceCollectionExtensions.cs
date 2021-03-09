using ElectionAPI.Repository;
using ElectionAPI.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace ElectionAPI.Models
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)
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
            services.RegisterAllTypes<ICategoryService>(new[] { typeof(Startup).Assembly });
            services.RegisterAllTypes<ICategoryTypeService>(new[] { typeof(Startup).Assembly });
            services.RegisterAllTypes<IElectionService>(new[] { typeof(Startup).Assembly });
            services.RegisterAllTypes<IPartyService>(new[] { typeof(Startup).Assembly });
            services.RegisterAllTypes<ITicketService>(new[] { typeof(Startup).Assembly });
            services.RegisterAllTypes<IVoteService>(new[] { typeof(Startup).Assembly });
            services.RegisterAllTypes<ISignatureService>(new[] { typeof(Startup).Assembly });
            services.RegisterAllTypes<IBallotService>(new[] { typeof(Startup).Assembly });
            services.RegisterAllTypes<ISignatureNoticeService>(new[] { typeof(Startup).Assembly });
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.RegisterAllTypes<ICategoryRepository>(new[] { typeof(Startup).Assembly });
            services.RegisterAllTypes<ICategoryTypeRepository>(new[] { typeof(Startup).Assembly });
            services.RegisterAllTypes<IElectionRepository>(new[] { typeof(Startup).Assembly });
            services.RegisterAllTypes<IPartyRepository>(new[] { typeof(Startup).Assembly });
            services.RegisterAllTypes<ITicketRepository>(new[] { typeof(Startup).Assembly });
            services.RegisterAllTypes<IVoteRepository>(new[] { typeof(Startup).Assembly });
            services.RegisterAllTypes<ISignatureRepository>(new[] { typeof(Startup).Assembly });
            services.RegisterAllTypes<IBallotRepository>(new[] { typeof(Startup).Assembly });
        }
    }
}
