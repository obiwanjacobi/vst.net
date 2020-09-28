using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;
using Jacobi.Vst.Plugin.Framework.Plugin;
using VstNetMidiPlugin.Dmp;

namespace VstNetMidiPlugin
{
    /// <summary>
    /// This object performs midi processing for your plugin.
    /// </summary>
    internal sealed class MidiProcessor : IVstMidiProcessor, IVstPluginMidiSource
    {
        private IVstMidiProcessor? _midiHost;

        /// <summary>
        /// Constructs a new Midi Processor.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        public MidiProcessor(IVstPluginEvents pluginEvents, PluginParameters parameters)
        {
            Gain = new Gain(parameters.GainParameters);
            Transpose = new Transpose(parameters.TransposeParameters);

            // for most hosts, midi output is expected during the audio processing cycle.
            SyncWithAudioProcessor = true;

            pluginEvents.Opened += Plugin_Opened;
        }

        private void Plugin_Opened(object? sender, System.EventArgs e)
        {
            var plugin = (VstPlugin?)sender;

            // a plugin must implement IVstPluginMidiSource or this call will throw an exception.
            _midiHost = plugin?.Host?.GetInstance<IVstMidiProcessor>();
        }

        internal Gain Gain { get; private set; }
        internal Transpose Transpose { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating to sync with audio processing.
        /// </summary>
        /// <remarks>
        /// False: will output midi to the host in the MidiProcessor.
        /// True: will output midi to the host in the AudioProcessor.
        /// </remarks>
        public bool SyncWithAudioProcessor { get; set; }

        public int ChannelCount
        {
            get { return 16; }
        }

        /// <summary>
        /// Midi events are received from the host on this method.
        /// </summary>
        /// <param name="events">A collection with midi events. Never null.</param>
        /// <remarks>
        /// Note that some hosts will only receieve midi events during audio processing.
        /// See also <see cref="IVstPluginAudioProcessor"/>.
        /// </remarks>
        public void Process(VstEventCollection events)
        {
            CurrentEvents = events;

            if (!SyncWithAudioProcessor)
            {
                ProcessCurrentEvents();
            }
        }

        // cache of events (for when syncing up with the AudioProcessor).
        public VstEventCollection? CurrentEvents { get; private set; }

        public void ProcessCurrentEvents()
        {
            if (CurrentEvents == null || CurrentEvents.Count == 0)
                return;

            // always expect some hosts not to support this.
            if (_midiHost != null)
            {
                VstEventCollection outEvents = new VstEventCollection();

                // NOTE: other types of events could be in the collection!
                foreach (VstEvent evnt in CurrentEvents)
                {
                    switch (evnt.EventType)
                    {
                        case VstEventTypes.MidiEvent:
                            VstMidiEvent midiEvent = (VstMidiEvent)evnt;

                            midiEvent = Gain.ProcessEvent(midiEvent);
                            midiEvent = Transpose.ProcessEvent(midiEvent);

                            outEvents.Add(midiEvent);
                            break;
                        default:
                            // non VstMidiEvent
                            outEvents.Add(evnt);
                            break;
                    }
                }

                _midiHost.Process(outEvents);
            }

            // Clear the cache, we've processed the events.
            CurrentEvents = null;
        }

        #region IVstPluginMidiSource Members

        int IVstPluginMidiSource.ChannelCount
        {
            get { return 16; }
        }

        #endregion
    }
}
