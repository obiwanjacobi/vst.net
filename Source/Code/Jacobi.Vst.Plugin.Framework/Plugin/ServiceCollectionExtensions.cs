using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Jacobi.Vst.Plugin.Framework.Plugin
{
    /// <summary>
    /// ServiceCollection helpers
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds all interfaces and the class type of <paramref name="instance"/> to <paramref name="services"/>.
        /// </summary>
        /// <param name="services">Must not be null.</param>
        /// <param name="instance">Does nothing if null.</param>
        public static IServiceCollection AddSingletonAll(this IServiceCollection services, object instance)
        {
            if (instance != null)
            {
                var type = instance.GetType();
                services.AddSingleton(type, instance);
                foreach (var interf in type.GetInterfaces())
                {
                    services.AddSingleton(interf, instance);
                }
            }

            return services;
        }

        /// <summary>
        /// Adds all interfaces and the class type of <typeparamref name="T"/> to <paramref name="services"/>.
        /// </summary>
        /// <typeparam name="T">The class type.</typeparam>
        /// <param name="services">Must not be null.</param>
        public static IServiceCollection AddSingletonAll<T>(this IServiceCollection services)
            where T : class
        {
            return services.AddSingletonAll(typeof(T));
        }

        /// <summary>
        /// Adds all interfaces and the class type of <paramref name="classType"/> to <paramref name="services"/>.
        /// </summary>
        /// <param name="services">Must not be null.</param>
        /// <param name="classType">The Type of the class.</param>
        public static IServiceCollection AddSingletonAll(this IServiceCollection services, Type classType)
        {
            services.AddSingleton(classType);

            foreach (var interf in classType.GetInterfaces())
            {
                services.TryAddSingleton(interf, classType);
            }

            return services;
        }
    }
}
