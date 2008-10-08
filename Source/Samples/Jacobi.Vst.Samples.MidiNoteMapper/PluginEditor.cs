namespace Jacobi.Vst.Samples.MidiNoteMapper
{
    using System;
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework;
    using Jacobi.Vst.Framework.Common;

    /// <summary>
    /// Implements the custom UI editor for the plugin.
    /// </summary>
    class PluginEditor : IVstPluginEditor
    {
        private Plugin _plugin;
        private WinFormsControlWrapper<MidiNoteMapperUI> _uiWrapper = new WinFormsControlWrapper<MidiNoteMapperUI>();

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        public PluginEditor(Plugin plugin)
        {
            _plugin = plugin;
        }

        #region IVstPluginEditor Members

        public System.Drawing.Rectangle Bounds
        {
            get { return _uiWrapper.Bounds; }
        }

        public void Close()
        {
            _uiWrapper.Close();
        }

        public void KeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            // no-op
        }

        public void KeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            // no-op
        }

        public VstKnobMode KnobMode { get; set; }

        public void Open(IntPtr hWnd)
        {
            _uiWrapper.SafeInstance.NoteMap = _plugin.NoteMap;
            _uiWrapper.Open(hWnd);
        }

        public void ProcessIdle()
        {
            // no-op
        }

        #endregion
    }
}
