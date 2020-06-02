﻿using Microsoft.Extensions.DependencyInjection;
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
        [CLSCompliant(false)]
        public static void AddPluginComponent(this IServiceCollection services, object instance)
        {
            if (instance == null) return;

            var type = instance.GetType();
            services.AddSingleton(type, instance);
            foreach (var interf in type.GetInterfaces())
            {
                services.AddSingleton(interf, instance);
            }
        }
    }
}
