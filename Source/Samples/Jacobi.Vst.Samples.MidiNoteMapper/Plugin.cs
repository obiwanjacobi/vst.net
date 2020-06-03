using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;
using Jacobi.Vst.Plugin.Framework.Plugin;
using Microsoft.Extensions.DependencyInjection;

namespace Jacobi.Vst.Samples.MidiNoteMapper
{
    /// <summary>
    /// The Plugin root class that implements the interface manager and the plugin midi source.
    /// </summary>
    internal sealed class Plugin : VstPluginWithServices, IVstPluginMidiSource
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Plugin()
            : base("VST.NET Midi Note Mapper",
                  new VstProductInfo("VST.NET Code Samples", "Jacobi Software © 2008-2020", 2000),
                VstPluginCategory.Synth, VstPluginCapabilities.NoSoundInStop, 0, 0x30313233)
        {
            NoteMap = new MapNoteItemList();
        }

        protected override void RegisterServices(IServiceCollection services)
        {
            services.AddPluginComponent(new AudioProcessor(this));
            services.AddPluginComponent(new PluginEditor(this));
            services.AddPluginComponent(new MidiProcessor(this));
            services.AddPluginComponent(new PluginPersistence(this));
            services.AddSingleton<IVstPluginMidiSource>(this);
        }

        #region IVstPluginMidiSource Members

        /// <summary>
        /// Returns the channel count as reported by the host
        /// </summary>
        public int ChannelCount
        {
            get
            {
                IVstMidiProcessor midiProcessor = null;

                if (Host != null)
                {
                    midiProcessor = Host.GetInstance<IVstMidiProcessor>();
                }

                if (midiProcessor != null)
                {
                    return midiProcessor.ChannelCount;
                }

                return 0;
            }
        }

        #endregion

        /// <summary>
        /// Gets the map where all the note map items are stored.
        /// </summary>
        public MapNoteItemList NoteMap { get; private set; }

    }
}
