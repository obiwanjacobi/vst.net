using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;
using Jacobi.Vst.Plugin.Framework.Plugin;
using Microsoft.Extensions.DependencyInjection;

namespace Jacobi.Vst.Samples.MidiNoteMapper
{
    /// <summary>
    /// The Plugin root class that implements the interface manager and the plugin midi source.
    /// </summary>
    internal sealed class Plugin : VstPluginWithServices
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Plugin()
            : base("VST.NET 2 Midi Note Mapper", 0x30313233,
                new VstProductInfo("VST.NET 2 Code Samples", "Jacobi Software © 2008-2020", 2000),
                VstPluginCategory.Synth)
        { }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MapNoteItemList>()
                .AddSingletonAll<AudioProcessor>()
                .AddSingletonAll<PluginEditor>()
                .AddSingletonAll<MidiProcessor>()
                .AddSingletonAll<PluginPersistence>();
        }
    }
}
