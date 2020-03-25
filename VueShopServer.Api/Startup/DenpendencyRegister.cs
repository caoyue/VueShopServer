using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using VueShopServer.Api.Data;

namespace VueShopServer.Api
{
    public static class DependencyRegister
    {
        public static IServiceCollection RegisterRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(MockRepository<>));
            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services,
            string name = "Service")
        {
            foreach (var t in GetServices(name))
            {
                foreach (var typeArray in t.Value)
                {
                    services.AddScoped(typeArray, t.Key);
                }
            }
            return services;
        }

        private static Dictionary<Type, Type[]> GetServices(string name)
        {
            const string project = "VueShopServer.Api";
            var assembly = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(t => t.FullName.Contains(project)).FirstOrDefault();
            var ts = assembly.GetTypes().ToList();

            var result = new Dictionary<Type, Type[]>();
            foreach (var item in ts.Where(s => !s.IsInterface && s.Name.EndsWith(name)))
            {
                var interfaceType = item.GetInterfaces();
                result.Add(item, interfaceType);
            }
            return result;
        }
    }
}
