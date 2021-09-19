using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Extensions
{
    public static class CustomServicesExtension
    {
        public static void Register<T, E>(this IServiceCollection services, AppDomain domain) where E : T
        {
            IEnumerable<Type> queryTypes = domain.GetAssemblies()
                 .SelectMany(s => s.GetTypes())
                 .Where(p => typeof(T).IsAssignableFrom(p) && p.IsInterface);

            IEnumerable<Type> queryClasses = domain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(E).IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);

            foreach (Type intf in queryTypes)
            {
                var impl = queryClasses.FirstOrDefault(c => intf.Name == c.GetInterface(intf.Name)?.Name);

                if (impl != null) services.AddScoped(intf, impl);
            }

        }
    }
}
