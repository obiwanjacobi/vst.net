namespace Jacobi.Vst.Plugin.Framework.Plugin
{
    using Jacobi.Vst.Core;

    /// <summary>
    /// The VstPluginAudioProcessorBase implements the <see cref="IVstPluginAudioProcessor"/> 
    /// interface and provides a basis for the Plugin implementation.
    /// </summary>
    public abstract class VstPluginAudioProcessor : IVstPluginAudioProcessor
    {
        /// <summary>
        /// A default ctor for derived classes.
        /// </summary>
        /// <remarks>When using this constructor you have to set the 
        /// <see cref="InputCount"/>, <see cref="OutputCount"/> and <see cref="TailSize"/>
        /// properties or they will be zero.</remarks>
        protected VstPluginAudioProcessor() { }

        /// <summary>
        /// Initialization ctor for derived classes.
        /// </summary>
        /// <param name="inputCount">The number of audio input channels.</param>
        /// <param name="outputCount">The number of audio output channels.</param>
        /// <param name="tailSize">The number of samples the Audio Processor will produce
        /// after input has stopped. Typically used in reverbs, echos and delays.</param>
        /// <param name="noSoundInStop">The plugin will not produce any sound when audio input is silence.</param>
        protected VstPluginAudioProcessor(int inputCount, int outputCount, int tailSize, bool noSoundInStop)
        {
            InputCount = inputCount;
            OutputCount = outputCount;
            TailSize = tailSize;
            NoSoundInStop = noSoundInStop;
        }

        #region IVstPluginAudioProcessor Members

        /// <inheritdoc />
        public int InputCount { get; protected set; }

        /// <inheritdoc />
        public int OutputCount { get; protected set; }

        /// <inheritdoc />
        public int TailSize { get; protected set; }

        /// <inheritdoc />
        public bool NoSoundInStop { get; protected set; }

        /// <inheritdoc />
        public virtual float SampleRate { get; set; }

        /// <inheritdoc />
        public virtual int BlockSize { get; set; }

        /// <inheritdoc />
        public virtual void Process(VstAudioBuffer[] inChannels, VstAudioBuffer[] outChannels)
        {
            int outCount = outChannels.Length;

            for (int n = 0; n < outCount; n++)
            {
                for (int i = 0; i < inChannels.Length && n < outCount; i++, n++)
                {
                    inChannels[i].CopyTo(outChannels[n]);
                }
            }
        }

        /// <inheritdoc />
        public virtual bool SetPanLaw(VstPanLaw type, float gain)
        {
            return false;
        }

        #endregion
    }
}
