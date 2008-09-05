namespace Jacobi.Vst.Samples.MidiNoteMapper
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework;
    using Jacobi.Vst.Framework.Common;
    using Jacobi.Vst.Framework.Plugin;

    /// <summary>
    /// The Plugin root class that implements the interface manager and the plugin midi source.
    /// </summary>
    class Plugin : PluginInterfaceManagerBase, IVstPlugin, IVstPluginMidiSource
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Plugin()
        {
            NoteMap = new MapNoteItemList();
        }

        private IVstHost _host;
        /// <summary>
        /// Gets the vst host interface.
        /// </summary>
        public IVstHost Host
        { get { return _host; } }

        /// <summary>
        /// Creates a default instance and reuses that for all threads.
        /// </summary>
        /// <param name="instance">A reference to the default instance or null.</param>
        /// <returns>Returns the default instance.</returns>
        protected override IVstPluginAudioProcessor CreateAudioProcessor(IVstPluginAudioProcessor instance)
        {
            if (instance == null) return new AudioProcessor(this);

            return instance;
        }

        /// <summary>
        /// Creates a default instance and reuses that for all threads.
        /// </summary>
        /// <param name="instance">A reference to the default instance or null.</param>
        /// <returns>Returns the default instance.</returns>
        protected override IVstPluginEditor CreateEditor(IVstPluginEditor instance)
        {
            if (instance == null) return new PluginEditor(this);

            return instance;
        }

        /// <summary>
        /// Creates a default instance and reuses that for all threads.
        /// </summary>
        /// <param name="instance">A reference to the default instance or null.</param>
        /// <returns>Returns the default instance.</returns>
        protected override IVstMidiProcessor CreateMidiProcessor(IVstMidiProcessor instance)
        {
            if (instance == null) return new MidiProcessor(this);

            return instance;
        }

        /// <summary>
        /// Always returns <b>this</b>.
        /// </summary>
        /// <param name="instance">A reference to the default instance or null.</param>
        /// <returns>Returns the default instance <b>this</b>.</returns>
        protected override IVstPluginMidiSource CreateMidiSource(IVstPluginMidiSource instance)
        {
            return this;
        }

        #region IVstPlugin Members

        public VstPluginCapabilities Capabilities
        {
            get { return VstPluginCapabilities.NoSoundInStop; }
        }

        public VstPluginCategory Category
        {
            get { return VstPluginCategory.Unknown; }
        }

        public int InitialDelay
        {
            get { return 0; }
        }

        public string Name
        {
            get { return "VST.NET Midi Note Mapper"; }
        }

        public void Open(IVstHost host)
        {
            _host = host;
        }

        public int PluginID
        {
            get { return 0x30313233; }
        }

        private VstProductInfo _productInfo;
        public VstProductInfo ProductInfo
        {
            get
            {
                if (_productInfo == null)
                {
                    _productInfo = new VstProductInfo("VST.NET Code Samples", "Jacobi Software (c) 2008", 1000);
                }

                return _productInfo;
            }
        }

        public void Resume()
        {
            // no-op
        }

        public void Suspend()
        {
            // no-op
        }

        #endregion

        #region IVstPluginMidiSource Members

        /// <summary>
        /// Returns the channel count as reported by the host
        /// </summary>
        public int ChannelCount
        {
            get
            {
                IVstMidiProcessor midiProcessor = null;
                
                if(_host != null)
                {
                    midiProcessor = _host.GetInstance<IVstMidiProcessor>();
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
