using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;
using Jacobi.Vst.Plugin.Framework.Plugin;
using Microsoft.Extensions.DependencyInjection;

namespace Jacobi.Vst.Samples.Delay
{
    /// <summary>
    /// The Plugin root class.
    /// </summary>
    internal sealed class Plugin : VstPluginWithServices
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Plugin()
            : base("VST.NET Delay Plugin",
                new VstProductInfo("VST.NET Code Samples", "Jacobi Software © 2008-2020", 2000),
                VstPluginCategory.RoomFx, VstPluginCapabilities.None, 0, 0x3A3A3A3A)
        {
            ParameterFactory = new PluginParameterFactory();

            AudioProcessor audioProcessor = GetInstance<AudioProcessor>();
            // add delay parameters to factory
            ParameterFactory.ParameterInfos.AddRange(audioProcessor.Delay.ParameterInfos);
        }

        /// <summary>
        /// Gets the parameter factory.
        /// </summary>
        public PluginParameterFactory ParameterFactory { get; private set; }

        protected override void RegisterServices(IServiceCollection services)
        {
            var audioProcessor = new AudioProcessor(this);
            services.AddPluginComponent(audioProcessor);

            var persistence = new PluginPersistence(this);
            services.AddPluginComponent(persistence);

            var programs = new PluginPrograms(this);
            services.AddPluginComponent(programs);
        }
    }
}
