using Jacobi.Vst.Core;

namespace Jacobi.Vst.Plugin.Framework.Plugin
{
    /// <summary>
    /// Implements a slightly optimized command handler by 
    /// caching references to the interfaces for methods called at realtime.
    /// </summary>
    /// <remarks>
    /// This does mean that these interfaces cannot be 'visible/invisible' dynamically anymore.
    /// That feature is mainly used in wrapper plugins.
    /// </remarks>
    public class VstPluginCommandsCached : VstPluginCommands
    {
        private IVstPluginAudioProcessor? _audioProcessor;
        private IVstPluginAudioPrecisionProcessor? _audioPrecisionProcessor;
        private IVstMidiProcessor? _midiProcessor;
        private IVstPluginPrograms? _programs;
        private IVstPluginParameters? _parameters;

        /// <inheritdoc />
        public VstPluginCommandsCached(VstPluginContext pluginCtx)
            : base(pluginCtx)
        { }

        /// <inheritdoc />
        public override void Open()
        {
            base.Open();

            var plugin = PluginContext.Plugin;
            _audioProcessor = plugin.GetInstance<IVstPluginAudioProcessor>();
            _audioPrecisionProcessor = plugin.GetInstance<IVstPluginAudioPrecisionProcessor>();
            _midiProcessor = plugin.GetInstance<IVstMidiProcessor>();
            _programs = plugin.GetInstance<IVstPluginPrograms>();
            _parameters = plugin.GetInstance<IVstPluginParameters>();
        }

        /// <inheritdoc />
        public override bool ProcessEvents(VstEvent[] events)
        {
            if (_midiProcessor != null)
            {
                _midiProcessor.Process(new VstEventCollection(events));
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public override void ProcessReplacing(VstAudioBuffer[] inputs, VstAudioBuffer[] outputs)
        {
            if (_audioProcessor != null)
            {
                _audioProcessor.Process(inputs, outputs);
            }
        }

        /// <inheritdoc />
        public override void ProcessReplacing(VstAudioPrecisionBuffer[] inputs, VstAudioPrecisionBuffer[] outputs)
        {
            if (_audioPrecisionProcessor != null)
            {
                _audioPrecisionProcessor.Process(inputs, outputs);
            }
        }

        /// <inheritdoc />
        public override void SetParameter(int index, float value)
        {
            if (_programs?.ActiveProgram != null &&
                _programs.ActiveProgram.IsReadOnly)
            {
                // do not allow the parameter to change value when the current program/preset is readonly.
                return;
            }

            if (_parameters != null)
            {
                _parameters.Parameters[index].NormalizedValue = value;
            }
        }

        /// <inheritdoc />
        public override float GetParameter(int index)
        {
            if (_parameters != null)
            {
                return _parameters.Parameters[index].NormalizedValue;
            }

            return 0.0f;
        }
    }
}
