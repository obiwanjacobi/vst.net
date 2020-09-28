using Jacobi.Vst.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Jacobi.Vst.Plugin.Framework.Plugin
{
    /// <summary>
    /// Provides a Plugin base class that uses DI for registering plugin object interfaces.
    /// </summary>
    public abstract class VstPluginWithServices : VstPlugin
    {
        // guard against recursion during init.
        private bool _insideConfigServices;

        /// <inheritdoc/>
        protected VstPluginWithServices(string name, int pluginID,
            VstProductInfo productInfo, VstPluginCategory category,
            int initialDelay = 0, VstPluginCapabilities capabilities = VstPluginCapabilities.None)
            : base(name, pluginID, productInfo, category, initialDelay, capabilities)
        { }

        /// <summary>
        /// DI container configuration. Configure and register services.
        /// Override to register the plugin interfaces and the class/types that implement them.
        /// </summary>
        /// <param name="services">Will never be null.</param>
        protected abstract void ConfigureServices(IServiceCollection services);

        /// <summary>
        /// Override to configure LogProviders.
        /// </summary>
        /// <param name="builder">Is never null.</param>
        protected virtual void ConfigureLogging(ILoggingBuilder builder)
        { }

        /// <summary>
        /// Provides access to all configured/registered services.
        /// </summary>
        /// <remarks>Does not create the service provider if it does not exist, so may return null.</remarks>
        public IServiceProvider? Services { get; private set; }

        /// <summary>
        /// Indicates if the interface <typeparamref name="T"/> is supported by the object.
        /// </summary>
        /// <typeparam name="T">The interface type.</typeparam>
        /// <returns>Returns true if the interface <typeparamref name="T"/> is supported.</returns>
        public override bool Supports<T>() where T : class
        {
            return (GetInstance<T>() != null);
        }

        /// <summary>
        /// Retrieves a reference to an implementation of the interface <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The interface type.</typeparam>
        /// <returns>Returns null when the <typeparamref name="T"/> is not supported.</returns>
        public override T? GetInstance<T>() where T : class
        {
            if (_insideConfigServices)
            {
                throw new InvalidOperationException(Properties.Resources.VstPluginWithServices_RecursionError);
            }

            var services = GetServices();
            var service = services.GetService<T>();
            if (service != null)
            {
                return service;
            }

            var type = typeof(T);

            // special case for the AudioProcessor: IVstPluginAudioprecisionProcessor could also provide the IVstPluginAudioProcessor.
            if (type.Equals(typeof(IVstPluginAudioProcessor)))
            {
                return (T)services.GetService<IVstPluginAudioPrecisionProcessor>();
            }

            // special case for parameters: can be on the active program
            if (type.Equals(typeof(IVstPluginParameters)))
            {
                var programs = services.GetService<IVstPluginPrograms>();
                return programs?.ActiveProgram as T;
            }

            return null;
        }

        private IServiceProvider GetServices()
        {
            if (Services == null)
            {
                _insideConfigServices = true;

                var serviceCollection = new ServiceCollection();
                if (Configuration != null)
                {
                    serviceCollection.AddSingleton(Configuration);
                }

                serviceCollection
                    .AddSingletonAll(this)
                    .AddLogging(ConfigureLogging);

                ConfigureServices(serviceCollection);
                Services = serviceCollection.BuildServiceProvider();

                _insideConfigServices = false;
            }

            return Services;
        }
    }
}
