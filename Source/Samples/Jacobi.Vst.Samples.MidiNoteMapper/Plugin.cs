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
            : base("VST.NET Midi Note Mapper", 0x30313233,
                new VstProductInfo("VST.NET Code Samples", "Jacobi Software © 2008-2020", 2000),
                VstPluginCategory.Synth)
        { }

        /// <summary>
        /// Gets the map where all the note map items are stored.
        /// </summary>
        public MapNoteItemList NoteMap { get; } = new MapNoteItemList();

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingletonAll<AudioProcessor>();
            services.AddSingletonAll<PluginEditor>();
            services.AddSingletonAll<MidiProcessor>();
            services.AddSingletonAll<PluginPersistence>();
        }
    }
}
