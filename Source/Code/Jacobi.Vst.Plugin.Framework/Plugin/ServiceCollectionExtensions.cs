using Microsoft.Extensions.DependencyInjection;

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
        /// <typeparam name="T">The implementation Type.</typeparam>
        /// <param name="services">Must not be null.</param>
        public static IServiceCollection AddSingletonAll<T>(this IServiceCollection services)
        {
            var type = typeof(T);
            services.AddSingleton(type);
            foreach (var interf in type.GetInterfaces())
            {
                services.AddSingleton(interf, type);
            }

            return services;
        }
    }
}
